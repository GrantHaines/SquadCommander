using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.Map
{
	public class Terrain : SadConsole.Cell
	{
		public bool IsWalkable;
		public bool IsTransparent;

		public Terrain() : base()
		{

		}

		public Terrain(bool isWalkable, bool isTransparent) : base(Color.White, Color.Black, '?')
		{
			IsWalkable = isWalkable;
			IsTransparent = isTransparent;
		}

		public Terrain(bool isWalkable, bool isTransparent, Color foreground, Color background, int glyph) : base(foreground, background, glyph)
		{
			IsWalkable = isWalkable;
			IsTransparent = isTransparent;
		}
	}

	public class SimpleVoid : Terrain
	{
		public SimpleVoid() : base(true, true, Color.Black, Color.Black, '.') { }
	}

	public class SimpleFloor : Terrain
	{
		public SimpleFloor() : base(true, true, Color.White, Color.Black, '.') { }
	}

	public class SimpleWall : Terrain
	{
		public SimpleWall() : base(false, false, Color.DarkSlateGray, Color.Black, '#') { }
	}
}