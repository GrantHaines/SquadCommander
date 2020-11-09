using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityBuilder.Entities;


namespace CityBuilder.Actions
{
	class MoveTo : Action
	{
		public GoRogue.Coord Goal;

		public MoveTo(Entity parent, GoRogue.Coord goal) : base(parent)
		{
			Goal = goal;
		}

		public override void PerformAction()
		{
			if (Parent.Position != Goal)
			{
				Map.Map map = (Map.Map)Parent.CurrentMap;
				GoRogue.Pathing.FastAStar pathing = new GoRogue.Pathing.FastAStar(map.WalkabilityView, GoRogue.Distance.EUCLIDEAN);
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
