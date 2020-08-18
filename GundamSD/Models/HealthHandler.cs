using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public class HealthHandler : IHealthHandler
    {
        private ISprite _sprite;
        private Texture2D _healthBarText;
        private int _currentHealth;
        private Color _color;

        public bool IsDead { get { return _currentHealth <= 0; } }
        public Rectangle HealthBar { get; set; }

        public HealthHandler(ISprite sprite, Color color)
        {
            _sprite = sprite;
            if (_sprite is IHasHealth hasHealth)
            {
                _currentHealth = hasHealth.MaxHealth;
            }
            
            _color = color;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _sprite.AtlasManager.AtlasPlayer.Color = Color.Red;
        }

        public void Update()
        {
            HealthBar = new Rectangle((int)_sprite.Position.X - 6, (int)_sprite.Position.Y - 20, _currentHealth / 2, 5);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _healthBarText = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _healthBarText.SetData(new Color[] { _color });
            spriteBatch.Draw(_healthBarText, HealthBar, Color.White);
        }
    }
}
