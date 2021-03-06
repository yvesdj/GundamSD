﻿using Microsoft.Xna.Framework.Input;

namespace GundamSD.Movement
{
    public interface IInput
    {
        Keys Down { get; set; }
        Keys Left { get; set; }
        Keys Right { get; set; }
        Keys Up { get; set; }
        Keys Melee { get; set; }
        Keys Jump { get; set; }
        Keys Ranged { get; set; }
        Keys Block { get; set; }

        KeyboardState GetKeyboardState();
        bool KeyIsHoldDown(Keys key);
        bool KeyIsPressed(Keys key);
    }
}