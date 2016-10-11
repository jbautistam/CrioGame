using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Particles
{
	/// <summary>
	///		Clase con los datos de una partícula
	/// </summary>
	public class ParticleModel : Common.Models.AbstractModelBase
	{
		public ParticleModel(TimeSpan tsBetweenUpdate, int intLifeTime,
												 Components.Physics.Movements.AbstractMovementComponent objMovement) : base(tsBetweenUpdate)
		{ Dimensions = new GameObjectDimensions(0, 0);
			LifeTime = intLifeTime;
			Movement = objMovement;
		}

		/// <summary>
		///		Inicializa la partícula
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ TimeLastUpdate = TimeSpan.Zero;
		}

		/// <summary>
		///		Modifica los datos de la partícula
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // Mueve la partícula
				Movement.Update(objContext, Dimensions);
			// Cambia el tiempo de vida
				LifeTime--;
			// Desactiva si ha llegado al final, si no, cambia el movimiento
				Active = LifeTime > 0;
		}

		/// <summary>
		///		Dimensiones del objeto
		/// </summary>
		internal GameObjectDimensions Dimensions { get; set; }

		/// <summary>
		///		Tiempo de vida
		/// </summary>
		private int LifeTime { get; set; }

		/// <summary>
		///		Manejador del movimiento
		/// </summary>
		private Components.Physics.Movements.AbstractMovementComponent Movement { get; }

		/// <summary>
		///		Indica si la partícula está activa
		/// </summary>
		public bool Active { get; set; }
	}
}
