using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public abstract class Weapon : IWeapon
    {

        public ISprite Sprite { get; }
        public int Damage { get; }
        public int Range { get; }

        public Weapon(ISprite sprite, int damage, int range)
        {
            Sprite = sprite;
            Damage = damage;
            Range = range;
        }

        public abstract void DealDamage(MapManager mapManager, GameTime gameTime);

    }
}
