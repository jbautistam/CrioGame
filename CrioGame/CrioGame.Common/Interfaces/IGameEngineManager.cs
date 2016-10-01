using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces
{
	/// <summary>
	///		Interface para el controlador del motor de juego
	/// </summary>
	public interface IGameEngineManager
	{
		/// <summary>
		///		Controlador del juego principal
		/// </summary>
		ICrioController MainManager { get; }

		/// <summary>
		///		Contexto del juego
		/// </summary>
		GameEngine.IGameContext GameContext { get; }

		/// <summary>
		///		Controlador del bucle de juego
		/// </summary>
		GameEngine.IGameLoopController GameLoopController { get; }

		/// <summary>
		///		Controlador de contenido
		/// </summary>
		GameEngine.IGameContentDictionary ContentController { get; }

		/// <summary>
		///		Controlador de eventos y mensajes
		/// </summary>
		GameEngine.IGameEventsController EventsManager { get; }

		/// <summary>
		///		Controlador de escenas
		/// </summary>
		GameEngine.ISceneController SceneController { get; }
	}
}
