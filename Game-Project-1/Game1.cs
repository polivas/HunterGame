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
        //private ArrowSprite arrow;
        private List<ArrowSprite> arrow;

        //Arrow stuff
         private Vector2 arrowPosition;
         private bool arrowFlipped;

        private Vector2 arrowPos;

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
            for (int i = 0; i < 5; i++)
            {
                var radius = random.Next(1,5);
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

            //Spawn hunter
            Vector2 pos = (new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));

            hunter = new HunterSprite(pos);

            //Spawn Arrow into world

            //var arrowBody = world.CreateRectangle(23, 6, 1,new Vector2(-10,-10),0, BodyType.Dynamic);//check back

            arrow = new List<ArrowSprite>();

            // 5 arrows into game
            for (int i = 0; i < 5; i++)
            {

                var arrowBody = world.CreateRectangle(23, 6, 1,(pos - (new Vector2(0, 35))), 0, BodyType.Dynamic);//check back
                arrowBody.LinearVelocity = new Vector2(0, 0);
                arrowBody.AngularVelocity = (float)0;
                arrowBody.SetRestitution(1);

                arrow.Add(new ArrowSprite(pos - (new Vector2(0,35)), arrowBody));

                // body.AngularVelocity = (float)random.NextDouble() * MathHelper.Pi - MathHelper.PiOver2;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background.LoadContent(Content);

            foreach (var birds in birds) birds.LoadContent(Content);
            foreach (var arrows in arrow) arrows.LoadContent(Content);

            hunter.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var bird in birds) bird.Update(gameTime);

            
            hunter.Update(gameTime, out Vector2 arrowPosition);

           this.arrowPosition = arrowPosition;

            arrowPos = hunter.Position;

            foreach(var arrows in arrow) arrows.Update(gameTime, arrowPos);


            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Getting sprite batch
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);

            hunter.Draw(gameTime, spriteBatch);


            foreach(var arrows in arrow) arrows.Draw(gameTime, spriteBatch);

            foreach (var bird in birds) bird.Draw(gameTime, spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
