using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityBuilder.Entities;


namespace CityBuilder.Actions
{
	class RandomMove : Action
	{
		public GoRogue.Coord Goal;
		static public Random rand = new Random();

		public RandomMove() : base()
		{
			Goal = new GoRogue.Coord(-1, -1);
		}

		public override void PerformAction(Entity parent)
		{
			if (Goal == parent.Position || Goal.X == -1)
			{
				GoRogue.Coord zero = new GoRogue.Coord(0, 0);
				int x;
				int y;
				do
				{
					x = rand.Next(0, parent.CurrentMap.Width);
					y = rand.Next(0, parent.CurrentMap.Height);
					Goal = new GoRogue.Coord(x, y);
				} while ((Goal - parent.Position) == zero || !parent.CurrentMap.WalkabilityView[x, y]);
			}

			Map.Map map = (Map.Map)parent.CurrentMap;
			GoRogue.Pathing.FastAStar pathing = new GoRogue.Pathing.FastAStar(map.WalkabilityView, GoRogue.Distance.EUCLIDEAN);
			GoRogue.Pathing.Path path = pathing.ShortestPath(parent.Position, Goal);
			if (path != null)
			{
				GoRogue.Coord next = path.GetStepWithStart(1);
				GoRogue.Coord toMove = next - parent.Position;
				parent.Position += toMove;
			}
			else
			{
				Goal = new GoRogue.Coord(-1, -1);
			}


			/*
			int dir = rand.Next(0, 4);
			GoRogue.Coord move;

			switch (dir)
			{
				case (0):
					move = new GoRogue.Coord(-1, 0);
					parent.Position += move;
					break;
				case (1):
					move = new GoRogue.Coord(0, 1);
					parent.Position += move;
					break;
				case (2):
					move = new GoRogue.Coord(1, 0);
					parent.Position += move;
					break;
				case (3):
					move = new GoRogue.Coord(0, -1);
					parent.Position += move;
					break;
				default:
					break;
			}*/
		}
	}
}
