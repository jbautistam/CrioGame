using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una mina
	/// </summary>
	public class BrickModel : AbstractActorModel
	{ 
		/// <summary>
		///		Tipo de ladrillo
		/// </summary>
		public enum BrickType
			{ Magenta,
				Brown,
				Blue,
				Gray,
				Antique,
				White,
				Orange,
				Red,
				Yellow
			}

		public BrickModel(IScene objScene, GameObjectDimensions objDimensions, BrickType intType, PillModel.PillType intPill, 
											ColorEngine? clrColor = null, int intZOrder = 2) 
							: base(objScene, objDimensions)
		{ Brick = intType;
			Pill = intPill;
			Strength = GetStrength(intType);
			CollisionEvaluator = new CollisionTargets(this, 
																									(int) Configuration.GroupCollisionObjects.Brick,
																									(int) (Configuration.GroupCollisionObjects.Ball | Configuration.GroupCollisionObjects.Player));

		}

		/// <summary>
		///		Obtiene la dureza de un tipo de archivo
		/// </summary>
		private int GetStrength(BrickType intType)
		{ switch (intType)
				{ case BrickType.Brown:
					case BrickType.Red:
					case BrickType.Blue:
						return 2;
					case BrickType.Gray:
					case BrickType.White:
						return 2;
					default:
						return 1;
				}
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddSprite(objContext.GameController.ContentController.GetContent("Paddle") as SpriteSheetContent,
								"Bricks", (int) Brick, new GameObjectDimensions(0, 0));
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ if (CollisionEvaluator.Targets.Count > 0)
				{ // Rebaja la dureza del ladrillo
						Strength--;
					// Si ha llegado al final ...
						if (Strength == 0)
							{ // Desactiva la entidad
									Scene.Map.RemoveGameEntity(this);
								// Añade una explosión a la capa
									Scene.Map.AddGameEntity(Scene.ViewDefault, Scenes.GameScene.LayerGame,
																					new ExplosionModel(Scene, 
																														 new GameObjectDimensions(Dimensions.Position.X + Dimensions.ScaledDimensions.Width / 2, 
																																											Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2), 
																														 new Vector2D(0, 5)), 
																					Configuration.TimeEnemyExplosion);
								// Activa el sonido
									objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.ExplosionSound);
								// Manda el mensaje para cambiar la puntuación
									objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.KillBrick, 30));
								// Crea una píldora si es necesario
									if (Pill != PillModel.PillType.None)
										Scene.Map.AddGameEntity(Scene.ViewDefault, Scenes.GameScene.LayerGame,
																						new PillModel(Scene, Pill, 
																													new GameObjectDimensions(Dimensions.Position.X + Dimensions.ScaledDimensions.Width / 2, 
																																									 Dimensions.Position.Y + Dimensions.ScaledDimensions.Height + 1),
																													new Vector2D(0, 3)), 
																						TimeSpan.FromMilliseconds(5));
							}
						else // ... manda el mensaje para cambiar la puntuación
							objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.AddScore, 10));
				}
		}

		/// <summary>
		///		Tipo de ladrillo
		/// </summary>
		private BrickType Brick { get; }
		
		/// <summary>
		///		Píldora asociada al ladrillo
		/// </summary>
		private PillModel.PillType Pill { get; }

		/// <summary>
		///		Dureza del ladrillo
		/// </summary>
		private int Strength { get; set; }

		/// <summary>
		///		Hoja de imágenes
		/// </summary>
		private SpriteSheetFrames SpriteSheet { get; }
	}
}
