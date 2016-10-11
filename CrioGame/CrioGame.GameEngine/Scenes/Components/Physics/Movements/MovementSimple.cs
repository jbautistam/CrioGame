using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements
{
	/// <summary>
	///		Movimiento simple: añade la velocidad 
	/// </summary>
	public abstract class MovementSimple : AbstractMovementBase
	{
		public MovementSimple(Entities.Graphics.AbstractActorModel objParent, Vector2D vctVelocity) : base(objParent, vctVelocity)
		{
		}

		/// <summary>
		///		Modifica las coordenadas
		/// </summary>
		public override void Update(IGameContext objContext, IView objView)
		{ if (IsDisabled(objContext, objView, Parent.Dimensions.Position.X + Velocity.X, Parent.Dimensions.Position.Y + Velocity.Y, false))
				IsOutView = true;
			else
				Parent.Dimensions.Translate(Velocity);
		}

		/// <summary>
		///		Comprueba si la nueva posición del elemento debe marcarle como inactivo
		/// </summary>
		public abstract bool IsDisabled(IGameContext objContext, IView objView, float fltNextX, float fltNextY, bool blnBorrarEsteParametroTesting);

		/// <summary>
		///		Indica si el movimiento está fuera de la vista
		/// </summary>
		public bool IsOutView { get; private set; }
	}
}
