using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace GameHunter
{
    public class ArrowSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;
        private double animationTimer;
        private short animationFrame = 0;


        float width;
        float length;

        float scale;

        Vector2 origin;
        Body body;

        private bool shot;

        public bool flipped;
        public Vector2 Position;

        public bool Colliding { get; protected set; }

        public ArrowSprite(Vector2 position, Body body)
        {
            this.body = body;
            this.Position = position;

            this.width = 23;
            this.length = 6;
            scale = 1;
            origin = new Vector2(5, 5);
            this.body.OnCollision += CollisionHandler;
        }

        /// <summary>
        /// Loads the arrow's texture
        /// </summary>
        /// <param name="contentManager">The content manager to use</param>
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Arrow");
        }


        /// <summary>
        /// Updates the arrow shot pattern
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, Vector2 position)
        {
            Colliding = false;
            if (keyboardState.IsKeyDown(Keys.Right)) flipped = false;
            if (keyboardState.IsKeyDown(Keys.Left)) flipped = true;

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                this.body.LinearVelocity = new Vector2(0, 5);
                this.body.AngularVelocity = (float) 1;
                shot = true;
            }
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //Check if collides
            Color color = (Colliding) ? Color.Green : Color.White;

            //Update animation Timer
            animationTimer += gametime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 1) animationFrame = 0;
                animationTimer -= 0.3;
            }
            if (animationTimer > 0.3) animationTimer -= 0.3;

            var source = new Rectangle(animationFrame * 32, 0 , 32, 32);
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if(shot == true)
            {
                spriteBatch.Draw(texture, Position, source, color, 0f, origin, scale, spriteEffects, 0);
            }

            spriteBatch.Draw(texture, Position, source, color, 0f, origin, scale, spriteEffects, 0);
        }

        /// <summary>
        /// Is checking if it has collided with an object
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="other"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            if(other.Body.BodyType == BodyType.Dynamic)
            {
                Colliding = true;
                return true;
            }

            return false;
        }
    }
}
