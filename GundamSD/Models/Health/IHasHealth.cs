namespace GundamSD.Models
{
    public interface IHasHealth
    {
        int MaxHealth { get; set; }
        int Lives { get; set; }
        IHealthHandler HealthHandler { get; set; }
    }
}