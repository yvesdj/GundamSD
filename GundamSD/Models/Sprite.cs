using GundamSD.Animations;
using GundamSD.Maps;
using GundamSD.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Models
{
    public class Sprite : ISprite
    {
        public IAnimationAtlas Atlas { get; set; }
        public IAnimationAtlasManager AtlasManager { get; set; }
        public CollisionHandler CollisionHandler { get; set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (AtlasManager != null)
                {
                    AtlasManager.AtlasPlayer.Position = _position;
                }
            }
        }

        //public IInput Inputs { get; set; }
        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public IMover Mover { get; set; }

        #region Collision
        public Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, Atlas.FrameWidth / 2, Atlas.FrameHeight / 2);

        #endregion

        public Sprite(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions)
        {
            Atlas = atlas;
            AtlasManager = Factory.CreateAnimAtlasManager(this, actions);

            Position = new Vector2(0,0);
            Speed = 5f;
            JumpHeight = 10f;

            Mover = Factory.CreateMover(this);
            CollisionHandler = new CollisionHandler(this);
        }

        public Sprite(Texture2D atlasTexture)
        {
            Atlas = Factory.CreateAnimAtlas(atlasTexture, 10, 10);
            //_atlasManager = Factory.CreateAnimAtlasManager(this, actions);

            Position = new Vector2(0, 0);
            Speed = 5f;
            JumpHeight = 10f;

            Mover = Factory.CreateMover(this);
            CollisionHandler = new CollisionHandler(this);

            //MaxHealth = 100;
            //HealthHandler = Factory.CreateHealthHandler(this, Color.Wheat);
        }

        public virtual void Update(GameTime gameTime, MapManager mapManager)
        {
            Mover.Move(gameTime, mapManager);

            AtlasManager.SetAnimation();
            AtlasManager.Update(gameTime);

            //Collision check should happen here
            //CollisionHandler.CheckCollision(mapManager);
            //CollisionHandler.CheckCollisionSprite(mapManager);

            Mover.UpdatePosition();
            Mover.ResetVelocity();

            //HealthReduction test at AtlasManager Attack and mapManager UpdateMap
            //HealthHandler.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (AtlasManager != null)
                AtlasManager.Draw(spriteBatch);
            else throw new Exception("this ni goe");

            //HealthHandler.Draw(spriteBatch);
        }
    }
}
