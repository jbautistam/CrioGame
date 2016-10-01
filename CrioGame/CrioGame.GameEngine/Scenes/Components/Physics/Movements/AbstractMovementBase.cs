using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements
{
	/// <summary>
	///		Componente para el manejo del movimiento de una figura
	/// </summary>
	public abstract class AbstractMovementBase : AbstractComponent
	{
		public AbstractMovementBase(Entities.Graphics.AbstractActorModel objParent, Vector2D vctVelocity)
		{ Parent = objParent;
			Velocity = vctVelocity;
		}

		/// <summary>
		///		Elemento padre
		/// </summary>
		protected Entities.Graphics.AbstractActorModel Parent { get; }

		/// <summary>
		///		Velocidad del movimiento
		/// </summary>
		protected Vector2D Velocity { get; set; }
	}
}
