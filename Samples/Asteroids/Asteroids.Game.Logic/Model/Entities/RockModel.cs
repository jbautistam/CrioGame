using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Asteroids.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una mina
	/// </summary>
	internal class RockModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntProbabilityFire = 90;
		// Variables privadas
			private TimeSpan tsPreviousFireTime = TimeSpan.Zero;

		public RockModel(IScene objScene, EnemySpawner objSpawner, GameObjectDimensions objDimensions, Polar2D vctVelocity, string strFramesKey) 
							: base(objScene, objDimensions)
		{ Spawner = objSpawner;
			Velocity = vctVelocity;
			FramesKey = strFramesKey;
			CollisionEvaluator = new CollisionEvaluator(this, 
																								(int) Configuration.GroupGameObjects.Enemy,
																								(int) Configuration.GroupGameObjects.Player,
																									CollisionEvaluator.BouncyMode.Circle);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ SpriteModel objSprite = AddAnimation("Rocks", FramesKey, "Default", 0, 0);

				objSprite.Dimensions.Scale = Dimensions.Scale;
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ Vector2D vctVelocity = Velocity.ToVector();

				if (Dimensions.Position.X + vctVelocity.X < 0)
					{	Scene.Map.RemoveGameEntity(this);
						Spawner.ComputeAsteroidKill();
					}
				else if (CollisionEvaluator.Targets.Count > 0)
					{ // Lo marca como inactivo
							Scene.Map.RemoveGameEntity(this);
						// Añade una explosión a la capa
							Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame,
																			new ExplosionModel(Scene, new GameObjectDimensions(Dimensions.Position.X, Dimensions.Position.Y), 
																													Velocity), 
																			Configuration.TimeEnemyExplosion);
						// Activa el sonido
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.ExplosionSound);
						// Manda el mensaje para cambiar la puntuación
							objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.EnemyKill, 30));
					}
				else
					{ float fltNewX = Dimensions.Position.X + vctVelocity.X;
						float fltNewY = Dimensions.Position.Y + vctVelocity.Y;

							// Desplaza la nave por los lados
								if (Dimensions.Position.X < 0)
									fltNewX = Scene.ViewDefault.ViewPortScreen.Width;
								else if (Dimensions.Position.X >= Scene.ViewDefault.ViewPortScreen.Width)
									fltNewX = 0;
								if (Dimensions.Position.Y < 0)
									fltNewY = Scene.ViewDefault.ViewPortScreen.Height;
								else if (Dimensions.Position.Y > Scene.ViewDefault.ViewPortScreen.Height)
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
