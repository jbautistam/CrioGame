using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de un láser
	/// </summary>
	public class LaserModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntRateOfFire = 200;

		public LaserModel(IScene objScene, int intFlagsSource, int intFlagsTarget, 
											GameObjectDimensions objDimensions, Vector2D vctVelocity, 
											string strTextureKey) 
							: base(objScene, objDimensions)
		{ Movement = new Movements.MovementVelocity(this, vctVelocity);
			TextureKey = strTextureKey;
			CollisionEvaluator = new CollisionEvaluator(this, intFlagsSource, intFlagsTarget,
																									CollisionEvaluator.BouncyMode.Rectangle);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddSprite(objContext.GameController.ContentController.GetContent("Paddle") as SpriteSheetContent,
								"Laser", 0, 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia la posición
				Movement.Update(objContext, Scene.ViewDefault);
			// Si se sale de la pantalla, se puede eliminar
				if (Movement.IsOutView || CollisionEvaluator.Targets.Count > 0)
					Scene.Map.RemoveGameEntity(this);
		}

		/// <summary>
		///		Clave de la textura
		/// </summary>
		public string TextureKey { get; }

		/// <summary>
		///		Movimiento
		/// </summary>
		private Movements.MovementVelocity Movement { get; }
	}
}
