using System.Collections.Generic;

namespace GundamSD.Models
{
    public interface IGruntHasMelee
    {
        List<int> AttackFrames { get; }
        IWeapon MeleeWeapon { get; set; }
    }
}