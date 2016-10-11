using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
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
					base.AddText("Font", "Vidas", intLeftLabel, 10);
					LifesLabel = base.AddText("Font", $"{Scores.Lives}", intLeftLabel + 60, 10);
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
		{ List<Messages.InformationMessage> objColMessages = objContext.GameController.EventsManager.Dequeue<Messages.InformationMessage>();

				// Asigna la puntuación
					foreach (Messages.InformationMessage objKillMessage in objColMessages)
						switch (objKillMessage.Type)
							{	case Messages.InformationMessage.MessageMode.EnemyKill:
										Scores.Score += objKillMessage.Score;
										Scores.Ships--;
										if (Scores.Ships <= 0)
											objContext.GameController.SceneController.SetScene(new Scenes.MainNextLevel(Scores));
									break;
								case Messages.InformationMessage.MessageMode.PlayerKill:
										Scores.Lives--;
										Scores.Score -= objKillMessage.Score;
										if (Scores.Lives < 0)
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
	}
}
