using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace SpaceWarGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PlayerModel : AbstractActorModel
	{ // Variables privadas
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PlayerModel(IView objView, GameObjectDimensions objDimensions) : base(objView, TimeSpan.Zero, objDimensions)
		{ Velocity = new Vector2D(3, 0);
			CollisionEvaluator = new CollisionTargets(this, (int) GroupGameObjects.Player, (int) GroupGameObjects.Enemy);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Inicializa los parámetros de disparo
				tsFireSpawnTime = Program.TimeSpanPlayerLaserFire;
				tsPreviousFireTime = TimeSpan.Zero;
			// Añade la animación
				AddSprite(objContext.GameController.ContentController.GetContent("SpaceWar") as SpriteSheetContent, "ShipPlayer1", 0, 0, 0, ColorEngine.Yellow, 1);
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
					fltNewX = objContext.MathHelper.Clamp(fltNewX, 0, View.ViewPortScreen.Width);
				// Desplaza la nave
					Dimensions.MoveTo(fltNewX, Dimensions.Position.Y);
				// Controla el disparo
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Space) &&
							objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
						{	// Guarda el momento del disparo
								tsPreviousFireTime = objContext.ActualTime;
							// Sonido para el láser
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.LaserSound);
							// Crea el láser
								View.AddEntity(Program.LayerGame, 
															 new LaserModel(View, GroupGameObjects.Player, GroupGameObjects.Enemy,
																							new GameObjectDimensions(Dimensions.Position.X, Dimensions.Position.Y),
																							Program.TimeSpanPlayerLaserUpdate));
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
