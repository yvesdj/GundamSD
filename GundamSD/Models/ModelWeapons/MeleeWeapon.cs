using System;
using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class MeleeWeapon : Weapon , IMeleeWeapon
    {
        private Rectangle _hitBox;
        public virtual Rectangle HitBox { get => _hitBox; }

        public List<int> AttackFramesRight { get; set; }
        public List<int> AttackFramesLeft { get; set; }

        public MeleeWeapon(ISprite sprite, int damage, int range, List<int> attackFramesRight, List<int> attackFramesLeft) : base(sprite, damage, range)
        {
            AttackFramesRight = attackFramesRight;
            AttackFramesLeft = attackFramesLeft;
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Melee))
            {
                Sprite.AtlasManager.IsMeleeAttacking = true;

                GenerateHitbox();

                List<int> attackFrames = DetermineAttackFrames();

                ISprite target = Sprite.CollisionHandler.GetOtherSprite(_hitBox, mapManager);

                if (target is IHasHealth hasHealth)
                {
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

                }

            }
            else
            {
                Sprite.AtlasManager.IsMeleeAttacking = false ;
            }
        }

        public List<int> DetermineAttackFrames()
        {
            if (Sprite.Mover.IsMovingRight)
                return AttackFramesRight;
            else
                return AttackFramesLeft;

        }

        public virtual void GenerateHitbox()
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
