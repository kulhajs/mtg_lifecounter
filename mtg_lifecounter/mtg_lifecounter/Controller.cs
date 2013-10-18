using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mtg_lifecounter
{
    class Controller
    {
        List<Button> buttons;

        TouchCollection touchCollection;

        Vector2 touchPosition;
        TouchLocationState touchState;

        public Controller()
        {
            buttons = new List<Button>();
        }

        public void Initialize()
        {
            //hurt buttons
            buttons.Add(new Button(ButtonType.Hurt, Id.One, new Vector2(40, 280)));
            buttons.Add(new Button(ButtonType.Hurt, Id.Two, new Vector2(760, 280)));
            //heal buttons
            buttons.Add(new Button(ButtonType.Heal, Id.One, new Vector2(360, 280)));
            buttons.Add(new Button(ButtonType.Heal, Id.Two, new Vector2(440, 280)));
            //add poison buttons
            buttons.Add(new Button(ButtonType.AddPoison, Id.One, new Vector2(360, 40)));
            buttons.Add(new Button(ButtonType.AddPoison, Id.Two, new Vector2(440, 40)));
            //remove poison buttons
            buttons.Add(new Button(ButtonType.RemovePoison, Id.One, new Vector2(40, 40)));
            buttons.Add(new Button(ButtonType.RemovePoison, Id.Two, new Vector2(760, 40)));
            //dice buttons
            buttons.Add(new Button(ButtonType.Dice, Id.One, new Vector2(360, 440)));
            buttons.Add(new Button(ButtonType.Dice, Id.Two, new Vector2(440, 440)));
            //restart buttons
            buttons.Add(new Button(ButtonType.Reset, Id.One, new Vector2(40, 440)));
            buttons.Add(new Button(ButtonType.Reset, Id.Two, new Vector2(760, 440)));
        }

        public void Update(List<Player> players)
        {
            touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                touchState = touchCollection[0].State;
                if(touchState == TouchLocationState.Pressed)
                {
                    touchPosition = touchCollection[0].Position;
                    HandleTouch(new Rectangle((int)touchPosition.X - 16, (int)touchPosition.Y - 16, 32, 32), players);
                }
            }


        }

        public void HandleTouch(Rectangle touchRectangle, List<Player> players)
        {
            foreach(Button button in buttons)
            {
                if(button.ButtonRectangle.Intersects(touchRectangle))
                {
                    if (button.ButtonType == ButtonType.Heal) { players.Where(player => player.Id == button.Id).Single().Heal(); return; }
                    if (button.ButtonType == ButtonType.Hurt) { players.Where(player => player.Id == button.Id).Single().Hurt(); return; }
                    if (button.ButtonType == ButtonType.AddPoison) { players.Where(player => player.Id == button.Id).Single().AddPoison(); return; }
                    if (button.ButtonType == ButtonType.RemovePoison) { players.Where(player => player.Id == button.Id).Single().RemovePoison(); return; }
                    if (button.ButtonType == ButtonType.Reset) { players.Where(player => player.Id == button.Id).Single().Reset(); return; }
                    if (button.ButtonType == ButtonType.Dice) { players.Where(player => player.Id == button.Id).Single().Dice(); return; }
                }
            }
        }

        public void LoadContent(ContentManager theContentManager)
        {
            foreach (Button button in buttons)
                button.LoadContent(theContentManager);
        }

        public void Draw(SpriteBatch theSpritebatch)
        {
            foreach (Button button in buttons)
                button.Draw(theSpritebatch);
        }
    }
}
