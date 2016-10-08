using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Mines.Logic.Model.Entities
{
	/// <summary>
	///		Clase para el manejo del interface de usuario
	/// </summary>
	public class UserInterfaceModel : AbstractActorModel
	{
		public UserInterfaceModel(IView objView, ScoresModel objScores, TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, new Bau.Libraries.CrioGame.Common.Models.Structs.GameObjectDimensions(0, 0))
		{ Scores = objScores;
		}

		/// <summary>
		///		Inicializa los datos del actor
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ int intLeftLabel = (int) View.ViewPortScreen.Width - 200;

				// Añade los textos fijos
					base.AddText("Font", "Puntos", 10, 10);
					base.AddText("Font", "Vidas", intLeftLabel, 10);
				// Añade las etiquetas de los textos variables
					ScoreLabel = base.AddText("Font", Scores.Score.ToString(), 70, 12);
					LifesLabel = base.AddText("Font", Scores.Lifes.ToString(), intLeftLabel + 60, 12);
		}

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{	// Cambia el sonido
				UpdateSounds(objContext);
			// Actualiza la puntuación
				UpdateScores(objContext);
			// Cambia las etiquetas
				LifesLabel.Text = Scores.Lifes.ToString();
				ScoreLabel.Text = Scores.Score.ToString();
		}

		/// <summary>
		///		Modifica los parámeros de sonido
		/// </summary>
		private void UpdateSounds(IGameContext objContext)
		{	if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Escape))
				objContext.GameController.MainManager.Stop();
			if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.F9))
				objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects = !objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects;
			if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.F8))
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
		{ List<Messages.EnemyKillMessage> objColKillMessages = objContext.GameController.EventsManager.Dequeue<Messages.EnemyKillMessage>();

				// Asigna la puntuación
					foreach (Messages.EnemyKillMessage objKillMessage in objColKillMessages)
						Scores.Score += objKillMessage.Score;
		}

		/// <summary>
		///		Panel de puntuaciones
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
