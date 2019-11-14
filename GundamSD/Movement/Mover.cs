using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GundamSD.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GundamSD.Movement
{
    public class Mover : IMover
    {
        //private IInput _inputs;
        private ISprite _sprite;

        public Mover(ISprite sprite)
        {
            //_inputs = inputs;
            _sprite = sprite;
        }
        public void Move()
        {
            if (_sprite.Inputs == null)
            {
                return;
            }

            //_sprite.Position = new Vector2(0, 2);

            if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Up))
                _sprite.Velocity = new Vector2(0 , -_sprite.Speed);
            else if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Down))
                _sprite.Velocity = new Vector2(0 , _sprite.Speed);
            else if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Left))
                _sprite.Velocity = new Vector2(-_sprite.Speed , 0);
            else if (Keyboard.GetState().IsKeyDown(_sprite.Inputs.Right))
                _sprite.Velocity = new Vector2(_sprite.Speed , 0);
        }

        public void UpdatePosition()
        {
            _sprite.Position += _sprite.Velocity;
        }

        public void ResetVelocity()
        {
            _sprite.Velocity = Vector2.Zero;
        }
    }
}
