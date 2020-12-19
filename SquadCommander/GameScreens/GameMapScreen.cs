using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SadConsole;
using SquadCommander.Controls;
using SquadCommander.Map;
using SquadCommander.Systems;

namespace SquadCommander.GameScreens
{
	class GameMapScreen
	{
		public GameMap CurrentMap;
		public SadConsole.Console MainConsole;

		public static uint GameTime;
		public static bool GamePaused;

		public GameMapScreen()
		{
			MainConsole = new SadConsole.Console(GameLogic.mapWidth, GameLogic.mapHeight);

			// Initialize a new map object with basic terrain
			CurrentMap = new GameMap(GameLogic.mapWidth, GameLogic.mapHeight);
			MapGenerator.BasicRoomHallTerrain(CurrentMap);

			// Initialize the main console
			MainConsole = new SadConsole.Console(GameLogic.mapWidth, GameLogic.mapHeight, CurrentMap.GetCells());
			MainConsole.Components.Add(new MapMouseControlComponent());
			MainConsole.Children.Add(ControlSystem.SelectionBox);

			// Initialize variables
			GamePaused = true;
			GameTime = 0;
		}

		public void SetAsCurrentScreen()
		{
			SadConsole.Global.CurrentScreen = MainConsole;
		}

		public void GameMapLoop(GameTime time)
		{
			if (!GamePaused)
			{
				ProcessTurn(time);
			}
		}

		private void ProcessTurn(GameTime time)
		{
			GameTime++;

			//MovementSystem.ProcessTurn(Entities);

			// Print game time and FPS
			String elapsedTime = $"Time: {GameTime}";
			//String fps = $"FPS: {Math.Round(1f / time.ElapsedGameTime.TotalSeconds)}";
			MainConsole.Clear(new Rectangle(1, 1, elapsedTime.Length, 1));
			//MainConsole.Clear(new Rectangle(1, 2, fps.Length, 1));
			MainConsole.Print(1, 1, elapsedTime);
			//MainConsole.Print(1, 2, fps);
		}
	}
}
