using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace ArkanoidGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : AbstractActorModel
	{ // Variables privadas
			private int intUpdates = 0;

		public ExplosionModel(IView objView, GameObjectDimensions objDimensions, Vector2D vctVelocity, TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ Movement = new Movements.MovementVelocity(this, vctVelocity);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Paddle", "Explosion", "Default", "PaddleImage", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (intUpdates++ >= 24)
				Active = false;
			else
				Movement.Update(objContext, View);
		}

		/// <summary>
		///		Control de movimiento
		/// </summary>
		private Movements.MovementVelocity Movement { get; }
	}
}
