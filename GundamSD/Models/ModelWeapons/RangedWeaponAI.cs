using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
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
