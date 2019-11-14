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
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }

        public Input()
        {
            Up = Keys.Z;
            Down = Keys.S;
            Left = Keys.Q;
            Right = Keys.D;
        }
    }
}
