using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameHunter
{
    class BackgroundBuilder
    {
        private Texture2D texture;


        public BackgroundBuilder()
        {

        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("forest_tiles");
        }
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            
            float numX = Constants.GAME_WIDTH;
            float numY = Constants.GAME_HEIGHT;

            for(int i = 0; i < numX; i+= 16)
            {
                for(int j = 0; j < numY; j+=16)
                {
                    spriteBatch.Draw(texture, new Vector2(i, j), new Rectangle(0, 0, 16, 16), Color.White);
                }
            }

            //tree-1
            spriteBatch.Draw(texture, new Vector2(50, 250), new Rectangle(64, 0, 32, 32), Color.White);
            spriteBatch.Draw(texture, new Vector2(350, 400), new Rectangle(64, 0, 32, 32), Color.White);
            spriteBatch.Draw(texture, new Vector2(35, 3250), new Rectangle(64, 0, 32, 32), Color.White);

            //tree-2
            spriteBatch.Draw(texture, new Vector2(350, 165), new Rectangle(128, 0, 32, 64), Color.White);
            spriteBatch.Draw(texture, new Vector2(56, 365), new Rectangle(128, 0, 32, 64), Color.White);
            spriteBatch.Draw(texture, new Vector2(89, 248), new Rectangle(128, 0, 32, 64), Color.White);
            spriteBatch.Draw(texture, new Vector2(623, 295), new Rectangle(128, 0, 32, 64), Color.White);

            //Log
            spriteBatch.Draw(texture, new Vector2(150, 60), new Rectangle(0, 32, 16, 16), Color.White);
            spriteBatch.Draw(texture, new Vector2(285, 345), new Rectangle(32, 32, 16, 16), Color.White);
            spriteBatch.Draw(texture, new Vector2(15, 395), new Rectangle(64,16, 16, 16), Color.White);

            //Bushes
            spriteBatch.Draw(texture, new Vector2(250, 160), new Rectangle(0, 96, 16, 16), Color.White);
            spriteBatch.Draw(texture, new Vector2(385, 245), new Rectangle(32, 96, 16, 16), Color.White);
            spriteBatch.Draw(texture, new Vector2(115, 295), new Rectangle(16, 112, 16, 16), Color.White);
        }
    }
}
