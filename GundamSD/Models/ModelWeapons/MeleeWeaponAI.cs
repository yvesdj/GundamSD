using System;
using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class MeleeWeaponAI : MeleeWeapon
    {
        private Rectangle _hitBox;
        public override Rectangle HitBox { get => _hitBox; }

        public MeleeWeaponAI(ISprite sprite, int damage, int range, List<int> attackFramesRight, List<int> attackFramesLeft) : base(sprite, damage, range, attackFramesRight, attackFramesLeft)
        {
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            Console.WriteLine("Moving right: " + Sprite.Mover.IsMovingRight + "   Moving left: " + Sprite.Mover.IsMovingLeft);
            GenerateHitbox();

            ISprite target = Sprite.CollisionHandler.GetOtherSprite(_hitBox, mapManager);

            if (target is IHasHealth hasHealth)
            {
                Sprite.AtlasManager.IsMeleeAttacking = true;

                List<int> attackFrames = DetermineAttackFrames();

                for (int i = 0; i < attackFrames.Count; i++)
                {
                    if (attackFrames[i] == Sprite.AtlasManager.AtlasPlayer.CurrentFrame && target != null)
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

        public override void GenerateHitbox()
        {
            if (Sprite.Mover.IsMovingRight)
            {
                _hitBox = new Rectangle(Sprite.HitBox.X, Sprite.HitBox.Y,
                                    Sprite.HitBox.Width + Range, Sprite.HitBox.Height);
            }
            else if (Sprite.Mover.IsMovingLeft)
            {
                _hitBox = new Rectangle(Sprite.HitBox.X - Range, Sprite.HitBox.Y,
                                    Sprite.HitBox.Width + Range, Sprite.HitBox.Height);
            }
        }
    }
}
