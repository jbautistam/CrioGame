using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Mines.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : SpriteAnimableModel
	{ // Variables privadas
			private int intUpdates = 0;

		public ExplosionModel(IScene objScene, float fltX, float fltY, Vector2D vctVelocity, TimeSpan tsBetweenUpdate) 
							: base(null, "Explosion", "Default", "Default", "ExplosionImage", tsBetweenUpdate, (int) fltX, (int) fltY)
		{ Scene = objScene;
			Velocity = vctVelocity;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ base.Initialize(objContext);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // Modifica las coordenadas y el estado del objeto
				if (X + Velocity.X <= 0 || intUpdates++ >= Frames)
					Scene.Map.RemoveGameEntity(this);
				else
					{ // Controla los movimientos
							X += (int) Velocity.X;
							Y += (int) Velocity.Y;
							X = (int) objContext.MathHelper.ClampScreenWidth(X, ScaledWidth, Scene.ViewDefault);
							Y = (int) objContext.MathHelper.ClampScreenHeight(Y, ScaledHeight, Scene.ViewDefault);
					}
			// Llama al método base
				base.Update(objContext);
		}

		/// <summary>
		///		Escena
		/// </summary>
		public IScene Scene { get; }

		/// <summary>
		///		Velocidad en horizontal
		/// </summary>
		public Vector2D Velocity { get; }
	}
}
