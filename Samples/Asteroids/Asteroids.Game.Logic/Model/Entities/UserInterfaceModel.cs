using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface;

namespace Bau.Libraries.Asteroids.Game.Logic.Model.Entities
{
	/// <summary>
	///		Clase para el manejo del interface de usuario
	/// </summary>
	public class UserInterfaceModel : AbstractActorModel
	{
		public UserInterfaceModel(IScene objScene, ScoresModel objScores, TimeSpan tsBetweenUpdate) 
								: base(objScene, tsBetweenUpdate, new GameObjectDimensions(0, 0))
		{ Scores = objScores;
		}

		/// <summary>
		///		Inicializa los datos del actor
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ int intLeftLabel = (int) Scene.ViewDefault.ViewPortScreen.Width - 200;

				// Añade los textos fijos
					base.AddText("Font", "Puntos", 10, 10);
					ScoreLabel = base.AddText("Font", $"{Scores.Score:#,##0}", 70, 10);
					base.AddText("Font", "Energia", intLeftLabel - 400, 10);
					base.AddText("Font", "Vidas", intLeftLabel, 10);
					LifesLabel = base.AddText("Font", $"{Scores.Lives}", intLeftLabel + 60, 10);
				// Crea la barra de progreso
					EnergyProgress = CreateBarProgress(objContext, new Rectangle(intLeftLabel - 335, 5, 200, 24));
		}

		/// <summary>
		///		Crea una barra de progreso
		/// </summary>
		private ProgressBarControl CreateBarProgress(IGameContext objContext, Rectangle rctPosition)
		{ ProgressBarControl objProgress = new ProgressBarControl(rctPosition, TimeSpan.FromMilliseconds(40), 0);
			SpriteSheetContent objSpriteSheet = objContext.GameController.ContentController.GetContent("Controls") as SpriteSheetContent;

				// Añade el fondo
					objProgress.Background = new SpriteModel(null, objSpriteSheet.ImageKey, TimeSpan.Zero, 0, 0,
																									 objSpriteSheet.SearchFrames("ProgressBarBackground").Rectangles[0],
																									 null, 0);
					objProgress.Bar = new SpriteModel(null, objSpriteSheet.ImageKey, TimeSpan.Zero, 1, 1,
																						objSpriteSheet.SearchFrames("ProgressBar").Rectangles[0],
																						null, 1);
				// Añade la barra de progreso a la colección de sprites
					AddControl(objProgress);
				// Devuelve la barra de progreso
					return objProgress;
		}


		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia el sonido
				UpdateSounds(objContext);
			// Actualiza la puntuación
				UpdateScores(objContext);
			// Muestra la puntuación
				LifesLabel.Text = $"{Scores.Lives}";
				ScoreLabel.Text = $"{Scores.Score:#,##0}";
			// Cambia la barra de progreso
				EnergyProgress.Percent = Scores.Energy;
		}

		/// <summary>
		///		Modifica los parámeros de sonido
		/// </summary>
		private void UpdateSounds(IGameContext objContext)
		{	if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.Escape))
				objContext.GameController.SceneController.SetScene(new Scenes.MainMenuScene());
			if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.F9))
				objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects = !objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects;
			if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.F8))
				{ // Cambia el parámetro
						objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic = !objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic;
					// Toca o detiene la música
						if (objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic)
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(objContext.GameController.MainManager.GameParameters.Configuration.ActualSong);
						else
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Stop();
				}
		}

		/// <summary>
		///		Modifica las puntuaciones a partir de los mensajes del juego
		/// </summary>
		private void UpdateScores(IGameContext objContext)
		{ List<Messages.InformationMessage> objColKillMessages = objContext.GameController.EventsManager.Dequeue<Messages.InformationMessage>();

				// Asigna la puntuación
					foreach (Messages.InformationMessage objKillMessage in objColKillMessages)
						switch (objKillMessage.Type)
							{	case Messages.InformationMessage.MessageMode.EnemyKill:
										Scores.Score += objKillMessage.Score;
									break;
								case Messages.InformationMessage.MessageMode.PlayerKill:
										Scores.Energy--;
										Scores.Score -= objKillMessage.Score;
										if (Scores.Energy < 0)
											objContext.GameController.SceneController.SetScene(new Scenes.GameOverScene());
									break;
							}
		}

		/// <summary>
		///		Puntuaciones
		/// </summary>
		private ScoresModel Scores { get; }

		/// <summary>
		///		Etiqueta con el número de vidas
		/// </summary>
		private TextModel LifesLabel { get; set; }

		/// <summary>
		///		Etiqueta con la puntuación
		/// </summary>
		private TextModel ScoreLabel { get; set; }

		/// <summary>
		///		Barra de progreso con la energía
		/// </summary>
		private ProgressBarControl EnergyProgress { get; set; }
	}
}
