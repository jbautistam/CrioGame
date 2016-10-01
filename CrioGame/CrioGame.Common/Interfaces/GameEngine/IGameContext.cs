using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Contexto del juego
	/// </summary>
	public interface IGameContext
	{
		/// <summary>
		///		Actualiza el contexto tras la modificación
		/// </summary>
		void EndUpdate(TimeSpan tsActual);

		/// <summary>
		///		Actualiza el contexto tras el dibujo
		/// </summary>
		void EndDraw(TimeSpan tsActual);

		/// <summary>
		///		Controlador
		/// </summary>
		IGameEngineManager GameController { get; }

		/// <summary>
		///		Herramientas matemáticas de ayuda
		/// </summary>
		Tools.MathHelper MathHelper { get; }

		/// <summary>
		///		Tiempo actual
		/// </summary>
		TimeSpan ActualTime { get; }

		/// <summary>
		///		Tiempo de última modificación
		/// </summary>
		TimeSpan LastUpdateTime { get; }
	}
}
