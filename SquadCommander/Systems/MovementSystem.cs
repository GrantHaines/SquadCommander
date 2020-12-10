using SquadCommander.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.Systems
{
	static class MovementSystem
	{
		public static bool ProcessTurn(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.HasGameComponent<EnergyComponent>())
				{
					entity.GetGameComponent<EnergyComponent>().AddEnergy(1);
				}

				if (entity.HasGameComponent<AIComponent>())
				{
					if (entity.HasGameComponent<EnergyComponent>())
					{
						if (entity.GetGameComponent<EnergyComponent>().EnergyIsFull())
						{
							entity.GetGameComponent<EnergyComponent>().ClearEnergy();
							entity.GetGameComponent<AIComponent>().TakeAction();
						}
					}
					else
					{
						entity.GetGameComponent<AIComponent>().TakeAction();
					}
				}
			}

			return true;
		}
	}
}
