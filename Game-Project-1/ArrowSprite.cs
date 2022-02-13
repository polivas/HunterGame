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
        private Texture2D texture;
        private double animationTimer;
        private short animationFrame = 0;


        float width;
        float length;

        float scale;

        Vector2 origin;
        Body body;

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
        public void Update(GameTime gameTime, Vector2 position, bool flipped)
        {
            Colliding = false;
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
            Colliding = true;
            return true;
        }
    }
}
