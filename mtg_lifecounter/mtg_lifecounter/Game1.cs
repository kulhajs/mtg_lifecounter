using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

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

        Board board;

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

            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;

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
            board = new Board();

            background = new Background();
            playerOne = new Player(Id.One);
            playerTwo = new Player(Id.Two);

            players = new List<Player>();
            players.Add(playerOne);
            players.Add(playerTwo);

            controller = new Controller();
            controller.Initialize();

            board.Background = background;
            board.Controller = controller;
            board.PlayerOne = playerOne;
            board.PlayerTwo = playerTwo;
            board.Players = players;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            board.LoadContent(this.Content);

            //background.LoadContent(this.Content);

            //playerOne.LoadContent(this.Content);
            //playerTwo.LoadContent(this.Content);

            //controller.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            board.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            board.Draw(this.spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
