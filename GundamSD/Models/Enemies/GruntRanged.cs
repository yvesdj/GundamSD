using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class GruntRanged : GruntBase
    {
        public IWeapon RangedWeapon { get; set; }

        public GruntRanged(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction RangedLeft = Factory.CreateAnimAtlasAction(11, 11, true);
            IAnimationAtlasAction RangedRight = Factory.CreateAnimAtlasAction(12, 12, true);

            Actions.Add("RangedLeft", RangedLeft);
            Actions.Add("RangedRight", RangedRight);

            Mover = new MoverAI(this, true);
            RangedWeapon = new RangedWeaponAI(this, 10, 480, 1000f);
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
