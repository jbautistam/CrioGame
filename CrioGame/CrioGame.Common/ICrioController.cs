using System;

namespace Bau.Libraries.CrioGame.Common
{
	/// <summary>
	///		Interface de controlador para el motor gráfico
	/// </summary>
	public interface ICrioController : IDisposable
	{
		/// <summary>
		///		Inicializa el controlador
		/// </summary>
		void Initialize(Interfaces.IGameParameters objGameParameters);

		/// <summary>
		///		Arranca el juego
		/// </summary>
		void Start(int intWindowsWidth = 0, int intWindowsHeight = 0);

		/// <summary>
		///		Detiene el juego
		/// </summary>
		void Stop();

		/// <summary>
		///		Motor del juego
		/// </summary>
		Interfaces.IGameEngineManager GameEngine { get; }

		/// <summary>
		///		Motor gráfico
		/// </summary>
		Interfaces.IGraphicsEngineManager GraphicsEngine { get; }

		/// <summary>
		///		Parámetros del juego
		/// </summary>
		Interfaces.IGameParameters GameParameters { get; }
	}
}
