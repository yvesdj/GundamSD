using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class MeleeWeapon : IWeapon, IMeleeWeapon
    {
        public ISprite Sprite { get;}
        public int Damage { get; }
        public int Range { get; }
        public List<int> AttackFrames { get; set; }

        private Rectangle _hitBox;
        public Rectangle HitBox { get => _hitBox; }

        public MeleeWeapon(ISprite sprite, int damage, int range, List<int> attackFrames)
        {
            Sprite = sprite;
            Damage = damage;
            Range = range;
            AttackFrames = attackFrames;
        }

        public void DealDamage(MapManager mapManager)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Melee))
            {
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
        }
    }
}
