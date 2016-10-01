using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces
{
	/// <summary>
	///		Parámetros del juego
	/// </summary>
	public interface IGameParameters
	{
		/// <summary>
		///		Configuración del juego
		/// </summary>
		Parameters.IGameConfiguration Configuration { get; }
	}
}
