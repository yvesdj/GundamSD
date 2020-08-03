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
        protected IAnimationAtlasManager _atlasManager;
        public CollisionHandler CollisionHandler { get; set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_atlasManager != null)
                {
                    _atlasManager.AtlasPlayer.Position = _position;
                }
            }
        }

        public IInput Inputs { get; set; }
        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public IMover Mover { get; set; }

        #region Collision
        public Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, Atlas.FrameWidth / 2, Atlas.FrameHeight / 2);

        #endregion

        public Sprite(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions)
        {
            Atlas = atlas;
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);

            Position = new Vector2(0,0);
            Speed = 3f;
            Mover = Factory.CreateMover(this);
            CollisionHandler = new CollisionHandler(this);
        }

        public Sprite(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions, /*bool isPlayer,*/ Vector2 spawnPoint)
        {
            Atlas = atlas;
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);

            Position = spawnPoint;
            Speed = 3f;
            Mover = Factory.CreateMover(this);
            CollisionHandler = new CollisionHandler(this);
        }

        public void Update(GameTime gameTime, MapManager mapManager)
        {
            Mover.Move(gameTime, mapManager);

            _atlasManager.SetAnimation();
            _atlasManager.Update(gameTime);

            //Collision check should happen here
            //CollisionHandler.CheckCollision(mapManager);

            Mover.UpdatePosition();
            Mover.ResetVelocity();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_atlasManager != null)
                _atlasManager.Draw(spriteBatch);
            else throw new Exception("this ni goe");
        }
    }
}
