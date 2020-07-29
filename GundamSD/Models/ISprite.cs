using System.Collections.Generic;
using GundamSD.Animations;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public interface ISprite
    {
        IAnimationAtlas Atlas { get; set; }

        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, List<ISprite> sprites);
        Vector2 Velocity { get; set; }
        float Speed { get; set; }
        float JumpHeight { get; set; }
        IInput Inputs { get; set; }
    }
}