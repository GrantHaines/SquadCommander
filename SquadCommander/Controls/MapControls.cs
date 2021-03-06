﻿using SquadCommander.Systems;
using Microsoft.Xna.Framework;
using SadConsole.Components;
using SadConsole.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCommander.Controls
{
	class MapMouseControlComponent : MouseConsoleComponent
	{
		private bool MakingSelection;
		private Point StartPoint;
		private Point EndPoint;

		public MapMouseControlComponent()
		{
			MakingSelection = false;
		}

		public override void ProcessMouse(SadConsole.Console console, MouseConsoleState state, out bool handled)
		{
			if (state.IsOnConsole)
			{
				if (state.Mouse.LeftButtonDown && state.Mouse.IsOnScreen)
				{
					Selection(state);
				}
				else
				{
					if (MakingSelection)
					{
						MakingSelection = false;
						ControlSystem.ClearSelectionBox();
					}
				}

				if (state.Mouse.RightClicked)
				{
					ControlSystem.GiveMoveOrders(state.ConsoleCellPosition);
				}
			}

			handled = true;
		}

		private void Selection(MouseConsoleState state)
		{
			if (!MakingSelection)
			{
				// Start selection box
				MakingSelection = true;
				StartPoint = state.ConsoleCellPosition;
				EndPoint = state.ConsoleCellPosition;

				ControlSystem.StartSelectionBox();
			}
			else
			{
				// Change selection box
				EndPoint = state.ConsoleCellPosition;
			}

			// Find selection box edges
			int minX = Math.Min(StartPoint.X, EndPoint.X);
			int minY = Math.Min(StartPoint.Y, EndPoint.Y);
			int maxX = Math.Max(StartPoint.X, EndPoint.X);
			int maxY = Math.Max(StartPoint.Y, EndPoint.Y);

			// Add 1 to make border around selection area
			Point StartCorner = new Point(minX - 1, minY - 1);
			Point EndCorner = new Point(maxX + 1, maxY + 1);

			// Update selection box
			Point size = EndCorner - StartCorner + new Point(1, 1);
			Rectangle selection = new Rectangle(StartCorner, size);
			ControlSystem.UpdateSelectionBox(selection);
		}
	}

	class MapKeyboardControlComponent : KeyboardConsoleComponent
	{
		public override void ProcessKeyboard(SadConsole.Console console, Keyboard info, out bool handled)
		{
			// Open main menu
			if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
			{
				GameLogic.SwitchGameState(GameState.MAIN_MENU);
			}

			handled = true;
		}
	}
}
