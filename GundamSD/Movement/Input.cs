using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Movement
{
    public class Input : IInput
    {
        private KeyboardState currentKeyState;
        private KeyboardState previousKeyState;

        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Attack { get; set; }
        public Keys Jump { get; set; }

        public Input()
        {
            Up = Keys.Z;
            Down = Keys.S;
            Left = Keys.Q;
            Right = Keys.D;
            Attack = Keys.E;
            Jump = Keys.Space;
        }

        public KeyboardState GetKeyboardState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public bool KeyIsHoldDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public bool KeyIsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}
