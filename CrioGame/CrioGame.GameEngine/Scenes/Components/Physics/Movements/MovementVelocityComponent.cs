using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements
{
	/// <summary>
	///		Movimiento asociado a la velocidad de un objeto
	/// </summary>
	public class MovementVelocityComponent : AbstractMovementComponent
	{
		public MovementVelocityComponent(Vector2D vctVelocity, float fltAngle = 0)
		{ Velocity = vctVelocity;
			Angle = fltAngle;
		}

		/// <summary>
		///		Mueve el objeto
		/// </summary>
		protected override void Move(IGameContext objContext, GameObjectDimensions objDimensions)
		{ objDimensions.Translate(Velocity);
			objDimensions.Angle += Angle;
		}

		/// <summary>
		///		Velocidad
		/// </summary>
		private Vector2D Velocity { get; }

		/// <summary>
		///		Angulo
		/// </summary>
		private float Angle { get; }
	}
}
