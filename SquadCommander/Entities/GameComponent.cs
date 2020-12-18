using SquadCommander.Actions;
using GoRogue;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoRogue.GameFramework.Components;
using GoRogue.GameFramework;

namespace SquadCommander.Entities
{
	public class GameComponent
	{
		public GameEntity Parent;
	}

	public class HealthComponent : GameComponent
	{
		int CurrentHealth;
		int MaxHealth;

		public HealthComponent(int max)
		{
			CurrentHealth = max;
			MaxHealth = max;
		}
	}

	public class EnergyComponent : GameComponent
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

	public class AIComponent : GameComponent
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
				} while ((Goal - Parent.Position) == zero || !Parent.CurrentMap.GetWalkabilityMap()[x, y]);

				ActionType = new MoveTo((GameEntity)Parent, Goal);
			}
		}

		public void SetGoal(Coord move)
		{
			Goal = move;
			ActionType = new MoveTo((GameEntity)Parent, Goal);
		}
	}

	public class SelectedActorComponent : GameComponent
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

			GameLogic.GameMapScreen.MainConsole.Children.Add(SelectedBox);
		}

		~SelectedActorComponent()
		{
			GameLogic.GameMapScreen.MainConsole.Children.Remove(SelectedBox);
		}
	}
}