using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mtg_lifecounter
{
    enum Id
    {
        One,
        Two
    };

    class Player : Sprite
    {
        SpriteFont font;

        Vector2 rotationOrigin = new Vector2(200, 280);

        const int initialHp = 20;

        const int minimumHp = 0;

        const int initialPoison = 0;

        const int maximumPoison = 10;

        public bool DeadEh { get { return this.Hitpoints <= minimumHp || this.PoisonCounters >= maximumPoison; } }

        public int Hitpoints { get; set; }

        public int PoisonCounters { get; set; }

        public Id Id { get; set; }

        public Player(Id id)
        {
            this.Id = id;
            this.PoisonCounters = initialPoison;
            this.Hitpoints = initialHp;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            font = contentManager.Load<SpriteFont>("fonts/segoe");
        }

        public void Hurt()
        {
            this.Hitpoints -= 1;
        }

        public void Heal()
        {
            this.Hitpoints += 1;
        }

        public void AddPoison()
        {
            this.PoisonCounters += 1;
        }

        public void RemovePoison()
        {
            this.PoisonCounters -= 1;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if(this.Id == mtg_lifecounter.Id.One)
            {
                theSpriteBatch.DrawString(
                    font, 
                    this.Hitpoints.ToString(), 
                    new Vector2(70, 380),           //position
                    Color.Black, FPI / 2,           //rotation
                    rotationOrigin,
                    this.Scale, 
                    SpriteEffects.None, 
                    0f);
            }
            else
            {
                theSpriteBatch.DrawString(
                    font, 
                    this.Hitpoints.ToString(), 
                    new Vector2(750, 180),          //position
                    Color.White, 3 * FPI / 2,       //rotation
                    rotationOrigin, 
                    this.Scale, 
                    SpriteEffects.None, 
                    0f);
            }
        }
    }
}
