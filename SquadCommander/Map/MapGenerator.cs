using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GoRogue.MapViews;

namespace SquadCommander.Map
{
	static class MapGenerator
	{
		static private Random rand = new Random();

		static public void TestMapTerrain(GameMap map)
		{
			// Initialize default map terrain
			ArrayMap2D<Terrain> arraymap = new ArrayMap2D<Terrain>(map.Width, map.Height);
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					if (y == map.Height / 2 && x > 5)
					{
						arraymap[x, y] = new SimpleWall();
					}
					else if (y == map.Height / 4 && x < map.Width - 5)
					{
						arraymap[x, y] = new SimpleWall();
					}
					else if (y == (map.Height / 4) + (map.Height / 2) && x < map.Width - 5)
					{
						arraymap[x, y] = new SimpleWall();
					}
					else
					{
						arraymap[x, y] = new SimpleFloor();
					}
				}
			}

			// Initialize the map object
			//ApplyTerrainOverlay(arraymap);
			map.TerrainTiles = arraymap;
		}

		static public void BasicRoomHallTerrain(GameMap map)
		{
			ArrayMap2D<Terrain> arraymap = new ArrayMap2D<Terrain>(map.Width, map.Height);
			// Fill all terrain with void to start
			for (int x = 0; x < map.Width; x++)
				for (int y = 0; y < map.Height; y++)
					arraymap[x, y] = new SimpleVoid();

			// Generate some number of rooms
			List<Room> rooms = new List<Room>();
			Room firstRoom = new Room();
			firstRoom.Position = new GoRogue.Coord(map.Width / 2 - 4, map.Height / 2 - 4);
			firstRoom.Width = 9;
			firstRoom.Height = 9;
			
			AddRoomToArrayMap(arraymap, firstRoom);

			GenerateRoom(arraymap, firstRoom, 0, 50);
			GenerateRoom(arraymap, firstRoom, 1, 50);
			GenerateRoom(arraymap, firstRoom, 2, 50);
			GenerateRoom(arraymap, firstRoom, 3, 50);

			map.TerrainTiles = arraymap;
		}

		static private void GenerateRoom(ArrayMap2D<Terrain> arraymap, Room parent, int direction, int newRoomChance)
		{
			if (newRoomChance <= 0)
			{
				return;
			}

			int width = rand.Next(4, 15);
			int height = rand.Next(4, 15);
			GoRogue.Coord position;
			GoRogue.Coord parentWallCenter;
			Room newroom;

			bool roomCreated = false;

			switch (direction)
			{
				case 0: // Left
					parentWallCenter = (parent.Position.X - 1, parent.Position.Y + parent.Height / 2);
					position = (parentWallCenter.X - width, parentWallCenter.Y - height / 2);
					newroom = new Room() { Height = height, Width = width, Position = position };
					if (ValidRoomLocation(arraymap, newroom))
					{
						newroom.Neighbors.Add(parent);
						parent.Neighbors.Add(newroom);
						AddRoomToArrayMap(arraymap, newroom);
						roomCreated = true;
					}
					break;
				case 1: // Up
					parentWallCenter = (parent.Position.X + parent.Width / 2, parent.Position.Y - 1);
					position = (parentWallCenter.X - width / 2, parentWallCenter.Y - height);
					newroom = new Room() { Height = height, Width = width, Position = position };
					if (ValidRoomLocation(arraymap, newroom))
					{
						newroom.Neighbors.Add(parent);
						parent.Neighbors.Add(newroom);
						AddRoomToArrayMap(arraymap, newroom);
						roomCreated = true;
					}
					break;
				case 2: // Right
					parentWallCenter = (parent.Position.X + parent.Width, parent.Position.Y + parent.Height / 2);
					position = (parentWallCenter.X + 1, parentWallCenter.Y - height / 2);
					newroom = new Room() { Height = height, Width = width, Position = position };
					if (ValidRoomLocation(arraymap, newroom))
					{
						newroom.Neighbors.Add(parent);
						parent.Neighbors.Add(newroom);
						AddRoomToArrayMap(arraymap, newroom);
						roomCreated = true;
					}
					break;
				case 3: // Down
					parentWallCenter = (parent.Position.X + parent.Width / 2, parent.Position.Y + parent.Height);
					position = (parentWallCenter.X - width / 2, parentWallCenter.Y + 1);
					newroom = new Room() { Height = height, Width = width, Position = position };
					if (ValidRoomLocation(arraymap, newroom))
					{
						newroom.Neighbors.Add(parent);
						parent.Neighbors.Add(newroom);
						AddRoomToArrayMap(arraymap, newroom);
						roomCreated = true;
					}
					break;
				default:
					newroom = new Room() { Height = 1, Width = 1, Position = (0, 0) };
					parentWallCenter = (0, 0);
					break;
			}

			if (roomCreated)
			{
				// Add door
				arraymap[parentWallCenter.X, parentWallCenter.Y] = new SimpleFloor();

				if (direction != 2 && rand.Next(0, 100) < newRoomChance)
					GenerateRoom(arraymap, newroom, 0, newRoomChance - 10);

				if (direction != 3 && rand.Next(0, 100) < newRoomChance)
					GenerateRoom(arraymap, newroom, 1, newRoomChance - 10);

				if (direction != 0 && rand.Next(0, 100) < newRoomChance)
					GenerateRoom(arraymap, newroom, 2, newRoomChance - 10);

				if (direction != 1 && rand.Next(0, 100) < newRoomChance)
					GenerateRoom(arraymap, newroom, 3, newRoomChance - 10);
			}
		}

		static private bool ValidRoomLocation(ArrayMap2D<Terrain> arraymap, Room room)
		{
			// Check thta the room is inside the map
			if (room.Position.X - 1 < 0 || room.Position.Y - 1 < 0 || room.Position.X + room.Width + 1 > arraymap.Width || room.Position.Y + room.Height + 1 > arraymap.Height)
			{
				return false;
			}

			// Check that the room does not intersect useful terrain (shared walls are okay)
			for (int x = room.Position.X; x < room.Position.X + room.Width; x++)
			{
				for (int y = room.Position.Y; y < room.Position.Y + room.Height; y++)
				{
					if (!(arraymap[x, y] is SimpleVoid))
					{
						return false;
					}
				}
			}

			return true;
		}

		static private void AddRoomToArrayMap(ArrayMap2D<Terrain> array, Room room)
		{
			// Add floors
			for (int x = room.Position.X; x < room.Position.X + room.Width; x++)
			{
				for (int y = room.Position.Y; y < room.Position.Y + room.Height; y++)
				{
					array[x, y] = new SimpleFloor();
				}
			}

			// Add walls
			for (int x = room.Position.X - 1; x < room.Position.X + room.Width + 1; x++)
			{
				array[x, room.Position.Y - 1] = new SimpleWall();
				array[x, room.Position.Y + room.Height] = new SimpleWall();
			}

			for (int y = room.Position.Y; y < room.Position.Y + room.Height; y++)
			{
				array[room.Position.X - 1, y] = new SimpleWall();
				array[room.Position.X + room.Width, y] = new SimpleWall();
			}
		}
	}
}
