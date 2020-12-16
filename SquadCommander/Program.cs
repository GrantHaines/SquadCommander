/*
 * 
 * Author: Grant Haines
 * Date Created: October 5th, 2020
 * 
 * Description: Main program class for the Squad Commander project.
 * 
 */

using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

using SquadCommander.Map;

namespace SquadCommander
{
    class Program
    {
        // Main
        static void Main()
        {
            GameLogic game = new GameLogic();
        }
    }
}