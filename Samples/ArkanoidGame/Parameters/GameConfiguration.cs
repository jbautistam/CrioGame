using System;

using Bau.Libraries.CrioGame.Common.Interfaces.Parameters;

namespace ArkanoidGame.Parameters
{
	/// <summary>
	///		Configuración del juego
	/// </summary>
	public class GameConfiguration : IGameConfiguration
	{
		/// <summary>
		///		Canción actual
		/// </summary>
		public string ActualSong { get; set; }

		/// <summary>
		///		Indica si se deben tocar los efectos de sonido
		/// </summary>
		public bool PlayEffects { get; set; } = true;

		/// <summary>
		///		Indica si se debe tocar la música
		/// </summary>
		public bool PlayMusic { get; set; } = true;

		/// <summary>
		///		Volumen de la música y los efectos de sonido
		/// </summary>
		public int Volume { get; set; }= 3;
	}
}
