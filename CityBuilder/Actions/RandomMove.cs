using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityBuilder.Entities;


namespace CityBuilder.Actions
{
	public class RandomMove : Action
	{
		public GoRogue.Coord Goal;
		static public Random rand = new Random();
		public int WaitTime;

		public RandomMove(GameEntity parent) : base(parent)
		{
			Goal = new GoRogue.Coord(-1, -1);
			WaitTime = 0;
		}

		public override void PerformAction()
		{
			if (Goal == Parent.Position || Goal.X == -1)
			{
				GoRogue.Coord zero = new GoRogue.Coord(0, 0);
				int x;
				int y;
				do
				{
					x = rand.Next(0, Parent.CurrentMap.Width);
					y = rand.Next(0, Parent.CurrentMap.Height);
					Goal = new GoRogue.Coord(x, y);
				} while ((Goal - Parent.Position) == zero || !Parent.CurrentMap.WalkabilityView[x, y]);
			}

			Map.GameMap map = (Map.GameMap)Parent.CurrentMap;
			GoRogue.Pathing.FastAStar pathing = new GoRogue.Pathing.FastAStar(map.WalkabilityView, GoRogue.Distance.EUCLIDEAN);
			GoRogue.Pathing.Path path = pathing.ShortestPath(Parent.Position, Goal);
			if (path != null)
			{
				GoRogue.Coord next = path.GetStepWithStart(1);
				GoRogue.Coord toMove = next - Parent.Position;
				Parent.Position += toMove;
			}
			else
			{
				if (WaitTime > 10)
				{
					Goal = new GoRogue.Coord(-1, -1);
					WaitTime = 0;
				}
				else
				{
					WaitTime++;
				}
			}
		}
	}
}
