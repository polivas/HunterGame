using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Common;

namespace GameHunter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private BackgroundBuilder background;
        private List<BirdSprite> birds;
        private HunterSprite hunter;
        private SwordSprite sword;


        private World world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //Grabbing Game constants
            graphics.PreferredBackBufferWidth = Constants.GAME_WIDTH;
            graphics.PreferredBackBufferHeight = Constants.GAME_HEIGHT;

            graphics.ApplyChanges();
        }

        /// <summary>
        /// Initilizes game
        /// </summary>
        protected override void Initialize()
        {
            System.Random rand = new System.Random();

            background = new BackgroundBuilder();


            //World Creation
            world = new World();
            world.Gravity = Vector2.Zero;

            var top = 0;
            var bottom = Constants.GAME_HEIGHT;
            var left = 0;
            var right = Constants.GAME_WIDTH;

            var edges = new Body[]{
                world.CreateEdge(new Vector2(left, top), new Vector2(right, top)),
                world.CreateEdge(new Vector2(left, top), new Vector2(left, bottom)),
                world.CreateEdge(new Vector2(left, bottom), new Vector2(right, bottom)),
                world.CreateEdge(new Vector2(right, top), new Vector2(right, bottom))
             };

            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1.0f);
            }

            //Spawn Birds/Bodies
            System.Random random = new System.Random();
            birds = new List<BirdSprite>();
            for (int i = 0; i < 8; i++)
            {
                var radius = random.Next(1, 5);
                var position = new Vector2(
                    random.Next(radius, Constants.GAME_WIDTH - radius),
                    random.Next(radius, Constants.GAME_HEIGHT - radius)
                    );

                //Adding rigid body
                var body = world.CreateCircle(radius, 1, position, BodyType.Dynamic);

                body.LinearVelocity = new Vector2(
                    random.Next(-20, 20),
                    random.Next(-20, 20)
                    );

                body.SetRestitution(1);
                body.AngularVelocity = (float)random.NextDouble() * MathHelper.Pi - MathHelper.PiOver2;
                birds.Add(new BirdSprite(radius, body));
            }

            //Spawn hunter in random location on map
            Vector2 pos = (new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            hunter = new HunterSprite(pos);


            var swordBody = world.CreateRectangle(25, 30, 1, (pos - (new Vector2(0, 35))), 0, BodyType.Dynamic);//check back
            swordBody.LinearVelocity = new Vector2(0, 0);
            swordBody.AngularVelocity = (float)0;
            swordBody.SetRestitution(1);

            sword = new SwordSprite(pos - (new Vector2(0, 35)), swordBody);

            base.Initialize();
        }

        /// <summary>
        /// Loads and creates spritebatch needed
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background.LoadContent(Content);

            foreach (var birds in birds) birds.LoadContent(Content);
            sword.LoadContent(Content);

            hunter.LoadContent(Content);

        }


        /// <summary>
        /// Updates sprites and their postions
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var bird in birds) bird.Update(gameTime);


            hunter.Update(gameTime);


            sword.Update(gameTime, (hunter.Position - (new Vector2(15, 38))), hunter.Flipped);


            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the sprites
        /// </summary>
        /// <param name="gameTime">Current gametime</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Getting sprite batch
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);

            hunter.Draw(gameTime, spriteBatch);


            sword.Draw(gameTime, spriteBatch);

            foreach (var bird in birds) bird.Draw(gameTime, spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
