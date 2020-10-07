using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CityBuilder.Entities
{
    class Entity : SadConsole.BasicEntity
    {
        public uint Id;

        public Entity(GoRogue.Coord position, int layer, bool isWalkable, bool isTransparent) : base(position, layer, isWalkable, isTransparent)
        {
            Id = GameLogic.EntityIDGenerator.UseID();
        }

        public Entity(GoRogue.Coord position, int layer, bool isWalkable, bool isTransparent, Color foreground, Color background, int glyph) : base(foreground, background, glyph, position, layer, isWalkable, isTransparent)
        {
            Id = GameLogic.EntityIDGenerator.UseID();
        }
    }
}