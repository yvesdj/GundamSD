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
        public IWeapon MeleeWeapon { get; set; }
        public IWeapon RangedWeapon { get; set; }

        public Player(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction WalkRight = Factory.CreateAnimAtlasAction(0, 3, false);
            IAnimationAtlasAction Jump = Factory.CreateAnimAtlasAction(5, 7, true);
            IAnimationAtlasAction Melee = Factory.CreateAnimAtlasAction(30, 38, false);
            IAnimationAtlasAction Ranged = Factory.CreateAnimAtlasAction(10, 10, false);
            Dictionary<string, IAnimationAtlasAction> actions = new Dictionary<string, IAnimationAtlasAction>()
            {
                { "WalkRight", WalkRight },
                { "Jump", Jump },
                { "Melee", Melee },
                { "Ranged", Ranged },
            };
            AtlasManager = Factory.CreateAnimAtlasManager(this, actions);
            Inputs = Factory.CreateInput();
            HealthHandler = Factory.CreateHealthHandler(this, Color.Green);

            List<int> attackFrames = new List<int>() { 31, 34, 37 };
            MeleeWeapon = new MeleeWeapon(this, 1, 20, attackFrames);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            MeleeWeapon.DealDamage(mapManager);
        }
    }
}
