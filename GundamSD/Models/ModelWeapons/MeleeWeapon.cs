using System;
using System.Collections.Generic;
using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                        }
                    }

                }

            }
        }
    }

    public class RangedWeapon : IWeapon
    {

        public int Damage { get; }
        public int Range { get; }
        public ISprite Sprite { get; }

        public Bullet GunBullet { get; set; }

        public RangedWeapon(ISprite sprite, int damage, int range)
        {
            Damage = damage;
            Range = range;
            Sprite = sprite;
            GunBullet = new Bullet(Sprite.Atlas.Texture);
        }

        public void DealDamage(MapManager mapManager)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsPressed(hasInput.Inputs.Ranged))
            {
                AddBullet(mapManager.Sprites);
            }


        }

        public void AddBullet(List<ISprite> sprites)
        {
            Bullet bullet = GunBullet.Clone() as Bullet;
            //bullet.Direction = this.Direction;
            bullet.Position = Sprite.Position;
            //bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 1f;
            //bullet.Parent = this;

            if (bullet is ISprite sprite)
                sprites.Add(sprite);
        }
    }

    public class Bullet : Sprite, ICloneable
    {
        //public IAnimationAtlasPlayer AnimationAtlasPlayer { get; set; }

        public float LifeSpan { get; set; }

        public Bullet(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction action = Factory.CreateAnimAtlasAction(90, 90, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "SingleAction", action }
            };
            AtlasManager = new AtlasAnimationSingleActionManager(this, actions);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
