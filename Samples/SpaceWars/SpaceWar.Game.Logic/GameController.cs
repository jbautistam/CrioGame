using System;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.GameEngine;

namespace Bau.Libraries.SpaceWar.Game.Logic
{
	/// <summary>
	///		Controlador del juego
	/// </summary>
  public class GameController 
  {
		public GameController(string strPathData)
		{ PathData = strPathData;
		}

		/// <summary>
		///		Arranca el juego
		/// </summary>
		public void Start(IGraphicsEngineManager objGraphicsEngine)
		{ using (CrioEngine objEngine = new CrioEngine(objGraphicsEngine))
				{ // Inicializa el motor de juego
						objEngine.Initialize(new Parameters.GameParameters());
					// Desactiva la música
						objEngine.GameParameters.Configuration.PlayMusic = false;
						objEngine.GameParameters.Configuration.PlayEffects = false;
					// Asigna la música de fondo
						objEngine.GameParameters.Configuration.ActualSong = "GameSong";
					// Añade el contenido
						objEngine.GameEngine.ContentController.LoadResources(System.IO.File.ReadAllText(System.IO.Path.Combine(PathData, "Resources.txt")));
					// Crea la escena
						objEngine.GameEngine.SceneController.SetScene(new Model.Scenes.MainMenuScene());
					// Ejecuta el motor
						objEngine.Start();
				}
		}

		/// <summary>
		///		Directorio donde se encuentran los datos e imágenes
		/// </summary>
		private string PathData { get; }
  }
}
