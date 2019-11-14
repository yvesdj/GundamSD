using System.Collections.Generic;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public interface ISprite
    {
        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, List<Sprite> sprites);
        Vector2 Velocity { get; set; }
        float Speed { get; set; }
        IInput Inputs { get; set; }
    }
}