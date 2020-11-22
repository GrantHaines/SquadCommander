using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GoRogue.GameFramework;
using GoRogue;

namespace CityBuilder.Entities
{
	public class GameEntity : SadConsole.Entities.Entity, IHasID
	{
		// ID properties
		private static IDGenerator generator = new IDGenerator();
		public uint ID { get; }

		private List<GameComponent> _gameComponents;

		/// <summary>
		/// Default entity constructor.
		/// </summary>
		public GameEntity() : base(Color.White, Color.Black, '?')
		{
			ID = generator.UseID();
		}

		/// <summary>
		/// GameEntity constructor.
		/// </summary>
		/// <param name="foreground">The foreground color of the entity.</param>
		/// <param name="background">The background color of the entity.</param>
		/// <param name="glyph">The glyph of the entity.</param>
		public GameEntity(Color foreground, Color background, int glyph, GoRogue.Coord position) : base (foreground, background, glyph)
		{
			Position = position;
			ID = generator.UseID();
		}

		/// <summary>
		/// Adds a new GameComponent to the GameEntity.
		/// </summary>
		/// <param name="newComponent">The component to add.</param>
		public void AddGameComponent(GameComponent newComponent)
		{
			// Make sure this is not a duplicate component
			foreach (GameComponent comp in _gameComponents)
			{
				if (comp.Equals(newComponent))
				{
					throw new System.Exception("GameEntity cannot have duplicate components");
				}
			}

			// Add the new component
			newComponent.Parent = this;
			_gameComponents.Add(newComponent);
		}

		/// <summary>
		/// Gets the component of the given type from the GameEntity. If no component is found, returns null.
		/// </summary>
		/// <typeparam name="T">The type of component to find.</typeparam>
		/// <returns>The matching component, or null if one is not found.</returns>
		public GameComponent GetGameComponent<T>()
		{
			// Search for the component
			foreach (GameComponent comp in _gameComponents)
			{
				if (comp is T)
				{
					// Return if found
					return comp;
				}
			}

			// If none are found, return null
			return null;
		}
	}
}