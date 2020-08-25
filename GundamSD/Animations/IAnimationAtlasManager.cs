using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Animations
{
    public interface IAnimationAtlasManager
    {
        IAnimationAtlasPlayer AtlasPlayer { get; set; }

        bool IsMeleeAttacking { get; set; }
        bool IsRangedAttacking { get; set; }
        bool IsBlocking { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void SetAnimation();
        void Update(GameTime gameTime);
    }
}