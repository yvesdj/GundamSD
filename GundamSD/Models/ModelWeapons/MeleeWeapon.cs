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
            GunBullet = new Bullet(Sprite.Atlas.Texture, Sprite);
        }

        public void DealDamage(MapManager mapManager)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsPressed(hasInput.Inputs.Ranged))
            {
                AddBullet(mapManager.Sprites);

                //foreach (ISprite sprite in mapManager.Sprites)
                //{
                //    if (sprite is Bullet bullet)
                //    {
                //        Console.WriteLine(bullet + "Sprite Position: " + bullet.Position);
                //        Console.WriteLine(bullet + "AtlasPlayer Position: " + bullet.AtlasManager.AtlasPlayer.Position);
                //        Console.WriteLine(bullet + "Mover Position: " + bullet.Mover.NextPosition);
                //    }

                //}
            }


        }

        public void AddBullet(List<ISprite> sprites)
        {
            Bullet bullet = GunBullet.Clone() as Bullet;
            //bullet.Direction = this.Direction;
            //bullet.Position = Sprite.Position;
            //bullet.Mover.NextPosition = Sprite.Position;
            //bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 1f;
            //bullet.Parent = this;

            if (bullet is ISprite sprite)
            {
                sprites.Add(sprite);
                //Console.WriteLine(sprite.Position);
            }

        }
    }

    public class Bullet : Sprite, ICloneable
    {
        public ISprite ParentSprite { get; set; }

        public float LifeSpan { get; set; }

        public Bullet(Texture2D atlasTexture) : base(atlasTexture)
        {
            //IAnimationAtlasAction action = Factory.CreateAnimAtlasAction(90, 90, false);
            //Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            //{
            //    { "SingleAction", action }
            //};
            //AtlasManager = new AtlasAnimationSingleActionManager(this, actions);
            //Speed = 10f;
            //Mover = new MoverBullet(this);
        }

        public Bullet(Texture2D atlasTexture, ISprite parentSprite) : base(atlasTexture)
        {
            IAnimationAtlasAction action = Factory.CreateAnimAtlasAction(90, 90, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "SingleAction", action }
            };
            AtlasManager = new AtlasAnimationSingleActionManager(this, actions);
            Speed = 10f;

            ParentSprite = parentSprite;
            Mover = new MoverBullet(this);
        }

        //public override void Update(GameTime gameTime, MapManager mapManager)
        //{
        //    //_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    //if (_timer >= LifeSpan)
        //    //    IsRemoved = true;

        //    float _velocityX = Speed;
        //    float _velocityY = 0;
        //    Vector2 Velocity = new Vector2(_velocityX, _velocityY);

        //    Position += Velocity;
        //}

        public object Clone()
        {
            //return this.MemberwiseClone();
            Bullet bulletClone = (Bullet)this.MemberwiseClone();
            bulletClone.Position = new Vector2(ParentSprite.Position.X + 32, ParentSprite.Position.Y + 16);
            bulletClone.Mover = new MoverBullet(bulletClone);
            return bulletClone;
        }
    }
}
