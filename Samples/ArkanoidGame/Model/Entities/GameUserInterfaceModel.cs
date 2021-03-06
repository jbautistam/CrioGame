﻿using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace ArkanoidGame.Model.Entities
{
	/// <summary>
	///		Clase para el manejo del interface de usuario
	/// </summary>
	public class GameUserInterfaceModel : AbstractActorModel
	{ // Enumerados privados
			/// <summary>
			///		Tipo de escena interna durante el juego
			/// </summary>
			private enum SceneType
				{ 
					/// <summary>Se está jugando</summary>
					Playing,
					/// <summary>Esperando que se pulse una tecla</summary>
					Waiting
				}

		public GameUserInterfaceModel(IView objView, IView objViewGame, ScoresModel objScore, 
															PaddleModel objPaddle, BallModel objBall, int intBricks,
															TimeSpan tsBetweenUpdate) : base(objView, tsBetweenUpdate, new GameObjectDimensions(0, 0))
		{ ViewGame = objViewGame;
			Paddle = objPaddle;
			Balls = new List<BallModel> { objBall };
			Scores = objScore;
			Bricks = intBricks;
			Scene = SceneType.Playing;
		}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Añade un fondo
				View.AddEntity(Scenes.GameScene.Layer.Background.ToString(),
											 new BackgroundEntity(View, "MainBackground", 0));
			// Inicializa los textos
				base.AddText("Font", "Nivel", 20, 20);
				base.AddText("Font", Scores.Level.ToString(), 20, 40);
				base.AddText("Font", "Puntos", 20, 60);
				ScoreLabel = base.AddText("Font", Scores.Score.ToString(), 20, 80);
				base.AddText("Font", "Ladrillos", 20, 100);
				BricksLabel = base.AddText("Font", Bricks.ToString(), 20, 120);
				base.AddText("Font", "Vidas", 20, 140);
				LifesLabel = base.AddText("Font", Scores.Level.ToString(), 20, 160);
		}

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Actualiza el interface de usuario
				UpdateUserInterface(objContext);
			// Actualiza la puntuación
				UpdateScores(objContext);
			// Cambia las etiquetas
				LifesLabel.Text = Scores.Lives.ToString();
				ScoreLabel.Text = Scores.Score.ToString();
				BricksLabel.Text = Bricks.ToString();
		}

		/// <summary>
		///		Controla el interface de usuario
		/// </summary>
		private void UpdateUserInterface(IGameContext objContext)
		{	// Sale de la aplicación
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Escape))
					objContext.GameController.SceneController.SetScene(new Scenes.MainMenuScene());
			// Activa los efectos de sonido
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.F9))
					objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects = !objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects;
			// Activa la música de fondo
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.F8))
					{ // Cambia el parámetro
							objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic = !objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic;
						// Toca o detiene la música
							if (objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic)
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(objContext.GameController.MainManager.GameParameters.Configuration.ActualSong);
							else
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Stop();
					}
			// Si estamos en una escena de espera, espera la pulsación de una tecla
				switch (Scene)
					{	case SceneType.Waiting:
								if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyKey() ||
										objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyMouseButton())
									SetScene(objContext, SceneType.Playing);
							break;
					}
		}

		/// <summary>
		///		Modifica las puntuaciones a partir de los mensajes del juego
		/// </summary>
		private void UpdateScores(IGameContext objContext)
		{ List<Messages.InformationMessage> objColMessages = objContext.GameController.EventsManager.Dequeue<Messages.InformationMessage>();

				// Asigna la puntuación
					foreach (Messages.InformationMessage objMessage in objColMessages)
						switch (objMessage.Type)
							{	case Messages.InformationMessage.InformationType.AddScore:
										Scores.Score += objMessage.Score;
									break;
								case Messages.InformationMessage.InformationType.KillBrick:
										Scores.Score += objMessage.Score;
										Bricks--;
										if (Bricks <= 0)
											objContext.GameController.SceneController.SetScene(new Scenes.MainNextLevel(Scores));
									break;
								case Messages.InformationMessage.InformationType.CreateBalls:
										CreateBalls(objContext, objMessage.Score);
									break;
								case Messages.InformationMessage.InformationType.KillBall:
										KillBall(objContext, objMessage.Tag as BallModel);
									break;
								case Messages.InformationMessage.InformationType.KillPlayer:
										KillPlayer(objContext);
									break;
								case Messages.InformationMessage.InformationType.NewLife:
										Scores.Lives++;
									break;
							}
		}

		/// <summary>
		///		Crea una serie de pelotas
		/// </summary>
		private void CreateBalls(IGameContext objContext, int intBalls)
		{ for (int intIndex = 0; intIndex < intBalls; intIndex++)
				{ BallModel objBall = new BallModel(ViewGame, 
																						new GameObjectDimensions(Balls[0].Dimensions.Position.X, 
																																		 Balls[0].Dimensions.Position.Y),
																						new Vector2D(objContext.MathHelper.Random(-3, 3), -5), 
																						Balls[0].TimeBetweenUpdate);

						// Añade la pelota al juego
							ViewGame.AddEntity(Scenes.GameScene.LayerGame, objBall);
						// Añade la pelota a la colección
							Balls.Add(objBall);
				}
		}

		/// <summary>
		///		Realiza las modificaciones que implican eliminar una pelota
		/// </summary>
		private void KillBall(IGameContext objContext, BallModel objBall)
		 {	if (Balls.Count > 1)
				{ // Elimina la pelota de la colección
						if (objBall != null)
							{ // Indica que ya no está activa
									objBall.Active = false;
								// Elimina la pelota de la colección
									Balls.Remove(objBall);
							}
				}
			else // ... sólo quedaba una pelota
				KillPlayer(objContext);
		}

		/// <summary>
		///		Realiza las modificaciones que implican eliminar al jugador
		/// </summary>
		private void KillPlayer(IGameContext objContext)
		{	if (Scene != SceneType.Waiting)
				{ // Cambia el modo de mostrar la escena
						SetScene(objContext, SceneType.Waiting);
					// Decrementa una vida
						Scores.Lives--;
					// Si ha perdido, cambia la escena
						if (Scores.Lives < 0)
							objContext.GameController.SceneController.SetScene(new Scenes.GameOverScene());
				}
		}

		/// <summary>
		///		Cambia la escena
		/// </summary>
		private void SetScene(IGameContext objContext, SceneType intNewScene)
		{ // Trata la nueva escena
				switch (intNewScene)
					{	case SceneType.Waiting:
								// Crea un mensaje
									Message = new TextModel(this, "Font", "Pulse una tecla",
																					(int) (ViewGame.ViewPortScreen.Width / 2 - 200),
																					(int) (ViewGame.ViewPortScreen.Height / 2),
																					ColorEngine.Red);
								// Muestra el mensaje
									ViewGame.AddEntity(Scenes.GameScene.LayerGame, Message);
							break;
						case SceneType.Playing:
								// Quita el mensaje
									if (Message != null)
										Message.Active = false;
								// Inicializa el jugador y la pelota
									Paddle.Reset();
									for (int intIndex = Balls.Count - 1; intIndex >= 1; intIndex--)
										{ Balls[intIndex].Active = false;
											Balls.RemoveAt(intIndex);
										}
									Balls[0].Reset();
							break;
					}
			// Cambia a la nueva escena
				Scene = intNewScene;
		} 

		/// <summary>
		///		Puntuación
		/// </summary>
		internal ScoresModel Scores { get; set; }

		/// <summary>
		///		Etiqueta con el número de vidas
		/// </summary>
		private TextModel LifesLabel { get; set; }

		/// <summary>
		///		Etiqueta con la puntuación
		/// </summary>
		private TextModel ScoreLabel { get; set; }

		/// <summary>
		///		Etiqueta con el número de ladrillos
		/// </summary>
		private TextModel BricksLabel { get; set; }
				
		/// <summary>
		///		Paddle
		/// </summary>
		private PaddleModel Paddle { get; }

		/// <summary>
		///		Pelota
		/// </summary>
		private List<BallModel> Balls { get; }

		/// <summary>
		///		Número de ladrillos
		/// </summary>
		private int Bricks { get; set; }

		/// <summary>
		///		Escena actual
		/// </summary>
		private SceneType Scene { get; set; }

		/// <summary>
		///		Mensaje actual
		/// </summary>
		private TextModel Message { get; set; }

		/// <summary>
		///		Vista donde se desarrolla el juego
		/// </summary>
		private	IView ViewGame { get; }
	}
}
