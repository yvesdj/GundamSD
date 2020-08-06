using System;
using System.Collections.Generic;
using GundamSD.Maps;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public class Weapon
    {
        private ISprite _sprite; //Input
        private int _damage;
        private int _range;
        private Rectangle _hitBox;
        private bool _hasAttacked;
        public List<int> AttackFrames { get; set; }

        public Weapon(ISprite sprite, int damage, int range, List<int> attackFrames)
        {
            _sprite = sprite;
            _damage = damage;
            _range = range;
            AttackFrames = attackFrames;
        }

        public void DealDamage(MapManager mapManager)
        {
            if (_sprite.Inputs.KeyIsHoldDown(_sprite.Inputs.Attack))
            {
                _hitBox = new Rectangle(_sprite.HitBox.X, _sprite.HitBox.Y,
                                        _sprite.HitBox.Width + _range, _sprite.HitBox.Height);

                ISprite target = _sprite.CollisionHandler.GetOtherSprite(_hitBox, mapManager);
                
                for (int i = 0; i < AttackFrames.Count; i++)
                {
                    if (AttackFrames[i] == _sprite.AtlasManager.AtlasPlayer.CurrentFrame && target != null)
                    {
                        _hasAttacked = true;
                        break;
                        //target.HealthHandler.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}
