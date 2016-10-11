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
		public UserInterfaceModel(IScene objScene, int intScore, int intLifes, TimeSpan tsBetweenUpdate) 
							: base(objScene, tsBetweenUpdate, new CrioGame.Common.Models.Structs.GameObjectDimensions(0, 0))
		{ Score = intScore;
			Lifes = intLifes;
		}

		/// <summary>
		///		Inicializa los datos del actor
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ int intLeftLabel = (int) Scene.ViewDefault.ViewPortScreen.Width - 200;

				// Añade los textos fijos
					base.AddText("Font", "Puntos", 10, 10);
					base.AddText("Font", "Vidas", intLeftLabel, 10);
				// Añade las etiquetas de los textos variables
					ScoreLabel = base.AddText("Font", Score.ToString(), 70, 12);
					LifesLabel = base.AddText("Font", Lifes.ToString(), intLeftLabel + 60, 12);
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
				LifesLabel.Text = Lifes.ToString();
				ScoreLabel.Text = Score.ToString();
		}

		/// <summary>
		///		Modifica los parámeros de sonido
		/// </summary>
		private void UpdateSounds(IGameContext objContext)
		{	if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(CrioGame.Common.Enums.Keys.Escape))
				objContext.GameController.MainManager.Stop();
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
		{ List<Messages.EnemyKillMessage> objColKillMessages = objContext.GameController.EventsManager.Dequeue<Messages.EnemyKillMessage>();

				// Asigna la puntuación
					foreach (Messages.EnemyKillMessage objKillMessage in objColKillMessages)
						Score += objKillMessage.Score;
		}

		/// <summary>
		///		Puntuación
		/// </summary>
		internal int Score { get; set; } = 0;

		/// <summary>
		///		Número de vidas
		/// </summary>
		internal int Lifes { get; set; } = 2;

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
