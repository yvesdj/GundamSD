using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Animations
{
    public class AnimationAtlasManager : IAnimationAtlasManager
    {
        private ISprite _sprite;
        public IAnimationAtlasPlayer AtlasPlayer { get; set; }
        //public AnimationAtlasPlayer AtlasPlayer;
        protected Dictionary<string, IAnimationAtlasAction> _actions;

        public AnimationAtlasManager(ISprite sprite, Dictionary<string, IAnimationAtlasAction> actions)
        {
            _sprite = sprite;
            _actions = actions;
            AtlasPlayer = Factory.CreateAnimAtlasPlayer(_sprite.Atlas, _actions.First().Value);
        }

        public void SetAnimation()
        {
            if (_sprite.Inputs == null)
            {
                AtlasPlayer.Stop();
                return;
            }

            if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Attack))
            {
                AtlasPlayer.Play(_actions["Attack"]);

                _sprite.HealthHandler.TakeDamage(10);
            }
            else if (_sprite.Mover.Velocity.Y < 0)
                AtlasPlayer.Play(_actions["Jump"]);
            else if (_sprite.Mover.Velocity.X > 0 || _sprite.Mover.Velocity.X < 0)
            {
                if (_sprite.Mover.Velocity.Y < 0)
                    AtlasPlayer.Play(_actions["Jump"]);
                else
                    AtlasPlayer.Play(_actions["WalkRight"]);
            }
            else
            {
                AtlasPlayer.Play(_actions["WalkRight"]);
                AtlasPlayer.Stop();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AtlasPlayer.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            AtlasPlayer.Update(gameTime);
        }
    }
}
