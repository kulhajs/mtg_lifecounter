using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace mtg_lifecounter
{
    enum Id
    {
        One = 1,
        Two = 2
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

        public bool ShowPercentage { get; set; }

        public bool ScoreSaved { get; set; }

        public int Hitpoints { get; set; }

        public int PercentGamesWon { get; set; }

        bool dice = false;

        const float diceTime = 1.5f;

        float elapsedDiceTime = 0.0f;

        int diceValue;

        public Id Id { get; set; }

        public Player(Id id)
        {
            this.Id = id;
            poisonCounter = new PoisonCounter(Id);
            this.poisonCounter.Count = initialPoison;
            this.Hitpoints = initialHp;
            this.ScoreSaved = false;

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
            if (this.poisonCounter.Count > 0)
            {
                poisonCounter.Count -= 1;
            }
        }

        public void Reset()
        {
            this.poisonCounter.Count = initialPoison;
            this.Hitpoints = initialHp;
            this.ScoreSaved = false;
            this.ShowPercentage = false;
        }

        public void Dice()
        {
            if (!dice)
            {
                dice = true;
                elapsedDiceTime = 0.0f;
                Random random = new Random();
                diceValue = random.Next(1, 21);
            }
        }

        public void Draw(SpriteBatch theSpriteBatch, GameTime gameTime)
        {
            if (dice)
            {
                elapsedDiceTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedDiceTime > diceTime)
                {
                    dice = false;
                    diceValue = 0;
                }
            }

            string text;

            if(!ShowPercentage)
            {
                text = dice == false ? this.Hitpoints.ToString() : diceValue.ToString();   
            }
            else
            {
                text = PercentGamesWon.ToString();
            }

            if (this.Id == Id.One)
            {
                theSpriteBatch.DrawString(
                    font,
                    ShowPercentage == false ? text : text + "%",
                    ShowPercentage == false ? new Vector2(70, 460 - text.Length * 40) : new Vector2(70, 390 - text.Length * 40),           //position
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
                    ShowPercentage == false ? text : text + "%",
                    ShowPercentage == false ? new Vector2(750, 100 + text.Length * 40) : new Vector2(750, 180 + text.Length * 40),          //position
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
