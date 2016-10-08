using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace MinesGameWin2D.Model.Entities
{
	/// <summary>
	///		Modelo con los datos del jugador
	/// </summary>
	public class PlayerModel : AbstractActorModel
	{ // Variables privadas
			private int intSpeed = 8;
			private TimeSpan tsFireSpawnTime, tsPreviousFireTime;

		public PlayerModel(IView objView, GameObjectDimensions objDimensions) : base(objView, TimeSpan.Zero, objDimensions)
		{ CollisionEvaluator = new CollisionTargets(this, (int) GroupGameObjects.Player, (int) GroupGameObjects.Enemy);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Inicializa los parámetros de disparo
				tsFireSpawnTime = Configuration.TimeSpanPlayerLaserFire;
				tsPreviousFireTime = TimeSpan.Zero;
			// Añade la animación
				AddAnimation("Player", "Default", "Default", "PlayerImage", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ float fltNewX = Dimensions.Position.X, fltNewY = Dimensions.Position.Y;

			// Controla los movimientos con el teclado
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Up))
					fltNewY -= intSpeed;
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Down))
					fltNewY += intSpeed;
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Right))
					fltNewX += intSpeed;
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Left))
					fltNewX -= intSpeed;
			// Normaliza las coordenadas
				Dimensions.MoveTo(fltNewX, fltNewY);
				Dimensions.ClampToView(View.ViewPortScreen.Width, View.ViewPortScreen.Height);
			// Controla el disparo
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Space) &&
						objContext.MathHelper.IsElapsed(objContext.ActualTime, tsFireSpawnTime, ref tsPreviousFireTime))
					{	// Guarda el momento del disparo
							tsPreviousFireTime = objContext.ActualTime;
						// Sonido para el láser
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(Configuration.LaserSound);
						// Crea el láser
							View.AddEntity(Configuration.LayerGame, 
														 new LaserModel(View, 
																						(int) GroupGameObjects.Player,
																						(int) GroupGameObjects.Enemy,
																						new GameObjectDimensions(objContext.MathHelper.Clamp(Dimensions.Position.X + Dimensions.ScaledDimensions.Width, 0, 
																																																 View.ViewPortScreen.Width),
																																		 objContext.MathHelper.Clamp(Dimensions.Position.Y + Dimensions.ScaledDimensions.Height / 2, 0, 
																																																 View.ViewPortScreen.Height)),
																						new Vector2D(30, 0), "Laser", Configuration.TimeSpanPlayerLaserUpdate));
					}
		}
	}
}
