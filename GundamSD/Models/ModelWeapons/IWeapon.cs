using GundamSD.Maps;

namespace GundamSD.Models
{
    public interface IWeapon
    {
        int Damage { get; }
        int Range { get; }
        ISprite Sprite { get; }

        void DealDamage(MapManager mapManager);
    }
}