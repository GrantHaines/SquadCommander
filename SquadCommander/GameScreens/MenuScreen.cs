﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SadConsole;

namespace SquadCommander.GameScreens
{
	public class MenuScreen
	{
		private SadConsole.Console menuConsole;

		public MenuScreen()
		{
			menuConsole = new SadConsole.Console(GameLogic.WindowWidth, GameLogic.WindowHeight);

			//menuConsole.FillWithRandomGarbage();

			ControlsConsole menuControls = new ControlsConsole(20, 30);
			menuControls.Position = new Microsoft.Xna.Framework.Point(GameLogic.WindowWidth / 2 - 10, GameLogic.WindowHeight - 41);

			SadConsole.Controls.Button continueGameButton = new SadConsole.Controls.Button(16);
			continueGameButton.Text = "Continue Game";
			continueGameButton.Position = new Microsoft.Xna.Framework.Point(2, 1);
			continueGameButton.Click += ContinueGameButtonClicked;

			SadConsole.Controls.Button newGameButton = new SadConsole.Controls.Button(16);
			newGameButton.Text = "New Game";
			newGameButton.Position = new Microsoft.Xna.Framework.Point(2, 3);
			newGameButton.Click += NewGameButtonClicked;

			SadConsole.Controls.Button exitGameButton = new SadConsole.Controls.Button(16);
			exitGameButton.Text = "Exit";
			exitGameButton.Position = new Microsoft.Xna.Framework.Point(2, 5);
			exitGameButton.Click += ExitGameButtonClicked;

			menuControls.Add(newGameButton);
			menuControls.Add(continueGameButton);
			menuControls.Add(exitGameButton);
			menuConsole.Children.Add(menuControls);
		}

		public void SetAsCurrentScreen()
		{
			SadConsole.Global.CurrentScreen = menuConsole;
		}

		private void ContinueGameButtonClicked(object sender, EventArgs e)
		{
			GameLogic.GameMapScreen.SetAsCurrentScreen();
		}

		private void NewGameButtonClicked(object sender, EventArgs e)
		{
			GameLogic.GameMapScreen = new GameMapScreen();
			GameLogic.GameMapScreen.SetAsCurrentScreen();
		}

		private void ExitGameButtonClicked(object sender, EventArgs e)
		{
			SadConsole.Game.Instance.Exit();
		}
	}
}
