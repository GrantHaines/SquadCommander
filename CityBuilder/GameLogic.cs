using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Console = SadConsole.Console;

using CityBuilder.Map;
using CityBuilder.Entities;

namespace CityBuilder
{
    class GameLogic
    {
        public Map.Map MainMap;
        public Console MainConsole;

        // ID Generator
        public static GoRogue.IDGenerator EntityIDGenerator;

        public GameLogic()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(80, 25);

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
            GoRogue.MapViews.ArrayMap2D<Terrain> arraymap = new GoRogue.MapViews.ArrayMap2D<Terrain>(80, 25);
            for (int x = 0; x < 80; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    arraymap[x, y] = new SimpleFloor(new GoRogue.Coord(x, y));
                }
            }

            // Initialize the map object
            MainMap = new Map.Map(80, 25, 1, GoRogue.Distance.EUCLIDEAN);
            MainMap.ApplyTerrainOverlay(arraymap);

            // Initialize the main console
            MainConsole = new Console(80, 25);
            MainMap.ConfigureAsRenderer(MainConsole);

            Entity player = new Entity(new GoRogue.Coord(40, 12), 1, false, true, Color.LightBlue, Color.Black, '@');
            player.AddGoRogueComponent(new Component());
            MainMap.AddEntity(player);

            // Set main console as the current screen
            SadConsole.Global.CurrentScreen = MainConsole;
        }

        public void RunGame(GameTime time)
        {

        }
    }
}
