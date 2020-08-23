using System;
using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class RangedWeapon : Weapon
    {
        public Bullet GunBullet { get; set; }
        public float FireRate { get; }

        private float _timer = 0f;

        public RangedWeapon(ISprite sprite, int damage, int range, float fireRate) : base(sprite, damage, range)
        {
            FireRate = fireRate;
            GunBullet = new Bullet(Sprite.Atlas.Texture, Sprite, Damage);
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Ranged))
            {
                Sprite.AtlasManager.IsRangedAttacking = true;
                _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                ShootBullet(mapManager);
            }
            else
            {
                Sprite.AtlasManager.IsRangedAttacking = false;
            }
        }

        private void ShootBullet(MapManager mapManager)
        {
            if (_timer == 0f)
            {
                AddBullet(mapManager.Sprites);
            }
            else if (_timer >= FireRate)
            {
                _timer = 0f;
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
