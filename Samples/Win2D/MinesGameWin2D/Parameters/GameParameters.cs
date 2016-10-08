using System;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.Common.Interfaces.Parameters;

namespace MinesGameWin2D.Parameters
{
	/// <summary>
	///		Parámetros del juego
	/// </summary>
	public class GameParameters : IGameParameters
	{
		/// <summary>
		///		Configuración del juego
		/// </summary>
		public IGameConfiguration Configuration { get; } = new GameConfiguration();
	}
}
