using System.Collections.Generic;
using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GundamSD.Models
{
    public interface ISprite
    {
        IAnimationAtlas Atlas { get; set; }
        IAnimationAtlasManager AtlasManager { get; set; }
        CollisionHandler CollisionHandler { get; set; }

        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, MapManager mapManager);
        
        float Speed { get; set; }
        float JumpHeight { get; set; }
        IInput Inputs { get; set; }
        IMover Mover { get; set; }
        Rectangle HitBox { get; }

        //int MaxHealth { get; set; }
        //IHealthHandler HealthHandler { get; set; }
    }
}