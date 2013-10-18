using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace mtg_lifecounter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Background background;

        Player playerOne;
        Player playerTwo;

        List<Player> players;

        Controller controller;

        public static ScoreDataContext scoreDb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = true;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            graphics.ApplyChanges();

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            using(ScoreDataContext db = new ScoreDataContext(ScoreDataContext.DBCoonnectionString))
            {
                if(db.DatabaseExists() == false)
                {
                    db.CreateDatabase();
                }
            }

            scoreDb = new ScoreDataContext(ScoreDataContext.DBCoonnectionString);
        }

        protected override void Initialize()
        {
            background = new Background();
            playerOne = new Player(Id.One);
            playerTwo = new Player(Id.Two);

            players = new List<Player>();
            players.Add(playerOne);
            players.Add(playerTwo);

            controller = new Controller();
            controller.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background.LoadContent(this.Content);

            playerOne.LoadContent(this.Content);
            playerTwo.LoadContent(this.Content);

            controller.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            controller.Update(players);

            if (playerOne.DeadEh && !playerOne.ScoreSaved)
            {
                scoreDb.ScoreTable.InsertOnSubmit(new Score() { WinnerId = 1 });
                playerOne.ScoreSaved = true;
                scoreDb.SubmitChanges();

                List<Score> allScores = scoreDb.ScoreTable.Where(score => score.WinnerId != 0).ToList();
                List<Score> scores = scoreDb.ScoreTable.Where(score => score.WinnerId == 1).ToList();

                playerOne.PercentGamesWon = (int)(((float)scores.Count / (float)allScores.Count) * 100);
                playerTwo.PercentGamesWon = 100 - playerOne.PercentGamesWon;

                playerOne.ShowPercentage = true;
                playerTwo.ShowPercentage = true;
            }
            else if (playerTwo.DeadEh && !playerTwo.ScoreSaved)
            {
                scoreDb.ScoreTable.InsertOnSubmit(new Score() { WinnerId = 2 });
                playerTwo.ScoreSaved = true;
                scoreDb.SubmitChanges();

                List<Score> allScores = scoreDb.ScoreTable.Where(score => score.WinnerId != 0).ToList();
                List<Score> scores = scoreDb.ScoreTable.Where(score => score.WinnerId == 2).ToList();

                playerTwo.PercentGamesWon = (int)(((float)scores.Count / (float)allScores.Count) * 100);
                playerOne.PercentGamesWon = 100 - playerTwo.PercentGamesWon;

                playerOne.ShowPercentage = true;
                playerTwo.ShowPercentage = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            background.Draw(this.spriteBatch);
            playerOne.Draw(this.spriteBatch, gameTime);
            playerTwo.Draw(this.spriteBatch, gameTime);

            controller.Draw(this.spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
