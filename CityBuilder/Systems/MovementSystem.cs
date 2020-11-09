using CityBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Systems
{
	static class MovementSystem
	{
		public static bool ProcessTurn(List<Entity> entities)
		{
			foreach (Entity entity in entities)
			{
				if (entity.HasGoRogueComponent<EnergyComponent>())
				{
					entity.GetGoRogueComponent<EnergyComponent>().AddEnergy(1);
				}

				if (entity.HasGoRogueComponent<AIComponent>())
				{
					if (entity.HasGoRogueComponent<EnergyComponent>())
					{
						if (entity.GetGoRogueComponent<EnergyComponent>().EnergyIsFull())
						{
							entity.GetGoRogueComponent<EnergyComponent>().ClearEnergy();
							entity.GetGoRogueComponent<AIComponent>().TakeAction();
						}
					}
					else
					{
						entity.GetGoRogueComponent<AIComponent>().TakeAction();
					}
				}
			}

			return true;
		}
	}
}
