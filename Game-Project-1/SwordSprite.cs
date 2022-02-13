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
    public class SwordSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;
        private Vector2 flipPos = new Vector2(40, 0);

        float width;
        float length;

        float scale;

        Vector2 origin;
        Body body;

        public bool flipped;
        public Vector2 Position;

        public bool Colliding { get; protected set; }

        public SwordSprite(Vector2 position, Body body)
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
            texture = contentManager.Load<Texture2D>("w_longsword(3)");
        }


        /// <summary>
        /// Updates the arrow shot pattern
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime, Vector2 position, bool hunterFlipped)
        {
            Colliding = false;
            if (keyboardState.IsKeyDown(Keys.Right)) flipped = false;
            if (keyboardState.IsKeyDown(Keys.Left)) flipped = true;

            this.flipped = hunterFlipped;
            this.Position = position;


            if (flipped == true)
            {
                this.body.Position = position - flipPos;
            }
            else
            {
                this.body.Position = position;
            }


        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0 , 0, 32, 32);

            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;



            if (flipped == true)
            {
                spriteBatch.Draw(texture, this.Position - flipPos, source, Color.White, 0f, origin, scale, spriteEffects, 0);
            }
            else
            {
                spriteBatch.Draw(texture, this.Position, source, Color.White, 0f, origin, scale, spriteEffects, 0);
            }


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
            if (other.Body.BodyType == BodyType.Dynamic)
            {
                Colliding = true;
                return true;
            }

            return false;
        }
    }
}
