using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Clase con los datos de tamaño
	/// </summary>
	public struct Size2D
	{
		public Size2D(float fltWidth = 0, float fltHeight = 0)
		{ Width = fltWidth;
			Height = fltHeight;
		}

		/// <summary>
		///		Ancho
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		///		Alto
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		///		Indica si tiene definido un tamaño
		/// </summary>
		public bool HasSize
		{ get { return Width > 0 && Height > 0; }
		}
	}
}
