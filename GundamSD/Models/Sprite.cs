﻿using GundamSD.Animations;
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

        public Vector2 Velocity { get; set; }

        public IMover Mover;

        #region Collision

        public Rectangle CollisionBox => new Rectangle((int)Position.X, (int)Position.Y, Atlas.FrameWidth, Atlas.FrameHeight);

        #endregion

        public Sprite(IAnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions, /*bool isPlayer,*/ Vector2 spawnPoint)
        {
            Atlas = atlas;
            _atlasManager = Factory.CreateAnimAtlasManager(this, actions);

            Position = spawnPoint;
            Speed = 3f;
            Mover = Factory.CreateMover(this);
            //if (isPlayer)
            //{
            //    Inputs = Factory.CreateInput();
            //}
        }

        public void Update(GameTime gameTime, List<ISprite> sprites)
        {
            Mover.Move();
            
            //methode was uitgecommenteerd
            _atlasManager.SetAnimation();
            _atlasManager.Update(gameTime);

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
