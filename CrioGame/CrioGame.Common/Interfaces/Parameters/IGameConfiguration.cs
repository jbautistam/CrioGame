using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.Parameters
{
	/// <summary>
	///		Configuración del juego
	/// </summary>
	public interface IGameConfiguration
	{
		/// <summary>
		///		Canción actual
		/// </summary>
		string ActualSong { get; set; }

		/// <summary>
		///		Volumen del sonido
		/// </summary>
		int Volume { get; set; }

		/// <summary>
		///		Indica si se deben tocar los efectos de sonido
		/// </summary>
		bool PlayEffects { get; set; }

		/// <summary>
		///		Indica si se debe tocar la música
		/// </summary>
		bool PlayMusic { get; set; }
	}
}
