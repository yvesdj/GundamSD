using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Animations
{
    public interface IAnimationAtlasPlayer
    {
        int CurrentFrame { get; }
        Vector2 Position { get; set; }
        Color Color { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Play(IAnimationAtlasAction action);
        void Stop();
        void Update(GameTime gameTime);
    }
}