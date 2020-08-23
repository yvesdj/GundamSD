using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class MeleeWeapon : Weapon , IMeleeWeapon
    {
        private Rectangle _hitBox;
        public virtual Rectangle HitBox { get => _hitBox; }
        public List<int> AttackFrames { get; set; }

        public MeleeWeapon(ISprite sprite, int damage, int range, List<int> attackFrames) : base(sprite, damage, range)
        {
            AttackFrames = attackFrames;
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Melee))
            {
                Sprite.AtlasManager.IsMeleeAttacking = true;
                _hitBox = new Rectangle(Sprite.HitBox.X, Sprite.HitBox.Y,
                                        Sprite.HitBox.Width + Range, Sprite.HitBox.Height);

                ISprite target = Sprite.CollisionHandler.GetOtherSprite(_hitBox, mapManager);

                if (target is IHasHealth hasHealth)
                {
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

                }

            }
            else
            {
                Sprite.AtlasManager.IsMeleeAttacking = false ;
            }
        }
    }
}
