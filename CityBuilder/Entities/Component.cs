using CityBuilder.Actions;
using GoRogue;
using Microsoft.Xna.Framework;
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

	class EnergyComponent : Component
	{
		private int CurrentEnergy;
		private int EnergyToMove;

		public EnergyComponent(int max)
		{
			CurrentEnergy = 0;
			EnergyToMove = max;
		}

		public void AddEnergy(int toAdd)
		{
			CurrentEnergy += toAdd;
			if (CurrentEnergy > EnergyToMove)
			{
				CurrentEnergy = EnergyToMove;
			}
		}

		public void ClearEnergy()
		{
			CurrentEnergy = 0;
		}

		public bool EnergyIsFull()
		{
			if (CurrentEnergy >= EnergyToMove)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	class AIComponent : Component
	{
		public Actions.Action ActionType;
		public GoRogue.Coord Goal;
		static public Random rand = new Random();

		public AIComponent() : base()
		{
			Goal = new GoRogue.Coord(-1, -1);
		}

		public bool TakeAction()
		{
			MakeDecision();

			if (ActionType != null)
			{
				ActionType.PerformAction();
			}

			return true;
		}

		public void MakeDecision()
		{
			if (Goal == Parent.Position || Goal.X == -1)
			{
				GoRogue.Coord zero = new GoRogue.Coord(0, 0);
				int x;
				int y;
				do
				{
					x = rand.Next(0, Parent.CurrentMap.Width);
					y = rand.Next(0, Parent.CurrentMap.Height);
					Goal = new GoRogue.Coord(x, y);
				} while ((Goal - Parent.Position) == zero || !Parent.CurrentMap.WalkabilityView[x, y]);

				ActionType = new MoveTo((Entity)Parent, Goal);
			}
		}

		public void SetGoal(Coord move)
		{
			Goal = move;
			ActionType = new MoveTo((Entity)Parent, Goal);
		}
	}

	class SelectedActorComponent : Component
	{
		private SadConsole.Console SelectedBox;

		public SelectedActorComponent()
		{
			SelectedBox = new SadConsole.Console(3, 3);
			SelectedBox.SetCellAppearance(0, 0, new SadConsole.Cell(Color.White, Color.Transparent, 218));
			SelectedBox.SetCellAppearance(2, 0, new SadConsole.Cell(Color.White, Color.Transparent, 191));
			SelectedBox.SetCellAppearance(0, 2, new SadConsole.Cell(Color.White, Color.Transparent, 192));
			SelectedBox.SetCellAppearance(2, 2, new SadConsole.Cell(Color.White, Color.Transparent, 217));
			SelectedBox.Position = Parent.Position - new Point(1, 1);

			GameLogic.MainConsole.Children.Add(SelectedBox);
		}

		~SelectedActorComponent()
		{
			GameLogic.MainConsole.Children.Remove(SelectedBox);
		}
	}
}