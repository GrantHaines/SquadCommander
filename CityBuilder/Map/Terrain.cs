using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Map
{
	public class Terrain : Entities.GameEntity
	{
		public bool IsWalkable;
		public bool IsTransparent;

		public Terrain() : base()
		{

		}

		public Terrain(GoRogue.Coord position, bool isWalkable, bool isTransparent) : base(Color.White, Color.Black, '?', position)
		{
			IsWalkable = isWalkable;
			IsTransparent = isTransparent;
		}

		public Terrain(GoRogue.Coord position, bool isWalkable, bool isTransparent, Color foreground, Color background, int glyph) : base(foreground, background, glyph, position)
		{
			IsWalkable = isWalkable;
			IsTransparent = isTransparent;
		}
	}

	public class SimpleFloor : Terrain
	{
		public SimpleFloor(GoRogue.Coord position) : base(position, true, true, Color.White, Color.Black, '.') { }
	}

	public class SimpleWall : Terrain
	{
		public SimpleWall(GoRogue.Coord position) : base(position, false, false, Color.DarkSlateGray, Color.Black, '#') { }
	}
}