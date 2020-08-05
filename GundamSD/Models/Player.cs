using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class Player : Sprite
    {
        public Weapon MeleeWeapon { get; set; }

        public Player(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 7, true);
            IAnimationAtlasAction Attack = Factory.CreateAnimAtlasAction(30, 38, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Attack", Attack },
            };
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);
            Inputs = Factory.CreateInput();
            HealthHandler = Factory.CreateHealthHandler(this, Color.Green);

            MeleeWeapon = new Weapon(this, 10, 20);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            MeleeWeapon.DealDamage(mapManager);
        }
    }

    public class Weapon
    {
        private ISprite _sprite; //Input
        private int _damage;
        private int _range;
        private Rectangle _hitBox;

        public Weapon(ISprite sprite, int damage, int range)
        {
            _sprite = sprite;
            _damage = damage;
            _range = range;            
        }

        public void DealDamage(MapManager mapManager)
        {
            if (_sprite.Inputs.KeyIsPressed(_sprite.Inputs.Attack))
            {
                _hitBox = new Rectangle(_sprite.HitBox.X, _sprite.HitBox.Y,
                                        _sprite.HitBox.Width + _range, _sprite.HitBox.Height);
                if (_sprite.CollisionHandler.IsCollisionSpriteAttack(_hitBox, mapManager))
                {
                    Console.WriteLine("Attacked with " + _damage);
                }
            }
        }
    }
}
