using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public interface IMeleeWeapon
    {
        List<int> AttackFrames { get; set; }
        Rectangle HitBox { get; }
    }
}