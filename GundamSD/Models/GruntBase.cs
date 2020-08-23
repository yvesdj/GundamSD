using System.Collections.Generic;
using GundamSD.Animations;
using GundamSD.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class GruntBase : Sprite, IHasHealth
    {
        public int MaxHealth { get; set; }
        public IHealthHandler HealthHandler { get; set; }
        public Dictionary<string, IAnimationAtlasAction> Actions { get; set; }

        public GruntBase(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkLeft = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(15, 18, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 6, true);

            Actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkLeft", WalkLeft },
                { "WalkRight", WalkRight },
                { "Jump", Jump },
            };

            AtlasManager = Factory.CreateAnimAtlasManager(this, Actions);
            MaxHealth = 100;
            HealthHandler = Factory.CreateHealthHandler(this, Color.Red);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            HealthHandler.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            HealthHandler.Draw(spriteBatch);
        }
    }

    public class GruntRanged : GruntBase
    {
        public IWeapon RangedWeapon { get; set; }

        public GruntRanged(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction Ranged = Factory.CreateAnimAtlasAction(11, 11, true);
            Actions.Add("Ranged", Ranged);

            RangedWeapon = new RangedWeaponAI(this, 10, 480, 300f);

        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            RangedWeapon.DealDamage(mapManager, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
