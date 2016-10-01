using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace AsteroidsGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PlayerModel : AbstractActorModel
	{ // Variables privadas
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PlayerModel(IView objView, GameObjectDimensions objDimensions) : base(objView, TimeSpan.Zero, objDimensions)
		{ CollisionEvaluator = new CollisionTargets(this, (int) GroupGameObjects.Player, (int) GroupGameObjects.Enemy);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Inicializa los parámetros de disparo
				tsFireSpawnTime = Program.TimeSpanPlayerLaserFire;
				tsPreviousFireTime = TimeSpan.Zero;
			// Añade la animación
				AddSprite(objContext.GameController.ContentController.GetContent("Rocks") as SpriteSheetContent, "Ship", 0, 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ float fltNewX = Dimensions.Position.X, fltNewY = Dimensions.Position.Y;
			Vector2D vctAcceleration = Acceleration.ToVector();

				// Controla los movimientos con el teclado
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Up) &&
							Acceleration.Length == 0)
						Acceleration = new Polar2D(Dimensions.Angle, 20);
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Down))
						Acceleration = new Polar2D(0, 0);
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Right))
						Dimensions.Angle += 0.1f;
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Left))
						Dimensions.Angle -= 0.1f;
				// Mueve la nave
					fltNewX += vctAcceleration.X;
					fltNewY += vctAcceleration.Y;
				// Desplaza la nave por los lados
					if (Dimensions.Position.X < -Dimensions.Position.Width)
						fltNewX = View.ViewPortScreen.Width - Dimensions.Position.Width;
					else if (Dimensions.Position.X >= View.ViewPortScreen.Width)
						fltNewX = 0;
					if (Dimensions.Position.Y < 0)
						fltNewY = View.ViewPortScreen.Height;
					else if (Dimensions.Position.Y > View.ViewPortScreen.Height)
						fltNewY = 0;
				// Cambia las coordenadas
					Dimensions.MoveTo(fltNewX, fltNewY);
				// Cambia el ángulo del sprite
					Sprites[0].Angle = Dimensions.Angle;
				// Decrementa la aceleación
					if (Acceleration.Length > 0.5)
						Acceleration = new Polar2D(Dimensions.Angle, Acceleration.Length - 0.3f);
					else
						Acceleration = new Polar2D(0, 0);
				// Controla el disparo
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Space) &&
							objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
						{	Vector2D vctPosition = new Polar2D(Dimensions.Angle, 1).ToVector() +
																		 new Vector2D(Dimensions.Position.X,
																									Dimensions.Position.Y);

								// Guarda el momento del disparo
									tsPreviousFireTime = objContext.ActualTime;
								// Sonido para el láser
									objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Program.LaserSound);
								// Crea el láser
									View.AddEntity(Program.LayerGame, 
																 new LaserModel(View, 
																								(int) GroupGameObjects.Player,
																								(int) GroupGameObjects.Enemy,
																								new GameObjectDimensions(vctPosition.X, vctPosition.Y),
																								new Polar2D(Dimensions.Angle, 15), 
																								Program.TimeSpanPlayerLaserUpdate));
						}
				// Controla las colisiones
					if (CollisionEvaluator.Targets.Count > 0)
						objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.MessageMode.PlayerKill, -20));
		}

		/// <summary>
		///		Aceleración
		/// </summary>
		private Polar2D Acceleration { get; set; }
	}
}
