using CityBuilder.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Entities
{
	class Component : SadConsole.Components.GoRogue.ComponentBase
	{
		
	}

	class HealthComponent : Component
	{
		int CurrentHealth;
		int MaxHealth;

		public HealthComponent(int max)
		{
			CurrentHealth = max;
			MaxHealth = max;
		}
	}

	class AIComponent : Component
	{
		Actions.Action ActionType;

		public AIComponent()
		{
			ActionType = new RandomMove();
		}

		public bool TakeAction()
		{
			ActionType.PerformAction((Entity)Parent);

			return true;
		}
	}
}