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
            MaxHealth = 50;
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
}
