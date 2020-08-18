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

        public virtual void SetAnimation()
        {
            IHasInput hasInput = _sprite as IHasInput;

            
            if (hasInput != null && Keyboard.GetState().IsKeyDown(hasInput.Inputs.Melee))
            {
                AtlasPlayer.Play(_actions["Melee"]);

                //if (_sprite is IHasHealth hasHealth)
                //{
                //    hasHealth.HealthHandler.TakeDamage(20);
                //}
            }
            else if (hasInput != null && Keyboard.GetState().IsKeyDown(hasInput.Inputs.Ranged))
            {
                AtlasPlayer.Play(_actions["Ranged"]);
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
            else if (_sprite.Mover.Velocity.X == 0 && _sprite.Mover.Velocity.Y == 0)
            {
                AtlasPlayer.Play(_actions.First().Value);
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
