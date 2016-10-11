using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de un láser
	/// </summary>
	public class LaserModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntRateOfFire = 200;

		public LaserModel(IScene objScene, Configuration.GroupGameObjects intFlagsSource, Configuration.GroupGameObjects intFlagsTarget, 
											GameObjectDimensions objDimensions) 
							: base(objScene, objDimensions)
		{ if (intFlagsSource == Configuration.GroupGameObjects.Player)
				{ Velocity = new Vector2D(0, -3);
					SheetKey = "LaserPlayer";
				}
			else
				{ Velocity = new Vector2D(0, 3);
					SheetKey = "LaserEnemy";
				}
			CollisionEvaluator = new CollisionEvaluator(this, (int) intFlagsSource, (int) intFlagsTarget,
																									CollisionEvaluator.BouncyMode.Rectangle);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("SpaceWar", SheetKey, "Default", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia la posición
				Dimensions.Translate(Velocity);
			// Si se sale de la pantalla, se puede eliminar
				if (!Dimensions.IsAtRectangle(Scene.ViewDefault.ViewPortScreen) || CollisionEvaluator.Targets.Count > 0)
					Scene.Map.RemoveGameEntity(this);
		}

		/// <summary>
		///		Clave de la hoja donde se encuentra la animación
		/// </summary>
		private string SheetKey { get; }

		/// <summary>
		///		Velocidad del láser
		/// </summary>
		public Vector2D Velocity { get; }
	}
}
