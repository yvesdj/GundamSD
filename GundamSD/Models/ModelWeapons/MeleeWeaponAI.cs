using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class MeleeWeaponAI : MeleeWeapon
    {
        private Rectangle _hitBox;
        public override Rectangle HitBox { get => _hitBox; }

        public MeleeWeaponAI(ISprite sprite, int damage, int range, List<int> attackFrames) : base(sprite, damage, range, attackFrames)
        {
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            //Sprite.AtlasManager.IsMeleeAttacking = true;
            _hitBox = new Rectangle(Sprite.HitBox.X - Range, Sprite.HitBox.Y,
                                        Sprite.HitBox.Width + Range, Sprite.HitBox.Height);

            ISprite target = Sprite.CollisionHandler.GetOtherSprite(_hitBox, mapManager);

            if (target is IHasHealth hasHealth)
            {
                Sprite.AtlasManager.IsMeleeAttacking = true;

                for (int i = 0; i < AttackFrames.Count; i++)
                {
                    if (AttackFrames[i] == Sprite.AtlasManager.AtlasPlayer.CurrentFrame && target != null)
                    {
                        hasHealth.HealthHandler.TakeDamage(Damage);

                        if (Sprite is IHasScore hasScore)
                        {
                            hasScore.Score += Damage;
                        }
                    }
                }
                //base.DealDamage(mapManager);
            } else
                Sprite.AtlasManager.IsMeleeAttacking = false;
        }
    }
}
