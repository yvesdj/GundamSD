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
    public class Grunt : Sprite, IHasHealth
    {
        public IWeapon MeleeWeapon { get; set; }

        public int MaxHealth { get; set; }
        public IHealthHandler HealthHandler { get; set; }

        public Grunt(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkLeft = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(15, 18, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 6, true);
            IAnimationAtlasAction Attack = Factory.CreateAnimAtlasAction(20, 22, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkLeft", WalkLeft },
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Attack", Attack },
            };
            AtlasManager = Factory.CreateAnimAtlasManager(this, actions);
            //Inputs = Factory.CreateInput();
            MaxHealth = 100;
            HealthHandler = Factory.CreateHealthHandler(this, Color.Red);
            //Mover = new MoverAI(this);

            List<int> attackFrames = new List<int>() { 21 };
            MeleeWeapon = new MeleeWeapon(this, 1, 20, attackFrames);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            //MeleeWeapon.DealDamage(mapManager);
            HealthHandler.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            HealthHandler.Draw(spriteBatch);
        }

    }

    public class MeleeWeaponAI : MeleeWeapon
    {
        private Rectangle _hitBox;
        public override Rectangle HitBox { get => _hitBox; }

        public MeleeWeaponAI(ISprite sprite, int damage, int range, List<int> attackFrames) : base(sprite, damage, range, attackFrames)
        {
        }

        public override void DealDamage(MapManager mapManager, GameTime gameTime)
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
                //base.DealDamage(mapManager);
            }
        }
    }
}
