using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.ArkanoidGame.Logic.Messages;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
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

		public GameUserInterfaceModel(IScene objScene, IView objViewUserInterface, IView objViewGame, ScoresModel objScore, 
																	PaddleModel objPaddle, BallModel objBall, int intBricks) 
							: base(objScene, new GameObjectDimensions(0, 0))
		{ ViewUserInterface = objViewUserInterface;
			ViewGame = objViewGame;
			Paddle = objPaddle;
			Balls = new List<BallModel> { objBall };
			Scores = objScore;
			Bricks = intBricks;
			ScenePart = SceneType.Playing;
		}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ // Añade un fondo
				Scene.Map.AddGameEntity(ViewUserInterface, Scenes.GameScene.Layer.Background.ToString(),
																new BackgroundEntity(Scene.ViewDefault, "MainBackground", 0));
			// Inicializa los textos
				AddText("Font", "Nivel", 20, 20);
				AddText("Font", Scores.Level.ToString(), 20, 40);
				AddText("Font", "Puntos", 20, 60);
				ScoreLabel = AddText("Font", Scores.Score.ToString(), 20, 80);
				AddText("Font", "Ladrillos", 20, 100);
				BricksLabel = base.AddText("Font", Bricks.ToString(), 20, 120);
				AddText("Font", "Vidas", 20, 140);
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
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.Escape))
					objContext.GameController.SceneController.SetScene(new Scenes.MainMenuScene(Configuration.LevelsRepository));
			// Activa los efectos de sonido
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.F9))
					objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects = !objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects;
			// Activa la música de fondo
				if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.F8))
					{ // Cambia el parámetro
							objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic = !objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic;
						// Toca o detiene la música
							if (objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic)
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(objContext.GameController.MainManager.GameParameters.Configuration.ActualSong);
							else
								objContext.GameController.MainManager.GraphicsEngine.SoundController.Stop();
					}
			// Si estamos en una escena de espera, espera la pulsación de una tecla
				switch (ScenePart)
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
					foreach (InformationMessage objMessage in objColMessages)
						switch (objMessage.Type)
							{	case InformationMessage.InformationType.AddScore:
										Scores.Score += objMessage.Score;
									break;
								case InformationMessage.InformationType.KillBrick:
										Scores.Score += objMessage.Score;
										Bricks--;
										if (Bricks <= 0)
											objContext.GameController.SceneController.SetScene(new Scenes.MainNextLevel(Scores));
									break;
								case InformationMessage.InformationType.CreateBalls:
										CreateBalls(objContext, objMessage.Score);
									break;
								case InformationMessage.InformationType.KillBall:
										KillBall(objContext, objMessage.Tag as BallModel);
									break;
								case InformationMessage.InformationType.KillPlayer:
										KillPlayer(objContext);
									break;
								case InformationMessage.InformationType.NewLife:
										Scores.Lives++;
									break;
							}
		}

		/// <summary>
		///		Crea una serie de pelotas
		/// </summary>
		private void CreateBalls(IGameContext objContext, int intBalls)
		{ for (int intIndex = 0; intIndex < intBalls; intIndex++)
				{ BallModel objBall = new BallModel(Scene, 
																						new GameObjectDimensions(Balls[0].Dimensions.Position.X, 
																																		 Balls[0].Dimensions.Position.Y),
																						new Vector2D(objContext.MathHelper.Random(-3, 3), -5));

						// Añade la pelota al juego
							Scene.Map.AddGameEntity(ViewGame, Scenes.GameScene.LayerGame, objBall);
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
									Scene.Map.RemoveGameEntity(objBall);
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
		{	if (ScenePart != SceneType.Waiting)
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
																					new GameObjectDimensions((int) (ViewGame.ViewPortScreen.Width / 2 - 200),
																																	 (int) (ViewGame.ViewPortScreen.Height / 2),
																																	 ColorEngine.Red));
								// Muestra el mensaje
									Scene.Map.AddGameEntity(ViewGame, Scenes.GameScene.LayerGame, Message);
							break;
						case SceneType.Playing:
								// Quita el mensaje
									if (Message != null)
										Scene.Map.RemoveGameEntity(Message);
								// Inicializa el jugador y la pelota
									Paddle.Reset();
									for (int intIndex = Balls.Count - 1; intIndex >= 1; intIndex--)
										{ Scene.Map.RemoveGameEntity(Balls[intIndex]);
											Balls.RemoveAt(intIndex);
										}
									Balls[0].Reset();
							break;
					}
			// Cambia a la nueva escena
				ScenePart = intNewScene;
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
		private SceneType ScenePart { get; set; }

		/// <summary>
		///		Mensaje actual
		/// </summary>
		private TextModel Message { get; set; }

		/// <summary>
		///		Vista de la interface de usuario
		/// </summary>
		private IView ViewUserInterface { get; }

		/// <summary>
		///		Vista donde se desarrolla el juego
		/// </summary>
		private	IView ViewGame { get; }
	}
}
