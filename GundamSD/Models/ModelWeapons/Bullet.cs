using System;
using System.Collections.Generic;
using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class Bullet : Sprite, ICloneable
    {
        public ISprite ParentSprite { get; set; }
        public int Damage { get; }
        public int BulletFrame { get; }

        public float LifeSpan { get; set; }
        public bool IsExpired { get; set; }
        public override Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, 16, 12);
        private float _timer; 

        public Bullet(Texture2D atlasTexture) : base(atlasTexture)
        {
        }

        public Bullet(Texture2D atlasTexture, ISprite parentSprite, int damage, int bulletFrame) : base(atlasTexture)
        {
            BulletFrame = bulletFrame;
            IAnimationAtlasAction action = Factory.CreateAnimAtlasAction(bulletFrame, bulletFrame, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "SingleAction", action }
            };
            AtlasManager = new AtlasAnimationSingleActionManager(this, actions);
            Damage = damage;
            LifeSpan = 1f;
            Speed = 10f;

            ParentSprite = parentSprite;
            bool direction = (ParentSprite.Mover.IsMovingLeft) ? true : false;
            Mover = new MoverBullet(this, direction);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            ISprite target = CollisionHandler.GetOtherSprite(HitBox, mapManager);

            if (target is IHasHealth hasHealth)
            {
                hasHealth.HealthHandler.TakeDamage(Damage);
                if (ParentSprite is IHasScore hasScore)
                {
                    hasScore.Score += Damage;
                }
                IsExpired = true;
            }
            BulletTimer(gameTime);
        }

        private void BulletTimer(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsExpired = true;
        }

        public object Clone()
        {
            Bullet bulletClone = (Bullet)this.MemberwiseClone();

            IAnimationAtlasAction action = Factory.CreateAnimAtlasAction(BulletFrame, BulletFrame, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "SingleAction", action }
            };
            bulletClone.AtlasManager = new AtlasAnimationSingleActionManager(bulletClone, actions);

            bool direction = (ParentSprite.Mover.IsMovingLeft) ? true : false;
            if (direction)
                bulletClone.Position = new Vector2(ParentSprite.Position.X - 6, ParentSprite.Position.Y);
            else
                bulletClone.Position = new Vector2(ParentSprite.Position.X + 32, ParentSprite.Position.Y);

            bulletClone.Mover = new MoverBullet(bulletClone, direction);
            bulletClone.CollisionHandler = new CollisionHandler(bulletClone);
            return bulletClone;
        }
    }
}
