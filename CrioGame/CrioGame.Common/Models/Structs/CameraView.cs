using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Clase con los datos de una cámara / vista
	/// </summary>
	public class CameraView
	{
		public CameraView(Rectangle rctPercentScreen, Rectangle rctWorld, Rectangle rctCamera)
		{ ViewPortPercentScreen = rctPercentScreen;
			ViewPortWorld = rctWorld;
			ViewPortCamera = rctCamera;
		}

		/// <summary>
		///		Rectángulo de la pantalla: lo trata como porcentaje, es decir, el porcentaje donde se
		///	coloca la vista en la pantalla (así evitamos manipularlo cuando se redimensiona la
		///	ventana)
		/// </summary>
		public Rectangle ViewPortPercentScreen { get; set; }

		/// <summary>
		///		Coordenadas del mundo
		/// </summary>
		public Rectangle ViewPortWorld { get; set; }

		/// <summary>
		///		Coordenadas de visualización de la cámara
		/// </summary>
		public Rectangle ViewPortCamera { get; set; }
	}
}
