using System;
using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class RangedWeapon : Weapon, IRangedWeapon
    {
        public Bullet GunBullet { get; set; }
        public float FireRate { get; set; }

        public float Timer { get; set; }

        public RangedWeapon(ISprite sprite, int damage, int range, float fireRate) : base(sprite, damage, range)
        {
            FireRate = fireRate;
            GunBullet = new Bullet(Sprite.Atlas.Texture, Sprite, Damage, 90);
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Ranged))
            {
                Sprite.AtlasManager.IsRangedAttacking = true;
                Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                ShootBullet(mapManager);
            }
            else
            {
                Sprite.AtlasManager.IsRangedAttacking = false;
            }
        }

        public void ShootBullet(MapManager mapManager)
        {
            if (Timer == 0f)
            {
                AddBullet(mapManager.Sprites);
            }
            else if (Timer >= FireRate)
            {
                Timer = 0f;
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

    public class RangedWeaponAI : RangedWeapon
    {
        public Rectangle KillZone { get; set; }

        public RangedWeaponAI(ISprite sprite, int damage, int range, float fireRate) : base(sprite, damage, range, fireRate)
        {
            FireRate = fireRate;
            GunBullet = new Bullet(Sprite.Atlas.Texture, Sprite, Damage, 10);
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            KillZone = new Rectangle(Sprite.HitBox.X - Range, Sprite.HitBox.Y, 
                                        Sprite.HitBox.Width + Range, Sprite.HitBox.Height);
            ISprite target = Sprite.CollisionHandler.GetOtherSprite(KillZone, mapManager);

            if (target is Player player)
            {
                Sprite.AtlasManager.IsRangedAttacking = true;
                Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                ShootBullet(mapManager);

            }
            else
            {
                Sprite.AtlasManager.IsRangedAttacking = false;
            }
        }
    }
}
