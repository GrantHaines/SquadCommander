using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquadCommander.Entities;


namespace SquadCommander.Actions
{
	public class MoveTo : Action
	{
		public GoRogue.Coord Goal;

		public MoveTo(GameEntity parent, GoRogue.Coord goal) : base(parent)
		{
			Goal = goal;
		}

		public override void PerformAction()
		{
			if (Parent.Position != Goal)
			{
				Map.GameMap map = (Map.GameMap)Parent.CurrentMap;
				GoRogue.Pathing.FastAStar pathing = new GoRogue.Pathing.FastAStar(map.GetWalkabilityMap(), GoRogue.Distance.EUCLIDEAN);
				GoRogue.Pathing.Path path = pathing.ShortestPath(Parent.Position, Goal);
				if (path != null)
				{
					GoRogue.Coord next = path.GetStepWithStart(1);
					GoRogue.Coord toMove = next - Parent.Position;
					Parent.Position += toMove;
				}
			}
		}
	}
}
