using CityBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Actions
{
	abstract class Action
	{
		public Entity Parent;

		public Action(Entity parent)
		{
			Parent = parent;
		}

		public abstract void PerformAction();
	}
}
