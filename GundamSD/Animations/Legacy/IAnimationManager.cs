using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Animations
{
    public interface IAnimationManager
    {
        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Play(IAnimation animation);
        void Stop();
        void Update(GameTime gameTime);
    }
}