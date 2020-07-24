using GundamSD.Animations;
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
        public AnimationAtlas Atlas { get; set; }
        private AnimationAtlas _atlas;
        protected AnimationAtlasManager _atlasManager;
        //protected AnimationAtlasPlayer _atlasPlayer;
        //protected Dictionary<string, AnimationAtlasAction> _actions;

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

        //public Vector2 Velocity;
        private Vector2 _velocity;

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public IMover Mover;

        public Sprite(AnimationAtlas atlas, Dictionary<string, IAnimationAtlasAction> actions, /*bool isPlayer,*/ Vector2 spawnPoint)
        {
            Atlas = atlas;
            //_actions = actions;
            //_atlasPlayer = new AnimationAtlasPlayer(atlas, _actions.First().Value);
            _atlasManager = new AnimationAtlasManager(this, actions);

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

        //protected void SetAnimation()
        //{
        //    if (Keyboard.GetState().IsKeyDown(Inputs.Attack))
        //        _atlasPlayer.Play(_actions["Attack"]);
        //    else if (_velocity.X > 0)
        //        _atlasPlayer.Play(_actions["WalkRight"]);
        //    else if (_velocity.X < 0)
        //        _atlasPlayer.Play(_actions["WalkRight"]);
        //    else if (Velocity.Y > 0)
        //        _atlasPlayer.Play(_actions["WalkRight"]);
        //    else if (Velocity.Y < 0)
        //        _atlasPlayer.Play(_actions["Jump"]);

        //    else _atlasPlayer.Stop();
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_atlasManager != null)
                _atlasManager.Draw(spriteBatch);
            else throw new Exception("this ni goe");
        }
    }

}
