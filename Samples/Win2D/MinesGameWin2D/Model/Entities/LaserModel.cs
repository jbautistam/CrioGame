using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace MinesGameWin2D.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de un láser
	/// </summary>
	public class LaserModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntRateOfFire = 200;

		public LaserModel(IView objView, int intFlagsSource, int intFlagsTarget, 
											GameObjectDimensions objDimensions, Vector2D vctVelocity, 
											string strTextureKey, TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ Velocity = vctVelocity;
			TextureKey = strTextureKey;
			CollisionEvaluator = new CollisionTargets(this, intFlagsSource, intFlagsTarget);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddSprite("Laser", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia la posición
				Dimensions.Translate(Velocity.X, Velocity.Y);
			// Si se sale de la pantalla, se puede eliminar
				if (!Dimensions.IsAtRectangle(View.ViewPortScreen) || CollisionEvaluator.Targets.Count > 0)
					Active = false;
		}

		/// <summary>
		///		Clave de la textura
		/// </summary>
		public string TextureKey { get; }

		/// <summary>
		///		Velocidad del láser
		/// </summary>
		public Vector2D Velocity { get; }
	}
}
