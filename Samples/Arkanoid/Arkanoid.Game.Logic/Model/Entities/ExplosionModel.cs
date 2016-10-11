using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : AbstractActorModel
	{
		public ExplosionModel(IScene objScene, GameObjectDimensions objDimensions, Vector2D vctVelocity) 
							: base(objScene, objDimensions)
		{ Movement = new Movements.MovementVelocity(this, vctVelocity);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Paddle", "Explosion", "Default", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (((Sprites[0] as SpriteAnimableModel)?.Loops ?? 0) > 0 || Movement.IsOutView)
				Scene.Map.RemoveGameEntity(this);
			else
				Movement.Update(objContext, Scene.ViewDefault);
		}

		/// <summary>
		///		Control de movimiento
		/// </summary>
		private Movements.MovementVelocity Movement { get; }
	}
}
