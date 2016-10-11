using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PlayerModel : AbstractActorModel
	{ // Variables privadas
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PlayerModel(IScene objScene, GameObjectDimensions objDimensions) : base(objScene, objDimensions)
		{ Velocity = new Vector2D(3, 0);
			CollisionEvaluator = new CollisionEvaluator(this, (int) Configuration.GroupGameObjects.Player, 
																									(int) Configuration.GroupGameObjects.Enemy,
																									CollisionEvaluator.BouncyMode.Rectangle);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Inicializa los parámetros de disparo
				tsFireSpawnTime = Configuration.TimeSpanPlayerLaserFire;
				tsPreviousFireTime = TimeSpan.Zero;
			// Añade la animación
				AddSprite(objContext.GameController.ContentController.GetContent("SpaceWar") as SpriteSheetContent, 
									"ShipPlayer1", 0, new GameObjectDimensions(0, 0, ColorEngine.Yellow, 1));
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ float fltNewX = Dimensions.Position.X;

				// Controla los movimientos con el teclado
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Right))
						fltNewX += Velocity.X;
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Left))
						fltNewX -= Velocity.X;
				// Restringe las coordenadas
					fltNewX = objContext.MathHelper.Clamp(fltNewX, 0, Scene.ViewDefault.ViewPortScreen.Width);
				// Desplaza la nave
					Dimensions.MoveTo(fltNewX, Dimensions.Position.Y);
				// Controla el disparo
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Space) &&
							objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
						{	// Guarda el momento del disparo
								tsPreviousFireTime = objContext.ActualTime;
							// Sonido para el láser
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.LaserSound);
							// Crea el láser
								Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame, 
																			  new LaserModel(Scene, Configuration.GroupGameObjects.Player, Configuration.GroupGameObjects.Enemy,
																											 new GameObjectDimensions(Dimensions.Position.X, Dimensions.Position.Y)),
																				Configuration.TimeSpanPlayerLaserUpdate);
						}
				// Controla las colisiones
					if (CollisionEvaluator.Targets.Count > 0)
						objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.PlayerKill, -20));
		}

		/// <summary>
		///		Velocidad del jugador
		/// </summary>
		private Vector2D Velocity { get; }
	}
}
