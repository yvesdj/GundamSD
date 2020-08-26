using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.UI
{
    public class Button
    {
        private MouseState _mouseState;
        private MouseState _previousState;
        private SpriteFont _font;
        private bool _isHovering;
        private Texture2D _texture;

        public event EventHandler Click;

        public bool IsClicked { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Background => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont spriteFont)
        {
            _texture = texture;
            _font = spriteFont;
        }

        public Button(Texture2D texture, SpriteFont spriteFont, string text, Vector2 position)
        {
            _texture = texture;
            _font = spriteFont;
            Text = text;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            _previousState = _mouseState;
            _mouseState = Mouse.GetState();

            Rectangle mouseZone = new Rectangle(_mouseState.X, _mouseState.Y, 1, 1);

            _isHovering = false;
            if (mouseZone.Intersects(Background))
            {
                _isHovering = true;

                if (_mouseState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (_isHovering)
                color = Color.Gray;

            spriteBatch.Draw(_texture, Background, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Background.X + (Background.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Background.Y + (Background.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Gold);
            }
        }
    }
}
