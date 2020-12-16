using GoRogue;
using GoRogue.MapViews;
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
		public Terrain[,] TerrainTiles;
		// Entity map - possibly replace with layered spatial map at some point
		public SpatialMap<GameEntity> MapActors;

		public List<Room> Rooms;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public GameMap(int width, int height)
		{
			TerrainTiles = new Terrain[width, height];
			Width = width;
			Height = height;

			MapActors = new SpatialMap<GameEntity>();

			Rooms = new List<Room>();
		}

		public void AddActor(GameEntity entity)
		{
			MapActors.Add(entity, entity.Position);
		}

		public GameEntity GetActor(GoRogue.Coord position)
		{
			return MapActors.GetItem(position);
		}

		public ArrayMap2D<bool> GetWalkabilityMap()
		{
			ArrayMap2D<bool> walkabilityMap = new ArrayMap2D<bool>(Width, Height);

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					walkabilityMap[x, y] = TerrainTiles[x, y].IsWalkable;
				}
			}

			return walkabilityMap;
		}

		public bool TileIsWalkable(int x, int y)
		{
			return TerrainTiles[x, y].IsWalkable;
		}

		public ArrayMap<Terrain> GetCells()
		{
			ArrayMap<Terrain> arraymap = new ArrayMap<Terrain>(Width, Height);

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					arraymap[x, y] = TerrainTiles[x, y];

			return arraymap;
		}
	}
}
