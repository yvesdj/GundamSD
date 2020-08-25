using GundamSD.Maps;
using Microsoft.Xna.Framework;
using System;

namespace GundamSD.Models
{
    public class Shield
    {
        public ISprite Sprite { get; }
        public int BlockRange { get; set; }
        public Rectangle BlockBox { get; set; }

        public Shield(ISprite sprite, int blockRange)
        {
            Sprite = sprite;
            BlockRange = blockRange;
        }

        public void BlockDamage(MapManager mapManager)
        {
            if (Sprite is IHasInput hasInput && hasInput.Inputs.KeyIsHoldDown(hasInput.Inputs.Block))
            {
                Sprite.AtlasManager.IsBlocking = true;
                Console.WriteLine("Is Blocking");
            }
            else
            {
                Sprite.AtlasManager.IsBlocking = false;
            }
        }

    }
}