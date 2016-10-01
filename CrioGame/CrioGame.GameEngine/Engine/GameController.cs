using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Motor del juego
	/// </summary>
	public class GameController : Common.Interfaces.IGameEngineManager
	{
		public GameController(Common.ICrioController objMainManager)
		{ MainManager = objMainManager;
			GameLoopController = new GameLoopController(this);
			GameContext = new GameContext(this);
			ContentController = new GameRepository("Content");
			EventsManager = new	EventsManager();
			SceneController = new	SceneController(this);
		}

		/// <summary>
		///		Motor principal
		/// </summary>
		public Common.ICrioController MainManager { get; }

		/// <summary>
		///		Contexto del juego
		/// </summary>
		public IGameContext GameContext { get; }

		/// <summary>
		///		Bucle de juego
		/// </summary>
		public IGameLoopController GameLoopController { get; }

		/// <summary>
		///		Controlador de contenido (diccionario de imágenes, sonidos... que se deben cargar)
		/// </summary>
		public IGameContentDictionary ContentController { get; }

		/// <summary>
		///		Manejador de eventos
		/// </summary>
		public IGameEventsController EventsManager { get; }

		/// <summary>
		///		Controlador de escenas
		/// </summary>
		public ISceneController SceneController { get; }
	}
}
