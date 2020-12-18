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
	class GameLogic
	{
		// List of every entity in the game
		public List<GameEntity> Entities;

		// Game screens
		public static MenuScreen MenuScreen;
		public static GameMapScreen GameMapScreen;

		public const int WindowWidth = 80;
		public const int WindowHeight = 60;

		public const int mapWidth = 60;
		public const int mapHeight = 60;

		public static uint GameTime;
		public static bool runConstantly;

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

			GameTime = 0;
			runConstantly = false;
		}

		public void RunGame(GameTime time)
		{
			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space) || runConstantly)
			{
				GameTime++;

				MovementSystem.ProcessTurn(Entities);

				// Print game time and FPS
				String elapsedTime = $"Time: {GameTime}";
				String fps = $"FPS: {Math.Round(1f / time.ElapsedGameTime.TotalSeconds)}";
				GameMapScreen.MainConsole.Clear(new Rectangle(1, 1, elapsedTime.Length, 1));
				GameMapScreen.MainConsole.Clear(new Rectangle(1, 2, fps.Length, 1));
				GameMapScreen.MainConsole.Print(1, 1, elapsedTime);
				GameMapScreen.MainConsole.Print(1, 2, fps);
			}

			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
			{
				MenuScreen.SetAsCurrentScreen();
			}

			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
			{
				SadConsole.Game.Instance.Exit();
			}

			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
			{
				if (runConstantly)
				{
					runConstantly = false;
				}
				else
				{
					runConstantly = true;
				}
			}
		}
	}
}
