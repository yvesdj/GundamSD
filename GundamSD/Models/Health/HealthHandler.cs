﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GundamSD.Models
{
    public class HealthHandler : IHealthHandler
    {
        private ISprite _sprite;
        private Texture2D _healthBarText;
        
        private int _maxHealth;
        private Color _color;

        private int _currentHealth;
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (IsDead)
                {
                    _currentHealth = value;
                }
            }
        }

        private bool _isDead;
        public bool IsDead
        {
            get => _currentHealth <= 0;
            set { _isDead = value; }
        }

        public Rectangle HealthBar { get; set; }

        public HealthHandler(ISprite sprite, Color color)
        {
            _sprite = sprite;
            if (_sprite is IHasHealth hasHealth)
            {
                _currentHealth = hasHealth.MaxHealth;
                _maxHealth = hasHealth.MaxHealth;
            }
            
            _color = color;
        }

        public void TakeDamage(int amount)
        {
            if (_sprite.AtlasManager.IsBlocking)
            {
                return;
            } else
            {
                _currentHealth -= amount;
                _sprite.AtlasManager.AtlasPlayer.Color = Color.Red;
            }
        }

        public void Update()
        {
            int healthPercentage = (int)Math.Round((double)(100 * _currentHealth) / _maxHealth);
            HealthBar = new Rectangle((int)_sprite.Position.X - 6, (int)_sprite.Position.Y - 20, healthPercentage / 2, 5);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _healthBarText = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _healthBarText.SetData(new Color[] { _color });
            spriteBatch.Draw(_healthBarText, HealthBar, Color.White);
        }
    }
}
