using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace mtg_lifecounter
{
    class PoisonCounter : Sprite
    {
        const int maxCount = 10;

        public Id Id { get; private set; }

        public int Count { get; set; }

        public PoisonCounter(Id id)
        {
            this.Id = id;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            texture = contentManager.Load<Texture2D>("images/poisonStep");
        }

        public void Draw(SpriteBatch theSpriteBatch, float boardY)
        {
            for(int i = 0; i < maxCount; i++)
            {
                theSpriteBatch.Draw(texture, Id == Id.One ? new Vector2(81 + i * 24, boardY + 20) : new Vector2(700 - i * 24, boardY + 20), i < Count ? Color.Lime : Color.DarkGreen);
            }
        }
    }
}
