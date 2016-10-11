using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una píldora de energía
	/// </summary>
	public class PillModel : AbstractActorModel
	{ 
		/// <summary>
		///		Tipo de píldora
		/// </summary>
		public enum PillType
		{ Pill0,
			Pill1,
			Pill2,
			Pill3,
			PillNewBalls,
			//Pill5,
			//Pill6,
			PillBomb,
			None
		}

		public PillModel(IScene objScene, PillType intType, GameObjectDimensions objDimensions, 
										 Vector2D vctVelocity, TimeSpan tsBetweenUpdate) 
							: base(objScene, tsBetweenUpdate, objDimensions)
		{ Pill = intType;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) Configuration.GroupCollisionObjects.Pill,
																								(int) Configuration.GroupCollisionObjects.Player);
			Movement = new Movements.MovementVelocity(this, vctVelocity);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Paddle", Pill.ToString(), "Default", "PaddleImage", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	if (CollisionEvaluator.Targets.Count > 0 || Movement.IsOutView)
				Scene.Map.RemoveGameEntity(this);
			else
				Movement.Update(objContext, Scene.ViewDefault);
		}

		/// <summary>
		///		Tipo de pildora
		/// </summary>
		public PillType Pill { get; }

		/// <summary>
		///		Control de movimiento
		/// </summary>
		private Movements.MovementVelocity Movement { get; }
	}
}
