using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.GameScreens
{
	public class MenuScreen
	{
		private SadConsole.Console menuConsole;

		public MenuScreen()
		{
			menuConsole = new SadConsole.Console(GameLogic.WindowWidth, GameLogic.WindowHeight);

			menuConsole.FillWithRandomGarbage();
		}

		public void SetAsCurrentScreen()
		{
			SadConsole.Global.CurrentScreen = menuConsole;
		}
	}
}
