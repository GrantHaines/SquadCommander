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

		// TEMP
		public static Map.GameMap MainMap;
		public static Console MainConsole;

		public const int WindowWidth = 100;
		public const int WindowHeight = 100;

		const int mapWidth = 60;
		const int mapHeight = 60;

		public static uint GameTime;
		public static bool runConstantly;

		public GameLogic()
		{
			// Setup the engine and create the main window.
			SadConsole.Game.Create("Assets/12x12.font", mapWidth, mapHeight);

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

			// Initialize the map object
			MainMap = new GameMap(mapWidth, mapHeight);
			MapGenerator.BasicRoomHallTerrain(MainMap);

			// Initialize the main console
			MainConsole = new Console(mapWidth, mapHeight, MainMap.GetCells());
			MainConsole.Components.Add(new MapMouseControlComponent());
			MainConsole.Children.Add(ControlSystem.SelectionBox);

			Random rand = new Random();

			/*Entities = new List<GameEntity>();
			for (int i = 0; i < 25; i++)
			{
				int x, y;
				do
				{
					x = rand.Next(0, MainMap.Width);
					y = rand.Next(0, MainMap.Height);
				} while (!MainMap.TileIsWalkable(x, y));

				Color color = new Color(rand.Next(100, 256), rand.Next(100, 256), rand.Next(100, 256));
				GameEntity player = new GameEntity(color, Color.Black, '@', new GoRogue.Coord(x, y));
				player.Name = $"Actor {i}";
				player.AddGameComponent(new AIComponent());
				player.AddGameComponent(new HealthComponent(10));
				player.AddGameComponent(new EnergyComponent(rand.Next(1, 10)));
				MainMap.AddActor(player);
				Entities.Add(player);
			}*/

			// Set main console as the current screen
			SadConsole.Global.CurrentScreen = MainConsole;

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
				MainConsole.Clear(new Rectangle(1, 1, elapsedTime.Length, 1));
				MainConsole.Clear(new Rectangle(1, 2, fps.Length, 1));
				MainConsole.Print(1, 1, elapsedTime);
				MainConsole.Print(1, 2, fps);
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
