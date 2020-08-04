using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public interface IHealthHandler
    {
        Rectangle HealthBar { get; set; }
        bool IsDead { get; }

        void Draw(SpriteBatch spriteBatch);
        void TakeDamage(int amount);
        void Update();
    }
}