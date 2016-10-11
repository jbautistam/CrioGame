using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PaddleModel : AbstractActorModel
	{ // Enumerados privados
			public enum PaddleType
				{ Normal,
					Large,
					Small,
					Fire,
					Died
				}
		// Variables privadas
			private int intSpeed = 8;
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PaddleModel(IScene objScene, GameObjectDimensions objDimensions) : base(objScene, TimeSpan.Zero, objDimensions)
		{ StartPosition = new Vector2D(objDimensions.Position.X, objDimensions.Position.Y);
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) Configuration.GroupCollisionObjects.Player,
																								(int) (Configuration.GroupCollisionObjects.Ball | 
																											 Configuration.GroupCollisionObjects.Enemy | 
																											 Configuration.GroupCollisionObjects.Pill));
		}

		/// <summary>
		///		Inicializa el paddle
		/// </summary>
		internal void Reset()
		{ Dimensions.MoveTo(StartPosition.X, StartPosition.Y);
			SetPaddle(PaddleType.Normal);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Inicializa los parámetros de disparo
				tsFireSpawnTime = Configuration.TimeSpanPlayerLaserFire;
				tsPreviousFireTime = TimeSpan.Zero;
			// Inicializa los sprites
				AddAnimation("Paddle", "Paddle", PaddleType.Normal.ToString(), "PaddleImage", 0, 0);
				AddAnimation("Paddle", "Paddle", PaddleType.Large.ToString(), "PaddleImage", 0, 0, false);
				AddAnimation("Paddle", "Paddle", PaddleType.Small.ToString(), "PaddleImage", 0, 0, false);
				AddAnimation("Paddle", "Paddle", PaddleType.Fire.ToString(), "PaddleImage", 0, 0, false);
				AddAnimation("Paddle", "Paddle", PaddleType.Died.ToString(), "PaddleImage", 0, 0, false);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (ActualPaddle != PaddleType.Died)
				{ // Controla las colisiones
						TreatCollisions(objContext);
					// Controla los movimientos con el teclado
						if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Right))
							Dimensions.Translate(intSpeed, 0);
						if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Left))
							Dimensions.Translate(-intSpeed, 0);
					// Normaliza las coordenadas
						Dimensions.ClampToView(Scene.ViewDefault.ViewPortScreen.Width, Scene.ViewDefault.ViewPortScreen.Height);
					// Controla el disparo
						if (ActualPaddle == PaddleType.Fire &&
								objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Space) &&
								objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
							{	// Guarda el momento del disparo
									tsPreviousFireTime = objContext.ActualTime;
								// Sonido para el láser
									objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.LaserSound);
								// Crea dos láser
									CreateLaser(objContext, Dimensions.Position.X);
									CreateLaser(objContext, Dimensions.Position.X + Dimensions.ScaledDimensions.Width);
							}
					}
		}

		/// <summary>
		///		Crea un láser
		/// </summary>
		private void CreateLaser(IGameContext objContext, float fltX)
		{	Scene.Map.AddGameEntity(Scene.ViewDefault, Scenes.GameScene.LayerGame, 
															new LaserModel(Scene, 
																						 (int) Configuration.GroupCollisionObjects.Player,
																						 (int) (Configuration.GroupCollisionObjects.Enemy | Configuration.GroupCollisionObjects.Brick),
																						 new GameObjectDimensions(objContext.MathHelper.Clamp(fltX, 0, Scene.ViewDefault.ViewPortScreen.Width),
																																			objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 
																																																	0, Scene.ViewDefault.ViewPortScreen.Height)),
																						 new Vector2D(0, -10), "Laser", Configuration.TimeSpanPlayerLaserUpdate));
		}

		/// <summary>
		///		Trata las colisiones
		/// </summary>
		private void TreatCollisions(IGameContext objContext)
		{ PaddleType intNewType = ActualPaddle;

				// Recorre las colisiones buscando las píldoras contra las que se choca
					foreach (CollisionTargets objTarget in CollisionEvaluator.Targets)
						if (objTarget.ParentDraw is PillModel)
							{ PillModel objPill = objTarget.ParentDraw as PillModel;

									switch (objPill.Pill)
										{	case PillModel.PillType.Pill0:
													intNewType = PaddleType.Small;
												break;
											case PillModel.PillType.Pill1:
													intNewType = PaddleType.Large;
												break;
											case PillModel.PillType.Pill2:
													intNewType = PaddleType.Fire;
												break;
											case PillModel.PillType.Pill3:
													objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.NewLife, 1));
												break;
											case PillModel.PillType.PillNewBalls:
													objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.CreateBalls, 3));
												break;
											case PillModel.PillType.PillBomb:
													intNewType = PaddleType.Died;
												break;
										}
							}
				// Si estoy muerto...
					if (intNewType == PaddleType.Died)
						objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.KillPlayer, 0));
					else
						SetPaddle(intNewType);
		}

		/// <summary>
		///		Cambia el estado del paddle
		/// </summary>
		private void SetPaddle(PaddleType intNewType)
		{	if (ActualPaddle != intNewType)
				{ // Desactiva la animación antigua
						Sprites.EnableAnimation(ActualPaddle.ToString(), false);
					// Activa la nueva animación
						Sprites.EnableAnimation(intNewType.ToString(), true);
					// Cambia la animación
						ActualPaddle = intNewType;
				}
		}

		/// <summary>
		///		Posición inicial
		/// </summary>
		private Vector2D StartPosition { get; set; }

		/// <summary>
		///		Tipo de paddle actual
		/// </summary>
		private PaddleType ActualPaddle { get; set; } = PaddleType.Normal;
	}
}
