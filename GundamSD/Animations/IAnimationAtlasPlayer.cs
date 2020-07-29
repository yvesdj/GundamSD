using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Animations
{
    public interface IAnimationAtlasPlayer
    {
        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Play(IAnimationAtlasAction action);
        void Stop();
        void Update(GameTime gameTime);
    }
}