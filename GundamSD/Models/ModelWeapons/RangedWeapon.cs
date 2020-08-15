using System.Collections.Generic;
using GundamSD.Maps;

namespace GundamSD.Models
{
    public class RangedWeapon : IWeapon
    {
        public int Damage { get; }
        public int Range { get; }
        public ISprite Sprite { get; }

        public Bullet GunBullet { get; set; }

        public RangedWeapon(ISprite sprite, int damage, int range)
        {
            Damage = damage;
            Range = range;
            Sprite = sprite;
            GunBullet = new Bullet(Sprite.Atlas.Texture, Sprite);
        }

        public void DealDamage(MapManager mapManager)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsPressed(hasInput.Inputs.Ranged))
            {
                AddBullet(mapManager.Sprites);
            }
        }

        public void AddBullet(List<ISprite> sprites)
        {
            Bullet bullet = GunBullet.Clone() as Bullet;

            if (bullet is ISprite sprite)
            {
                sprites.Add(sprite);
            }

        }
    }
}
