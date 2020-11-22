using CityBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Actions
{
	public abstract class Action
	{
		public GameEntity Parent;

		public Action(GameEntity parent)
		{
			Parent = parent;
		}

		public abstract void PerformAction();
	}
}
