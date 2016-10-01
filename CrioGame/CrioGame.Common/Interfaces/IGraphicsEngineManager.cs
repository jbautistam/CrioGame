using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Common.Interfaces
{
	/// <summary>
	///		Interface que debe cumplir el manager de gráficos (es decir, la librería que dibuja los gráficos)
	/// </summary>
	public interface IGraphicsEngineManager
	{
		/// <summary>
		///		Inicializa el controlador
		/// </summary>
		void Initialize(IGameEngineManager objGameEngine);

		/// <summary>
		///		Arranca el juego
		/// </summary>
		void Start(int intWindowsWidth = 0, int intWindowsHeight = 0);

		/// <summary>
		///		Detiene el juego
		/// </summary>
		void Stop();

		/// <summary>
		///		Controlador de pantalla
		/// </summary>
		IScreenController ScreenController { get; }

		/// <summary>
		///		Controlador de contenido
		/// </summary>
		IContentManager ContentController { get; }

		/// <summary>
		///		Controlador de dibujo por lotes
		/// </summary>
		ISpriteBatch SpriteBatch { get; }

		/// <summary>
		///		Controlador de sonido
		/// </summary>
		ISoundController SoundController { get; }

		/// <summary>
		///		Controlador de dispositivos de entrada
		/// </summary>
		IInputController InputManager { get; }

		/// <summary>
		///		Indica si se ha inicializado el motor y es seguro acceder a sus datos
		/// </summary>
		bool IsStarted { get; }
	}
}
