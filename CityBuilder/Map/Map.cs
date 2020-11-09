using GoRogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Map
{
	public class Map : SadConsole.BasicMap
	{
		public Map(int width, int height, int numberOfEntityLayers, Distance distanceMeasurement) : base(width, height, numberOfEntityLayers, distanceMeasurement)
		{

		}

		public void TestMapTerrain(int mapWidth, int mapHeight)
		{
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
			ApplyTerrainOverlay(arraymap);
		}
	}
}
