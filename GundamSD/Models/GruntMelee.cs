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
    public class GruntMelee : GruntBase, IGruntHasMelee
    {
        public IWeapon MeleeWeapon { get; set; }
        public List<int> AttackFrames { get; }

        public GruntMelee(Texture2D atlasTexture) : base(atlasTexture)
        {
            IAnimationAtlasAction Melee = Factory.CreateAnimAtlasAction(20, 22, false);
            Actions.Add("Melee", Melee);

            Mover = new MoverAI(this);

            List<int> AttackFrames = new List<int>() { 21 };
            MeleeWeapon = new MeleeWeaponAI(this, 1, 20, AttackFrames);
        }

        public override void Update(GameTime gameTime, MapManager mapManager)
        {
            base.Update(gameTime, mapManager);
            MeleeWeapon.DealDamage(mapManager, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
