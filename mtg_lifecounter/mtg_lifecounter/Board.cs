using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mtg_lifecounter
{
    class Board
    {
        Vector2 position;

        public Controller Controller { get; set; }

        public Player PlayerOne { get; set; }

        public Player PlayerTwo { get; set; }

        public List<Player> Players { get; set; }

        public Background Background { get; set; }

        public bool SlideOff;

        public Board()
        {
            this.position = Vector2.Zero;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            this.Background.LoadContent(theContentManager);
            
            this.PlayerOne.LoadContent(theContentManager);
            this.PlayerTwo.LoadContent(theContentManager);

            this.Controller.LoadContent(theContentManager);
        }

        public void Update(GameTime gameTime)
        {

            this.Controller.Update(this.Players, this);

            if (this.Players.Where(player => player.DeadEh && !player.ScoreSaved).ToList().Count > 0)
            {
                Player deadPlayer = this.Players.Where(player => player.DeadEh).Single();
                Game1.scoreDb.ScoreTable.InsertOnSubmit(new Score() { WinnerId = (int)deadPlayer.Id });
                deadPlayer.ScoreSaved = true;
                Game1.scoreDb.SubmitChanges();

                int allScores = Game1.scoreDb.ScoreTable.Where(score => score.WinnerId != 0).ToList().Count;
                int scores = Game1.scoreDb.ScoreTable.Where(score => score.WinnerId == (int)deadPlayer.Id).ToList().Count;

                deadPlayer.PercentGamesWon = (int)(((float)scores / (float)allScores) * 100);
                this.Players.Where(player => player.Id != deadPlayer.Id).Single().PercentGamesWon = 100 - deadPlayer.PercentGamesWon;

                //players.All(player => player.ShowPercentage = true;
            }

            if (this.SlideOff && this.position.Y < 480)
            {
                this.position.Y += (490 - this.position.Y) / 0.33f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch theSpriteBatch, GameTime gameTime)
        {
            this.Background.Draw(theSpriteBatch, this.position.Y);

            this.Controller.Draw(theSpriteBatch, this.position.Y);

            this.PlayerOne.Draw(theSpriteBatch, gameTime, this.position.Y);
            this.PlayerTwo.Draw(theSpriteBatch, gameTime, this.position.Y);
        }
    }
}
