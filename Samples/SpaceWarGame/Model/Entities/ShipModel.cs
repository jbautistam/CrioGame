﻿using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace SpaceWarGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una nave enemiga
	/// </summary>
	internal class ShipModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstIntProbabilityFire = 90;
		// Variables privadas
			private TimeSpan tsPreviousFireTime = TimeSpan.Zero;

		public ShipModel(IView objView, GameObjectDimensions objDimensions, int intShipType, 
										 Vector2D vctVelocity, int intOffset, ColorEngine clrColor, TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ StartPoint = new Vector2D(objDimensions.Position.X, objDimensions.Position.Y);
			ShipType = intShipType;
			Velocity = new Vector2D(Math.Abs(vctVelocity.X), Math.Abs(vctVelocity.Y));
			Offset = intOffset;
			Color = clrColor;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) GroupGameObjects.Enemy,
																								(int) GroupGameObjects.Player);
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
				{ // Lo marca como inactivo
						Active = false;
					// Añade una explosión a la capa
						View.AddEntity(Program.LayerGame,
													 ExplosionModel.Create(View, ExplosionModel.ExplosionType.Ship, 
																								 Dimensions.Position.X, Dimensions.Position.Y, new Vector2D(0, Velocity.Y), Program.TimeEnemyExplosion));
					// Activa el sonido
						objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.ExplosionSound);
					// Manda el mensaje para cambiar la puntuación
						objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.EnemyKill, 30));
				}
			else // movimiento
				{ float fltNewX = Dimensions.Position.X;
					float fltNewY = Dimensions.Position.Y;

						// Dependiendo de hacia dónde se mueva
							if (MoveRight)
								{ // Desplaza la nave hacia la derecha
										fltNewX += Velocity.X;
									// Si ha superado su desplazamiento máximo, la siguiente vez irá hacia la izquierda
										if (fltNewX > StartPoint.X + Offset)
											MoveRight = false;
								}
							else
								{ // Desplaza la nave hacia la izquierda
										fltNewX -= Velocity.X;
									// Si ha superado su desplazamiento máximo, baja la nave y la siguiente vez irá hacia la derecha
										if (fltNewX < StartPoint.X - Offset)
											{ // Desplaza hacia abajo
													fltNewY += Velocity.Y;
												// Cambia el movimiento
													MoveRight = true;
											}
								}
						// Si ha pasado el borde inferior de la pantalla, explota y quita una vida al jugador
							if (Dimensions.Position.Y > View.ViewPortScreen.Height - 10)
								System.Diagnostics.Debug.WriteLine("Explotar");
						// Cambia las coordenadas
							Dimensions.MoveTo(fltNewX, fltNewY);
						// ... y dispara
							if (objContext.MathHelper.IsElapsed(objContext.ActualTime, Program.TimeSpanShipFire, ref tsPreviousFireTime) && 
									objContext.MathHelper.Random(100) > cnstIntProbabilityFire)
								{	// Sonido para el láser
										objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.LaserSound);
									// Crea el láser
										View.AddEntity
												(Program.LayerGame,
												 new LaserModel(View, GroupGameObjects.Enemy, GroupGameObjects.Player,
																				new GameObjectDimensions(objContext.MathHelper.Clamp(Dimensions.Position.X + Dimensions.ScaledDimensions.Width / 2, 
																																														 0, View.ViewPortScreen.Width),
																																 objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 
																																														 0, View.ViewPortScreen.Height)),
																				Program.TimeSpanMineLaserUpdate));
								}
				}
		}

		/// <summary>
		///		Punto inicial
		/// </summary>
		private Vector2D StartPoint { get; }

		/// <summary>
		///		Desplazamiento
		/// </summary>
		private int Offset { get; }

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

		/// <summary>
		///		Indica si se está moviendo hacia la derecha
		/// </summary>
		private bool MoveRight { get; set; } = true;
	}
}
