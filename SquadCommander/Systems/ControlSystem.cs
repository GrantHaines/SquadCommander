using SquadCommander.Entities;
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
using GameEntity = SquadCommander.Entities.GameEntity;

namespace SquadCommander.Systems
{
	static class ControlSystem
	{
		public static SadConsole.Console SelectionBox = new SadConsole.Console(1, 1) { UseMouse = false };
		public static List<GameEntity> SelectedEntities = new List<GameEntity>();

		public static bool ActiveSelecting = false;
		public static bool UpdatedSelection = false;

		private static Random rand = new Random();

		public static void StartSelectionBox()
		{
			foreach (Entities.GameEntity entity in SelectedEntities)
			{
				//entity.RemoveGoRogueComponent(entity.GetGameComponent<Entities.SelectedActorComponent>());
			}
			SelectedEntities.Clear();
		}
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
			for (int x = SelectionBox.Position.X + 1; x < SelectionBox.Position.X + SelectionBox.Width - 1; x++)
			{
				for (int y = SelectionBox.Position.Y + 1; y < SelectionBox.Position.Y + SelectionBox.Height - 1; y++)
				{
					Entities.GameEntity entity = GameLogic.GameMapScreen.MainMap.GetActor(new Coord(x, y));
					if (entity != null)
					{
						System.Console.WriteLine($"{entity.Name} selected");
						SelectedEntities.Add(entity);
						//entity.AddGoRogueComponent(new Entities.SelectedActorComponent());
					}
				}
			}

			ActiveSelecting = false;
		}

		public static void GiveMoveOrders(Coord toMove)
		{
			foreach (GameEntity entity in SelectedEntities)
			{
				if (entity.GetGameComponent<AIComponent>() != null)
				{
					Coord moveWithRand = toMove + new Coord(rand.Next(-1, 2), rand.Next(-1, 2));
					entity.GetGameComponent<AIComponent>().SetGoal(moveWithRand);
				}
			}
		}
	}
}
