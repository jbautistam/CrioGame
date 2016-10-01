using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine
{
	/// <summary>
	///		Controlador de sonido
	/// </summary>
	public interface ISoundController
	{
		/// <summary>
		///		Toca un sonido
		/// </summary>
		void Play(string strKey);

		/// <summary>
		///		Detiene el sonido
		/// </summary>
		void Stop();
	}
}
