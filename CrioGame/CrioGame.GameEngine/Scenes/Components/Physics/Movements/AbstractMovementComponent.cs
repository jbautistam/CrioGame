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
		public AbstractMovementComponent(TimeSpan tsBetweenUpdate)
		{ TimeBetweenUpdate = tsBetweenUpdate;
		}

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public void Update(IGameContext objContext, GameObjectDimensions objDimensions)
		{ if (objContext.ActualTime - TimeLastUpdate > TimeBetweenUpdate)
				{ // Realiza el movimiento
						Move(objContext, objDimensions);
					// Actualiza el momento de última modificación
						TimeLastUpdate = objContext.ActualTime;
				}
		}

		/// <summary>
		///		Realiza el movimiento
		/// </summary>
		protected abstract void Move(IGameContext objContext, GameObjectDimensions objDimensions);

		/// <summary>
		///		Tiempo entre modificaciones
		/// </summary>
		public TimeSpan TimeBetweenUpdate { get; }

		/// <summary>
		///		Momento de la última modificación
		/// </summary>
		private TimeSpan TimeLastUpdate { get; set; }
	}
}
