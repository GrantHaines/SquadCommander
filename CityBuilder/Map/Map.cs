using GoRogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Map
{
	public class Map : SadConsole.BasicMap
	{
		public Map(int width, int height, int numberOfEntityLayers, Distance distanceMeasurement) : base(width, height, numberOfEntityLayers, distanceMeasurement)
		{

		}
	}
}
