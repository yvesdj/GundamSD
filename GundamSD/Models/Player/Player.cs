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
    public class Player : Sprite, IHasHealth, IHasInput, IHasScore
    {
        public IWeapon MeleeWeapon { get; set; }
        public IWeapon RangedWeapon { get; set; }
        public Shield Shield { get; set; }

        public int MaxHealth { get; set; }
        public int Lives { get; set; }
        public IHealthHandler HealthHandler { get; set; }
        public IInput Inputs { get; set; }

        public int Score { get; set; }

        public Player(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction WalkLeft = Factory.CreateAnimAtlasAction(15, 18, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 7, true);
            IAnimationAtlasAction MeleeRight = Factory.CreateAnimAtlasAction(30, 38, false);
            IAnimationAtlasAction MeleeLeft = Factory.CreateAnimAtlasAction(90, 98, false);
            IAnimationAtlasAction BlockRight = Factory.CreateAnimAtlasAction(4, 4, true);
            IAnimationAtlasAction BlockLeft = Factory.CreateAnimAtlasAction(14, 14, true);
            IAnimationAtlasAction RangedRight = Factory.CreateAnimAtlasAction(10, 10, true);
            IAnimationAtlasAction RangedLeft = Factory.CreateAnimAtlasAction(12, 12, true);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "WalkLeft", WalkLeft },
                { "Jump", Jump },
                { "MeleeRight", MeleeRight },
                { "MeleeLeft", MeleeLeft },
                { "RangedRight", RangedRight },
                { "RangedLeft", RangedLeft },
                { "BlockRight", BlockRight },
                { "BlockLeft", BlockLeft },
            };
            AtlasManager = Factory.CreateAnimAtlasManager(this, actions);
            Inputs = Factory.CreateInput();

            MaxHealth = 100;
            Lives = 3;
            HealthHandler = Factory.CreateHealthHandler(this, Color.Green);

            List<int> attackFramesRight = new List<int>() { 31, 34, 37 };
            List<int> attackFramesLeft = new List<int>() { 91, 94, 97 };
            MeleeWeapon = new MeleeWeapon(this, 1, 20, attackFramesRight, attackFramesLeft);

            RangedWeapon = new RangedWeapon(this, 10, 20, 300f);

            Shield = new Shield(this, 5);

            Score = 0;
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            MeleeWeapon.DealDamage(mapManager, gameTime);
            RangedWeapon.DealDamage(mapManager, gameTime);
            Shield.BlockDamage(mapManager);
            HealthHandler.Update();

            Console.WriteLine(Lives);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            HealthHandler.Draw(spriteBatch);
        }
    }
}
