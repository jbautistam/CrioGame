using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de un asteroide
	/// </summary>
	internal class RockModel : AbstractActorModel
	{ 
		public RockModel(IScene objScene, EnemySpawner objSpawner, GameObjectDimensions objDimensions, Vector2D vctVelocity, string strFramesKey,
										 TimeSpan tsBetweenUpdate) 
							: base(objScene, tsBetweenUpdate, objDimensions)
		{ Spawner = objSpawner;
			Velocity = vctVelocity;
			FramesKey = strFramesKey;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ SpriteModel objSprite = AddAnimation("SpaceWar", FramesKey, "Default", "SpaceWarImage", 0, 0);

				objSprite.Scale = Dimensions.Scale;
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ float fltNewX = Dimensions.Position.X + Velocity.X;
			float fltNewY = Dimensions.Position.Y + Velocity.Y;

				// Comprueba si se ha salido de la pantalla
					if (fltNewX < 0 || fltNewX > Scene.ViewDefault.ViewPortScreen.Width || fltNewY > Scene.ViewDefault.ViewPortScreen.Height)
						{ Scene.Map.RemoveGameEntity(this);
							Spawner.ComputeAsteroidKill();
						}
				// Cambia las coordenadas
					Dimensions.MoveTo(fltNewX, fltNewY);
		}

		/// <summary>
		///		Entidad que ha generado el asteroide
		/// </summary>
		private EnemySpawner Spawner { get; }

		/// <summary>
		///		Clave de los frames de animación
		/// </summary>
		private string FramesKey { get; }

		/// <summary>
		///		Velocidad
		/// </summary>
		public Vector2D Velocity { get; }
	}
}
