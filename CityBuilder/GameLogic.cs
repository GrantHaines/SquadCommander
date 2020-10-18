using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Console = SadConsole.Console;

using CityBuilder.Map;
using CityBuilder.Entities;
using CityBuilder.Actions;
using CityBuilder.Systems;
using System.Reflection;

namespace CityBuilder
{
	class GameLogic
	{
		// Current program state
		// 0 = Gameplay map
		//public int GameState;

		// List of every entity in the game
		public List<Entity> Entities;

		// TEMP
		public Map.Map MainMap;
		public Console MainConsole;

		const int mapWidth = 50;
		const int mapHeight = 50;

		public static uint GameTime;

		// ID Generator
		public static GoRogue.IDGenerator EntityIDGenerator;

		public GameLogic()
		{
			// Setup the engine and create the main window.
			SadConsole.Game.Create("Assets/12x12.font", mapWidth, mapHeight);

			// Hook the start event so we can add consoles to the system.
			SadConsole.Game.OnInitialize = InitializeGame;
			SadConsole.Game.OnUpdate = RunGame;

			// Start the game.
			SadConsole.Game.Instance.Run();
			SadConsole.Game.Instance.Dispose();
		}

		public void InitializeGame()
		{
			// Initialize id generator
			EntityIDGenerator = new GoRogue.IDGenerator();

			// Initialize default map terrain
			GoRogue.MapViews.ArrayMap2D<Terrain> arraymap = new GoRogue.MapViews.ArrayMap2D<Terrain>(mapWidth, mapHeight);
			for (int x = 0; x < mapWidth; x++)
			{
				for (int y = 0; y < mapHeight; y++)
				{
					if (y == mapHeight / 2 && x > 5)
					{
						arraymap[x, y] = new SimpleWall(new GoRogue.Coord(x, y));
					}
					else if (y == mapHeight / 4 && x < mapWidth - 5)
					{
						arraymap[x, y] = new SimpleWall(new GoRogue.Coord(x, y));
					}
					else if (y == (mapHeight / 4) + (mapHeight / 2) && x < mapWidth - 5)
					{
						arraymap[x, y] = new SimpleWall(new GoRogue.Coord(x, y));
					}
					else
					{
						arraymap[x, y] = new SimpleFloor(new GoRogue.Coord(x, y));
					}
				}
			}

			// Initialize the map object
			MainMap = new Map.Map(mapWidth, mapHeight, 1, GoRogue.Distance.EUCLIDEAN);
			MainMap.ApplyTerrainOverlay(arraymap);

			// Initialize the main console
			MainConsole = new Console(mapWidth, 25);
			MainMap.ConfigureAsRenderer(MainConsole);

			Random rand = new Random();

			Entities = new List<Entity>();
			for (int i = 0; i < 20; i++)
			{
				int x = rand.Next(0, MainMap.Width);
				int y = rand.Next(0, MainMap.Height);
				while (!MainMap.WalkabilityView[x, y])
				{
					x = rand.Next(0, MainMap.Width);
					y = rand.Next(0, MainMap.Height);
				}
				Color color = new Color(rand.Next(100, 256), rand.Next(100, 256), rand.Next(100, 256));
				Entity player = new Entity(new GoRogue.Coord(x, y), 1, false, true, color, Color.Black, '@');
				player.AddGoRogueComponent(new AIComponent());
				player.AddGoRogueComponent(new HealthComponent(10));
				MainMap.AddEntity(player);
				Entities.Add(player);
			}

			// Set main console as the current screen
			SadConsole.Global.CurrentScreen = MainConsole;

			GameTime = 0;
		}

		public void RunGame(GameTime time)
		{
			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
			{
				GameTime++;

				MovementSystem.ProcessTurn(Entities);
				
				MainConsole.Print(1, 1, $"{GameTime}");
			}

			if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
			{
				SadConsole.Game.Instance.Exit();
			}
		}
	}
}
