using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Effects;
using SadConsole.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Systems
{
	static class ControlSystem
	{
		public static SadConsole.Console SelectionBox = new SadConsole.Console(1, 1) { UseMouse = false };
		public static List<Entity> SelectedEntities = new List<Entity>();

		public static bool ActiveSelecting = false;
		public static bool UpdatedSelection = false;

		public static void UpdateSelectionBox(GoRogue.Rectangle box)
		{
			SelectionBox.Resize(box.Width, box.Height, true);
			SelectionBox.Position = box.Position;
			SelectionBox.DrawBox(new Microsoft.Xna.Framework.Rectangle(0, 0, SelectionBox.Width, SelectionBox.Height), new Cell(Color.White, Color.Transparent, 179));

			SelectionBox.ConnectLines();
		}

		public static void ClearSelectionBox()
		{
			// Clear visual box display
			SelectionBox.Clear();

			// Add all entities in selection to SelectedEntities list

			ActiveSelecting = false;
		}
	}
}
