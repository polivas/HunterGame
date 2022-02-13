﻿using System;
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

    public enum TextureMode
    {
        idle = 0,
        Right = 2,
        Left = 2,
        Shoot = 3,
        Up = 4,
        Down = 5,
    }


    public class HunterSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture; //Idle,Gesture,Walk,Attack, - 10 frames per
        private Texture2D textureUp;
        private Texture2D textureDown;

        //Animation Stuff
        private bool flipped;
        private bool pressing = false;
        private double animationTimer;
        private short animationFrame = 0;

        /// <summary>
        /// the direction of the hunter
        /// </summary>
        public TextureMode TextureMode;

        /// <summary>
        /// poition of the hunter
        /// </summary>
        public Vector2 Position;

        public HunterSprite(Vector2 position)
        {
            this.Position = position;
            
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ranger");
            textureUp = content.Load<Texture2D>("Ranger Walk Up");
            textureDown = content.Load<Texture2D>("Walk Down Ranger");
        }


        /// <summary>
        /// Updates the hunter sprite
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, -1);
                TextureMode = TextureMode.Up; //4
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1);
                TextureMode = TextureMode.Down;//5
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0);
                TextureMode = TextureMode.Left;
                flipped = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0);
                TextureMode = TextureMode.Right;
                flipped = false;
            }

            if (keyboardState.IsKeyDown(Keys.Space) )
            {
                TextureMode = TextureMode.Shoot;
            }
            

        }


        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
     

            //Update animation Timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3 )
            {
                animationFrame++;
                if (animationFrame > 9) animationFrame = 0;
                animationTimer -= 0.3;
            }
           if (animationTimer > 0.3 ) animationTimer -= 0.3;

           
            var source = new Rectangle(animationFrame * 32, (int)TextureMode * 32, 32, 32);

            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Texture2D currTexture = texture;

/*
            if (TextureMode == TextureMode.Down)
            { 
                spriteBatch.Draw(textureDown, Position, source, Color.White, 0f, new Vector2(32, 32), 1.25f, SpriteEffects.None, 0);
            }
            if (TextureMode == TextureMode.Up)
            {
                spriteBatch.Draw(textureUp, Position, source, Color.White, 0f, new Vector2(32, 32), 1.25f, SpriteEffects.None, 0);
            }
            else if (!(TextureMode == TextureMode.Up) && !(TextureMode == TextureMode.Down)) spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(32, 32), 1.25f, spriteEffects, 0);
*/
            if (TextureMode == TextureMode.Down) currTexture = textureDown;
            else if(TextureMode == TextureMode.Up) currTexture = textureUp;
            if (!(TextureMode == TextureMode.Up) && !(TextureMode == TextureMode.Down)) currTexture = texture;

            spriteBatch.Draw(currTexture, Position, source, Color.White, 0f, new Vector2(32, 32), 1.25f, spriteEffects, 0);

        }

    }
}
