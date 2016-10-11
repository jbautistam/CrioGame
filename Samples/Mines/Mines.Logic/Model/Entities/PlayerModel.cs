using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.Mines.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PlayerModel : AbstractActorModel
	{ // Variables privadas
			private int intSpeed = 8;
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PlayerModel(IScene objScene, GameObjectDimensions objDimensions) : base(objScene, objDimensions)
		{ CollisionEvaluator = new CollisionEvaluator(this, 
																								(int) Configuration.GroupGameObjects.Player, 
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
				AddAnimation("Player", "Default", "Default", 0, 0);
			// Coloca la imagen
				Dimensions.Position = new Rectangle(0, Scene.ViewDefault.ViewPortScreen.Height / 2, 
																						Dimensions.Position.Width, Dimensions.Position.Height);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ float fltNewX = Dimensions.Position.X, fltNewY = Dimensions.Position.Y;

				// Controla los movimientos con el teclado
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Up))
						fltNewY -= intSpeed;
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Down))
						fltNewY += intSpeed;
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Right))
						fltNewX += intSpeed;
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Left))
						fltNewX -= intSpeed;
				// Normaliza las coordenadas
					Dimensions.MoveTo(fltNewX, fltNewY);
					Dimensions.ClampToView(Scene.ViewDefault.ViewPortScreen.Width, Scene.ViewDefault.ViewPortScreen.Height);
				// Controla el disparo
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(CrioGame.Common.Enums.Keys.Space) &&
							objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
						{	// Guarda el momento del disparo
								tsPreviousFireTime = objContext.ActualTime;
							// Sonido para el láser
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.LaserSound);
							// Crea el láser
								Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame, 
																				new LaserModel(Scene, 
																											 (int) Configuration.GroupGameObjects.Player,
																											 (int) Configuration.GroupGameObjects.Enemy,
																											 new GameObjectDimensions(objContext.MathHelper.Clamp(Dimensions.Position.X + Dimensions.ScaledDimensions.Width, 0, 
																																																						Scene.ViewDefault.ViewPortScreen.Width),
																																								 objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 0, 
																																																						 Scene.ViewDefault.ViewPortScreen.Height)),
																												new Vector2D(30, 0), "Laser"), 
																				Configuration.TimeSpanPlayerLaserUpdate);
						}
		}
	}
}
