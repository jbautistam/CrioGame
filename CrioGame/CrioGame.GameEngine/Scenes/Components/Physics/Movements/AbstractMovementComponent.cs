using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements
{
	/// <summary>
	///		Componente para manejo del movimiento
	/// </summary>
	public abstract class AbstractMovementComponent
	{
		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public void Update(IGameContext objContext, GameObjectDimensions objDimensions)
		{ // Realiza el movimiento
				Move(objContext, objDimensions);
		}

		/// <summary>
		///		Realiza el movimiento
		/// </summary>
		protected abstract void Move(IGameContext objContext, GameObjectDimensions objDimensions);
	}
}
