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
    public class AnimationAtlasManager
    {
        private ISprite _sprite;
        public AnimationAtlasPlayer atlasPlayer;
        protected Dictionary<string, AnimationAtlasAction> _actions;

        public AnimationAtlasManager(ISprite sprite, Dictionary<string, AnimationAtlasAction> actions)
        {
            _sprite = sprite;
            _actions = actions;
            atlasPlayer = new AnimationAtlasPlayer(_sprite.Atlas, _actions.First().Value);
        }

        public void SetAnimation()
        {
            if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Attack))
                atlasPlayer.Play(_actions["Attack"]);
            else if (_sprite.Velocity.X > 0)
                atlasPlayer.Play(_actions["WalkRight"]);
            else if (_sprite.Velocity.X < 0)
                atlasPlayer.Play(_actions["WalkRight"]);
            else if (_sprite.Velocity.Y > 0)
                atlasPlayer.Play(_actions["WalkRight"]);
            else if (_sprite.Velocity.Y < 0)
                atlasPlayer.Play(_actions["Jump"]);

            else atlasPlayer.Stop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            atlasPlayer.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            atlasPlayer.Update(gameTime);
        }
    }
}
