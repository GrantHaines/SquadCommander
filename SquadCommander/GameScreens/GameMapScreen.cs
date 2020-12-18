using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SquadCommander.Controls;
using SquadCommander.Map;
using SquadCommander.Systems;

namespace SquadCommander.GameScreens
{
	class GameMapScreen
	{
		public GameMap MainMap;
		public SadConsole.Console MainConsole;

		public GameMapScreen()
		{
			MainConsole = new SadConsole.Console(GameLogic.mapWidth, GameLogic.mapHeight);

			// Initialize the map object
			MainMap = new GameMap(GameLogic.mapWidth, GameLogic.mapHeight);
			MapGenerator.BasicRoomHallTerrain(MainMap);

			// Initialize the main console
			MainConsole = new SadConsole.Console(GameLogic.mapWidth, GameLogic.mapHeight, MainMap.GetCells());
			MainConsole.Components.Add(new MapMouseControlComponent());
			MainConsole.Children.Add(ControlSystem.SelectionBox);
		}

		public void SetAsCurrentScreen()
		{
			SadConsole.Global.CurrentScreen = MainConsole;
		}
	}
}
