using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GundamSD.Models
{
    public class Player : IPlayer
    {
        public ISprite PlayerSprite { get; set; }

        public Player(ISprite sprite)
        {
            PlayerSprite = sprite;
            PlayerSprite.Inputs = Factory.CreateInput();
        }
    }
}
