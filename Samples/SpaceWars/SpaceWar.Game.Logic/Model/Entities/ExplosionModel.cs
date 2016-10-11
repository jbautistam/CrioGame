using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : SpriteAnimableModel
	{ // Tipo de explosion
			public enum ExplosionType
				{ Ship,
					BossGround
				}

		private ExplosionModel(IScene objScene, ExplosionType intType, GameObjectDimensions objDimensions, Vector2D vctVelocity, 
													 string strSheetContentKey, string strFramesKey, string strAnimationKey) 
							: base(null, strSheetContentKey, strFramesKey, strAnimationKey, objDimensions)
		{ Scene = objScene;
			Velocity = vctVelocity;
			Type = intType;
		}

		/// <summary>
		///		Crea una explosión
		/// </summary>
		public static ExplosionModel Create(IScene objScene, ExplosionType intType, GameObjectDimensions objDimensions, Vector2D vctVelocity)
		{ string strFramesKey = "ExplosionShip";
		
				// Obtiene la clave de animación
					switch (intType)
						{	case ExplosionType.BossGround:
									strFramesKey = "ExplosionGround";
								break;
						}
				// Crea la explosión
					return new ExplosionModel(objScene, intType, objDimensions, vctVelocity, "SpaceWar", strFramesKey, "Default");
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Inicializa el objeto
				base.Initialize(objContext);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // Modifica las coordenadas y el estado del objeto
				if (Dimensions.Position.X < 0 || Loops > 0)
					Scene.Map.RemoveGameEntity(this);
				else
					{ Dimensions.Translate(Velocity);
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

		/// <summary>
		///		Tipo de explosión
		/// </summary>
		private ExplosionType Type { get; }
	}
}
