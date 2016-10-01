using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine
{
	/// <summary>
	///		Controlador de pantalla
	/// </summary>
	public interface IScreenController
	{
		/// <summary>
		///		Cambia el tamaño de la ventana
		/// </summary>
		void SetWindowsSize(int intWidth, int intHeight);

		/// <summary>
		///		Cambia la ventana a pantalla completa
		/// </summary>
		void SetWindowsFullScreen();

		/// <summary>
		///		Rectángulo de la vista
		/// </summary>
		Models.Structs.Rectangle ViewPort { get; }
	}
}
