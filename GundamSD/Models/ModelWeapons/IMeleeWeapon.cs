using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GundamSD.Models
{
    public interface IMeleeWeapon
    {
        List<int> AttackFramesRight { get; set; }
        List<int> AttackFramesLeft { get; set; }
        Rectangle HitBox { get; }

        List<int> DetermineAttackFrames();
        void GenerateHitbox();
    }
}