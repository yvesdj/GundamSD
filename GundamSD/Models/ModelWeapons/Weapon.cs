using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class Weapon : IWeapon
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

        public virtual void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
