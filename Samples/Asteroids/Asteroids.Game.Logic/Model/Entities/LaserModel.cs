using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Asteroids.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de un láser
	/// </summary>
	public class LaserModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntRateOfFire = 200;

		public LaserModel(IScene objScene, int intFlagsSource, int intFlagsTarget, 
											GameObjectDimensions objDimensions, Polar2D vctVelocity, 
											TimeSpan tsBetweenUpdate) 
							: base(objScene, tsBetweenUpdate, objDimensions)
		{ Velocity = vctVelocity;
			CollisionEvaluator = new CollisionTargets(this, intFlagsSource, intFlagsTarget);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Rocks", "Laser", "Default", "RocksImage", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia la posición
				Dimensions.Translate(Velocity.ToVector());
			// Si se sale de la pantalla, se puede eliminar
				if (!Dimensions.IsAtRectangle(Scene.ViewDefault.ViewPortScreen) || CollisionEvaluator.Targets.Count > 0)
					Scene.Map.RemoveGameEntity(this);
		}

		/// <summary>
		///		Velocidad del láser
		/// </summary>
		public Polar2D Velocity { get; }
	}
}
