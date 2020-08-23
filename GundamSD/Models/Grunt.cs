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
            Mover = new MoverAI(this);
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
}
