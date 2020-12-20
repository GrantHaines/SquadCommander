using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole.Components;
using SadConsole.Input;
using SquadCommander.GameScreens;

namespace SquadCommander.Controls
{
	class MenuKeyboardControlComponent : KeyboardConsoleComponent
	{
		private MenuScreen Parent;

		public MenuKeyboardControlComponent(MenuScreen screen)
		{
			Parent = screen;
		}

		public override void ProcessKeyboard(SadConsole.Console console, Keyboard info, out bool handled)
		{
			// Exit
			if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
			{
				SadConsole.Game.Instance.Exit();
			}

			// Keyboard control of menu
			if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
			{
				Parent.MenuUp();
			}
			else if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
			{
				Parent.MenuDown();
			}

			if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
			{
				Parent.MenuSelect();
			}

			handled = true;
		}
	}
}
