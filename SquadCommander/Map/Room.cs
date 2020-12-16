using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.Map
{
	public class Room
	{
		public GoRogue.Coord Position;
		public int Width, Height;
		public List<Room> Neighbors;

		public Room()
		{
			Neighbors = new List<Room>();
		}
	}
}
