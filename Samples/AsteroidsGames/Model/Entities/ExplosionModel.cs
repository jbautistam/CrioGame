using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace AsteroidsGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : SpriteAnimableModel
	{ // Variables privadas
			private int intUpdates = 0;

		public ExplosionModel(IView objView, float fltX, float fltY, Polar2D vctVelocity, TimeSpan tsBetweenUpdate) 
							: base(null, "Rocks", "Explosion", "Default", "RocksImage", tsBetweenUpdate, (int) fltX, (int) fltY)
		{ View = objView;
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
				if (X < 0 || intUpdates++ >= Frames)
					Active = false;
				else
					{ Vector2D vctVelocity = Velocity.ToVector();

							// Controla los movimientos
								X += (int) vctVelocity.X;
								Y += (int) vctVelocity.Y;
								X = (int) objContext.MathHelper.ClampScreenWidth(X, ScaledWidth, View);
								Y = (int) objContext.MathHelper.ClampScreenHeight(Y, ScaledHeight, View);
					}
			// Llama al método base
				base.Update(objContext);
		}

		/// <summary>
		///		Vista
		/// </summary>
		public IView View { get; }

		/// <summary>
		///		Velocidad en horizontal
		/// </summary>
		public Polar2D Velocity { get; }
	}
}
