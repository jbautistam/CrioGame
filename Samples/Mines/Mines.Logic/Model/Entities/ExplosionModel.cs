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
	{ 
		public ExplosionModel(IScene objScene, GameObjectDimensions objDimensions, Vector2D vctVelocity) 
							: base(null, "Explosion", "Default", "Default",  objDimensions)
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
				if (Dimensions.Position.X + Velocity.X <= 0 || Loops > 0)
					Scene.Map.RemoveGameEntity(this);
				else
					{ // Controla los movimientos
							Dimensions.Translate(Velocity.X, Velocity.Y);
							Dimensions.ClampToView(Scene.ViewDefault.ViewPortScreen.Width, Scene.ViewDefault.ViewPortScreen.Height);
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
