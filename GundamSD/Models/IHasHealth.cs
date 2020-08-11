namespace GundamSD.Models
{
    public interface IHasHealth
    {
        int MaxHealth { get; set; }
        IHealthHandler HealthHandler { get; set; }
    }
}