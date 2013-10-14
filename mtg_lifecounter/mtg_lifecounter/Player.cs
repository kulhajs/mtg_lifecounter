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

        PoisonCounter poisonCounter;

        const int initialHp = 20;

        const int minimumHp = 0;

        const int initialPoison = 0;

        const int maximumPoison = 10;

        public bool DeadEh { get { return this.Hitpoints <= minimumHp || this.poisonCounter.Count >= maximumPoison; } }

        public int Hitpoints { get; set; }


        public Id Id { get; set; }

        public Player(Id id)
        {
            this.Id = id;
            poisonCounter = new PoisonCounter(Id);
            this.poisonCounter.Count = initialPoison;
            this.Hitpoints = initialHp;

        }

        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            font = contentManager.Load<SpriteFont>("fonts/segoe");
            poisonCounter.LoadContent(contentManager);
        }

        public void Hurt()
        {
            if (this.Hitpoints > 0)
                this.Hitpoints -= 1;
        }

        public void Heal()
        {
            this.Hitpoints += 1;
        }

        public void AddPoison()
        {
            if (this.poisonCounter.Count < 10)
            {
                poisonCounter.Count += 1;
            }
        }

        public void RemovePoison()
        {
            if(this.poisonCounter.Count > 0)
            {
                poisonCounter.Count -= 1;
            }
        }

        public void Reset()
        {
            this.poisonCounter.Count = initialPoison;
            this.Hitpoints = initialHp;
        }

        public void Dice()
        {
            return;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if(this.Id == mtg_lifecounter.Id.One)
            {
                theSpriteBatch.DrawString(
                    font, 
                    this.Hitpoints.ToString(), 
                    this.Hitpoints >= 10 ? new Vector2(70, 380) : new Vector2(70, 420),           //position
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
                    this.Hitpoints >= 10 ? new Vector2(750, 180) : new Vector2(750, 140),          //position
                    Color.White, 3 * FPI / 2,       //rotation
                    rotationOrigin, 
                    this.Scale, 
                    SpriteEffects.None, 
                    0f);
            }

            poisonCounter.Draw(theSpriteBatch);
        }
    }
}
