using GoRogue;
using SquadCommander.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.Map
{
	public class GameMap
	{
		// Terrain tiles
		public Terrain[] TerrainTiles;
		// Entity map - possibly replace with layered spatial map at some point
		public SpatialMap<GameEntity> MapActors;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public GameMap(int width, int height)
		{
			TerrainTiles = new Terrain[width * height];
			Width = width;
			Height = height;
		}

		public void TestMapTerrain(int mapWidth, int mapHeight)
		{
			// Initialize default map terrain
			GoRogue.MapViews.ArrayMap<Terrain> arraymap = new GoRogue.MapViews.ArrayMap<Terrain>(mapWidth, mapHeight);
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
			//ApplyTerrainOverlay(arraymap);
			TerrainTiles = arraymap;
		}

		public void AddActor(GameEntity entity)
		{
			MapActors.Add(entity, entity.Position);
		}

		public GameEntity GetActor(GoRogue.Coord position)
		{
			return MapActors.GetItem(position);
		}

		public GoRogue.MapViews.ArrayMap2D<bool> GetWalkabilityMap()
		{
			GoRogue.MapViews.ArrayMap2D<bool> walkabilityMap = new GoRogue.MapViews.ArrayMap2D<bool>(Width, Height);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (TerrainTiles[Width * x + y].IsWalkable)
					{
						walkabilityMap[x, y] = true;
					}
					else
					{
						walkabilityMap[x, y] = false;
					}
				}
			}

			return walkabilityMap;
		}

		public bool TileIsWalkable(int x, int y)
		{
			return TerrainTiles[Width * x + y].IsWalkable;
		}
	}
}
