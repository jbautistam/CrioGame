using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace MinesGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una mina
	/// </summary>
	public class MineModel : Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics.AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntProbabilityFire = 90;
		// Variables privadas
			private TimeSpan tsPreviousFireTime = TimeSpan.Zero;

		public MineModel(IView objView, GameObjectDimensions objDimensions, Vector2D vctVelocity, 
										 TimeSpan tsBetweenUpdate, TimeSpan tsFireSpawnTime) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ FireSpawnTime = tsFireSpawnTime;
			Velocity = vctVelocity;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) GroupGameObjects.Enemy,
																								(int) GroupGameObjects.Player);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Mine", "Default", "Default", "MineImage", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (Dimensions.Position.X + Velocity.X < 0)
				Active = false;
			else if (CollisionEvaluator.Targets.Count > 0)
				{ // Lo marca como inactivo
						Active = false;
					// Añade una explosión a la capa
						View.AddEntity(Program.LayerGame,
													 new ExplosionModel(View, Dimensions.Position.X, Dimensions.Position.Y, Velocity, Program.TimeEnemyExplosion));
					// Activa el sonido
						objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.ExplosionSound);
					// Manda el mensaje para cambiar la puntuación
						objContext.GameController.EventsManager.Enqueue(new Messages.EnemyKillMessage(1, 30));
				}
			else
				{ // Controla los movimientos
						Dimensions.Translate(Velocity);
						Dimensions.ClampToView(View.ViewPortScreen.Width, View.ViewPortScreen.Height);
					// Controla el disparo
						if (objContext.MathHelper.IsElapsed(objContext.ActualTime, FireSpawnTime, ref tsPreviousFireTime) && 
								objContext.MathHelper.Random(100) > cnstIntProbabilityFire)
							{	// Sonido para el láser
									objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.LaserSound);
								// Crea el láser
									View.AddEntity
											(Program.LayerGame,
											 new LaserModel(View, 
																			(int) GroupGameObjects.Enemy,
																			(int) GroupGameObjects.Player,
																			new GameObjectDimensions(objContext.MathHelper.Clamp(Dimensions.Position.X - Dimensions.ScaledDimensions.Width, 
																																													 0, View.ViewPortScreen.Width),
																															 objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 
																																													 0, View.ViewPortScreen.Height)),
																			new Vector2D(-5, 0), "Laser", Program.TimeSpanMineLaserUpdate));
							}
				}
		}

		/// <summary>
		///		Tiempo entre disparos
		/// </summary>
		public TimeSpan FireSpawnTime { get; }

		/// <summary>
		///		Velocidad en horizontal
		/// </summary>
		public Vector2D Velocity { get; }
	}
}
