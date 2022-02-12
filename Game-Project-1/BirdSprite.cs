using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;


namespace GameHunter
{
    public enum BirdDirection
    {
        Left = 0,
        Up = 1,
        Down = 2,
        Right = 3,
    }

    public class BirdSprite
    {
        private Texture2D texture;

        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 0;

        Vector2 origin;
        Body body;

        /// <summary>
        /// the direction of the birds
        /// </summary>
        public BirdDirection Direction;

        /// <summary>
        /// poition of the bird
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// A boolean indicating if this birds is colliding with an object
        /// </summary>
        public bool Colliding { get; protected set; }

        public BirdSprite(float radius , Body body)
        {
            this.body.OnCollision += CollisionHandler;
        }

        /// <summary>
        /// Loads the bird's texture
        /// </summary>
        /// <param name="contentManager">The content manager to use</param>
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("bird_3_sparrow");
        }

        /// <summary>
        /// Updates the birds sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //Clear collision
            Colliding = false;

        }


        /// <summary>
        /// Draws the animated sprite
        /// </summary>
        /// <param name="gametime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to draw with</param>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //Check if collides
            Color color = (Colliding) ? Color.Green : Color.White;

            //Update animation Timer
            animationTimer += gametime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 1;
                animationTimer -= 0.3;
            }

            //Draw the sprite
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);

            spriteBatch.Draw(texture, Position, source, Color.White);
        }


        //Is checking if it has collided wiht an object
        bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }
    }
}
