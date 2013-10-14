using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mtg_lifecounter
{
    class Background : Sprite
    {

        Texture2D playerBackground;
        Texture2D poisonBackground;


        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            playerBackground = contentManager.Load<Texture2D>("images/playerBackground");
            poisonBackground = contentManager.Load<Texture2D>("images/poisonBackground");
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(poisonBackground, new Vector2(0, 0), Color.Gray);
            theSpriteBatch.Draw(playerBackground, new Vector2(0, 80), Color.White);
            theSpriteBatch.Draw(playerBackground, new Vector2(400, 80), Color.Black);
        }
    }
}
