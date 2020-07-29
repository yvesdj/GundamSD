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
        public Keys Jump { get; set; }
        public Keys Down { get; set; }
        public Keys Attack { get; set; }

        public Input()
        {
            Jump = Keys.Space;
            Down = Keys.S;
            Left = Keys.A;
            Right = Keys.D;
            Attack = Keys.E;
        }
    }
}
