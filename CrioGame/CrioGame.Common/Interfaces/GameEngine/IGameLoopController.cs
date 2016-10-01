using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Controlador del juego
	/// </summary>
	public interface IGameLoopController
	{
		/// <summary>
		///		Comienza la ejecución del juego
		/// </summary>
		void Start(int intWindowsWidth = 0, int intWindowsHeight = 0);

		/// <summary>
		///		Detiene el juego
		/// </summary>
		void Stop();

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		void Initialize();

		/// <summary>
		///		Carga el contenido
		/// </summary>
		void LoadContent();

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		void UnloadContent();

		/// <summary>
		///		Modifica el juego
		/// </summary>
		void Update(TimeSpan tsActual);

		/// <summary>
		///		Dibuja el juego
		/// </summary>
		void Draw(TimeSpan tsActual);
	}
}
