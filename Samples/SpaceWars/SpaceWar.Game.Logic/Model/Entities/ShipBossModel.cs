using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una nave enemiga
	/// </summary>
	internal class ShipBossModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntProbabilityFire = 90;
		// Variables privadas
			private TimeSpan tsPreviousFireTime = TimeSpan.Zero;

		public ShipBossModel(IScene objScene, GameObjectDimensions objDimensions, int intShipType, 
												 Vector2D vctVelocity, ColorEngine clrColor, TimeSpan tsBetweenUpdate) 
							: base(objScene, tsBetweenUpdate, objDimensions)
		{ ShipType = intShipType;
			Velocity = vctVelocity;
			Color = clrColor;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) Configuration.GroupGameObjects.Enemy,
																								(int) Configuration.GroupGameObjects.Player);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddSprite(objContext.GameController.ContentController.GetContent("SpaceWar") as SpriteSheetContent,
								$"ShipEnemy{ShipType}", 0, 0, 0, Color, 1);
			Sprites[0].Scale = Dimensions.Scale;
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (CollisionEvaluator.Targets.Count > 0)
				Explode(objContext, ExplosionModel.ExplosionType.Ship);
			else if (Dimensions.Position.Y + Dimensions.ScaledDimensions.Height > Scene.ViewDefault.ViewPortScreen.Height)
				Explode(objContext, ExplosionModel.ExplosionType.BossGround);
			else // movimiento
				{ // Cambia las coordenadas
						Dimensions.Translate(Velocity);
					// ... y dispara
						if (objContext.MathHelper.IsElapsed(objContext.ActualTime, Configuration.TimeSpanShipFire, ref tsPreviousFireTime) && 
								objContext.MathHelper.Random(100) > cnstIntProbabilityFire)
							{	// Sonido para el láser
									objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.LaserSound);
								// Crea el láser
									Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame,
																					new LaserModel(Scene, Configuration.GroupGameObjects.Enemy, Configuration.GroupGameObjects.Player,
																												 new GameObjectDimensions(objContext.MathHelper.Clamp(Dimensions.Position.X - Dimensions.ScaledDimensions.Width / 2, 
																																																							0, Scene.ViewDefault.ViewPortScreen.Width),
																																									objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 
																																																							0, Scene.ViewDefault.ViewPortScreen.Height)),
																			Configuration.TimeSpanMineLaserUpdate));
							}
				}
		}

		/// <summary>
		///		Trata la explosión
		/// </summary>
		private void Explode(IGameContext objContext, ExplosionModel.ExplosionType intType)
		{ // Lo marca como inactivo
				Scene.Map.RemoveGameEntity(this);
			// Añade una explosión a la capa
				Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame,
															  ExplosionModel.Create(Scene, intType, Dimensions.Position.X, Dimensions.Position.Y - 20, 
																										  new Vector2D(0, 0), Configuration.TimeEnemyExplosion));
			// Activa el sonido
				objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.ExplosionSound);
			// Manda el mensaje para cambiar la puntuación
				if (intType != ExplosionModel.ExplosionType.BossGround)
					objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.EnemyBossKill, 60));
		}

		/// <summary>
		///		Tipo de nave
		/// </summary>
		private int ShipType { get; }

		/// <summary>
		///		Velocidad
		/// </summary>
		private Vector2D Velocity { get; }

		/// <summary>
		///		Color de la nave
		/// </summary>
		private ColorEngine Color { get; }
	}
}
