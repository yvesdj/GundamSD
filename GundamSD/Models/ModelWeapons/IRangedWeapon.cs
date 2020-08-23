using System.Collections.Generic;
using GundamSD.Maps;

namespace GundamSD.Models
{
    public interface IRangedWeapon
    {
        float FireRate { get; }
        Bullet GunBullet { get; set; }

        void AddBullet(List<ISprite> sprites);
        void ShootBullet(MapManager mapManager);
    }
}