using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mtg_lifecounter
{
    enum ButtonType
    {
        Hurt,
        Heal,
        AddPoison,
        RemovePoison,
        Dice,
        Reset
    };

    class Button : Sprite
    {
        public ButtonType ButtonType { get; private set; }

        public Id Id { get; private set; }

        public Rectangle ButtonRectangle { get { return new Rectangle((int)X - texture.Width / 2, (int)Y - texture.Height / 2, texture.Width, texture.Height); } }

        public Button(ButtonType buttonType, Id id, Vector2 position)
        {
            this.ButtonType = buttonType;
            this.Id = id;
            this.Position = position;
            this.Rotation = Id == mtg_lifecounter.Id.One ? FPI / 2 : 3 * FPI / 2;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;

            string suffix = this.Id == Id.One ? "_white" : "";

            if (ButtonType == mtg_lifecounter.ButtonType.Hurt) { texture = contentManager.Load<Texture2D>("images/hurt" + suffix); return; }
            if (ButtonType == mtg_lifecounter.ButtonType.Heal) { texture = contentManager.Load<Texture2D>("images/heal" + suffix); return; }
            if (ButtonType == mtg_lifecounter.ButtonType.AddPoison) { texture = contentManager.Load<Texture2D>("images/heal"); return; }
            if (ButtonType == mtg_lifecounter.ButtonType.RemovePoison) { texture = contentManager.Load<Texture2D>("images/hurt"); return; }
            if (ButtonType == mtg_lifecounter.ButtonType.Dice) { texture = contentManager.Load<Texture2D>("images/20dice" + suffix); return; }
            if (ButtonType == mtg_lifecounter.ButtonType.Reset) { texture = contentManager.Load<Texture2D>("images/restart" + suffix); return; }
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if(ButtonType == ButtonType.AddPoison)
            {
                theSpriteBatch.Draw(texture, this.Position, null, Color.Lime, this.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
            }
            else if(ButtonType == ButtonType.RemovePoison)
            {
                theSpriteBatch.Draw(texture, this.Position, null, Color.DarkGreen, this.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
            }
            else
            {
                theSpriteBatch.Draw(texture, this.Position, null, Color.White, this.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);   
            }
        }
    }
}
