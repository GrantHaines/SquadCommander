using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Map
{
    public class Terrain : SadConsole.BasicTerrain
    {
        public Terrain(GoRogue.Coord position, bool isWalkable, bool isTransparent) : base(position, isWalkable, isTransparent)
        {

        }

        public Terrain(GoRogue.Coord position, bool isWalkable, bool isTransparent, Color foreground, Color background, int glyph) : base(foreground, background, glyph, position, isWalkable, isTransparent)
        {

        }
    }

    public class SimpleFloor : Terrain
    {
        public SimpleFloor(GoRogue.Coord position) : base(position, true, true, Color.White, Color.Black, '.')
        {

        }
    }
}