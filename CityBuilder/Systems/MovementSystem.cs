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
				if (entity.HasGoRogueComponent<AIComponent>())
				{
					entity.GetGoRogueComponent<AIComponent>().TakeAction();
				}
			}

			return true;
		}
	}
}
