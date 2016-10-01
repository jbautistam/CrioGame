using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace AsteroidsGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una mina
	/// </summary>
	internal class RockModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntProbabilityFire = 90;
		// Variables privadas
			private TimeSpan tsPreviousFireTime = TimeSpan.Zero;

		public RockModel(IView objView, EnemySpawner objSpawner, GameObjectDimensions objDimensions, Polar2D vctVelocity, string strFramesKey,
										 TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ Spawner = objSpawner;
			Velocity = vctVelocity;
			FramesKey = strFramesKey;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) GroupGameObjects.Enemy,
																								(int) GroupGameObjects.Player);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ SpriteModel objSprite = AddAnimation("Rocks", FramesKey, "Default", "RocksImage", 0, 0);

				objSprite.Scale = Dimensions.Scale;
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ Vector2D vctVelocity = Velocity.ToVector();

				if (Dimensions.Position.X + vctVelocity.X < 0)
					{	Active = false;
						Spawner.ComputeAsteroidKill();
					}
				else if (CollisionEvaluator.Targets.Count > 0)
					{ // Lo marca como inactivo
							Active = false;
						// Añade una explosión a la capa
							View.AddEntity(Program.LayerGame,
														 new ExplosionModel(View, Dimensions.Position.X, Dimensions.Position.Y, Velocity, Program.TimeEnemyExplosion));
						// Activa el sonido
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.ExplosionSound);
						// Manda el mensaje para cambiar la puntuación
							objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.EnemyKill, 30));
					}
				else
					{ float fltNewX = Dimensions.Position.X + vctVelocity.X;
						float fltNewY = Dimensions.Position.Y + vctVelocity.Y;

							// Desplaza la nave por los lados
								if (Dimensions.Position.X < 0)
									fltNewX = View.ViewPortScreen.Width;
								else if (Dimensions.Position.X >= View.ViewPortScreen.Width)
									fltNewX = 0;
								if (Dimensions.Position.Y < 0)
									fltNewY = View.ViewPortScreen.Height;
								else if (Dimensions.Position.Y > View.ViewPortScreen.Height)
									fltNewY = 0;
							// Cambia las coordenadas
								Dimensions.MoveTo(fltNewX, fltNewY);
					}
		}

		/// <summary>
		///		Entidad que ha generado el asteroido
		/// </summary>
		private EnemySpawner Spawner { get; }

		/// <summary>
		///		Clave de los frames de animación
		/// </summary>
		private string FramesKey { get; }

		/// <summary>
		///		Velocidad
		/// </summary>
		public Polar2D Velocity { get; }
	}
}
