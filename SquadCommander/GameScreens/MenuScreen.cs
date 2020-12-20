using System;
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
		private ControlsConsole menuControls;

		private int currentButton;
		private List<SadConsole.Controls.ButtonBase> buttons;

		public MenuScreen()
		{
			// Initialize the main console
			menuConsole = new SadConsole.Console(GameLogic.WindowWidth, GameLogic.WindowHeight);
			menuConsole.Components.Add(new Controls.MenuKeyboardControlComponent(this));
			menuConsole.IsFocused = true;

			// Create control console
			menuControls = new ControlsConsole(20, 30);
			menuControls.Position = new Microsoft.Xna.Framework.Point(GameLogic.WindowWidth / 2 - 10, GameLogic.WindowHeight - 41);

			buttons = new List<SadConsole.Controls.ButtonBase>();

			UpdateMenuList();

			// Add control console to the main console
			menuConsole.Children.Add(menuControls);
		}

		public void UpdateMenuList()
		{
			menuControls.RemoveAll();

			int buttonPosition = 1;
			currentButton = -1;
			buttons.Clear();

			// Continue game button
			if (GameLogic.GameMapScreen != null)
			{
				SadConsole.Controls.Button continueGameButton = new SadConsole.Controls.Button(16);
				continueGameButton.Text = "Continue Game";
				continueGameButton.Position = new Microsoft.Xna.Framework.Point(2, buttonPosition);
				buttonPosition += 2;
				continueGameButton.Click += ContinueGameButtonClicked;
				menuControls.Add(continueGameButton);
				buttons.Add(continueGameButton);
			}

			// New game button
			SadConsole.Controls.Button newGameButton = new SadConsole.Controls.Button(16);
			newGameButton.Text = "New Game";
			newGameButton.Position = new Microsoft.Xna.Framework.Point(2, buttonPosition);
			buttonPosition += 2;
			newGameButton.Click += NewGameButtonClicked;
			menuControls.Add(newGameButton);
			buttons.Add(newGameButton);

			// Exit game button
			SadConsole.Controls.Button exitGameButton = new SadConsole.Controls.Button(16);
			exitGameButton.Text = "Exit";
			exitGameButton.Position = new Microsoft.Xna.Framework.Point(2, buttonPosition);
			buttonPosition += 2;
			exitGameButton.Click += ExitGameButtonClicked;
			menuControls.Add(exitGameButton);
			buttons.Add(exitGameButton);
		}

		public void SetAsCurrentScreen()
		{
			SadConsole.Global.CurrentScreen = menuConsole;
			menuConsole.IsFocused = true;
		}

		private void ContinueGameButtonClicked(object sender, EventArgs e)
		{
			GameLogic.SwitchGameState(GameState.GAME_MAP);
		}

		private void NewGameButtonClicked(object sender, EventArgs e)
		{
			GameLogic.GameMapScreen = new GameMapScreen();
			GameLogic.SwitchGameState(GameState.GAME_MAP);
		}

		public void MenuUp()
		{
			if (currentButton == -1)
			{
				currentButton = buttons.Count - 1;
				buttons[currentButton].IsFocused = true;
			}
			else
			{
				buttons[currentButton].IsFocused = false;
				currentButton -= 1;
				if (currentButton < 0)
					currentButton = buttons.Count - 1;
				buttons[currentButton].IsFocused = true;
			}
		}

		public void MenuDown()
		{
			if (currentButton == -1)
			{
				currentButton = 0;
				buttons[currentButton].IsFocused = true;
			}
			else
			{
				buttons[currentButton].IsFocused = false;
				currentButton += 1;
				if (currentButton >= buttons.Count)
					currentButton = 0;
				buttons[currentButton].IsFocused = true;
			}
		}

		public void MenuSelect()
		{
			// Select current option
			buttons[currentButton].DoClick();
		}

		private void ExitGameButtonClicked(object sender, EventArgs e)
		{
			SadConsole.Game.Instance.Exit();
		}
	}
}
