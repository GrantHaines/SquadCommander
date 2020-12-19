using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Console = SadConsole.Console;

using SquadCommander.Map;
using SquadCommander.Entities;
using SquadCommander.Actions;
using SquadCommander.Systems;
using System.Reflection;
using SquadCommander.Controls;
using SquadCommander.GameScreens;

namespace SquadCommander
{
	enum GameState
	{
		MAIN_MENU,
		GAME_MAP
	}

	class GameLogic
	{
		// Game screens
		public static MenuScreen MenuScreen;
		public static GameMapScreen GameMapScreen;

		// Game state
		public static GameState CurrentGameState;

		public const int WindowWidth = 80;
		public const int WindowHeight = 60;

		public const int mapWidth = 60;
		public const int mapHeight = 60;

		public GameLogic()
		{
			// Setup the engine and create the main window.
			SadConsole.Game.Create("Assets/12x12.font", WindowWidth, WindowHeight);

			// Hook the start event so we can add consoles to the system.
			SadConsole.Game.OnInitialize = InitializeGame;
			SadConsole.Game.OnUpdate = RunGame;

			// Set FPS limit
			SadConsole.Game.Instance.IsFixedTimeStep = true;
			SadConsole.Game.Instance.TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0f);

			// Start the game.
			SadConsole.Game.Instance.Run();
			SadConsole.Game.Instance.Dispose();
		}

		public void InitializeGame()
		{
			// Create main menu
			MenuScreen = new MenuScreen();
			GameMapScreen = new GameMapScreen();

			Random rand = new Random();

			MenuScreen.SetAsCurrentScreen();
		}

		public void RunGame(GameTime time)
		{
			switch (CurrentGameState)
			{
				case GameState.MAIN_MENU:
					break;
				case GameState.GAME_MAP:
					GameMapScreen.GameMapLoop(time);
					break;
			}
		}
	}
}
