using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Estructura para coordenadas polares
	/// </summary>
	public struct Polar2D
	{
		public Polar2D(float fltAngle, float fltLength)
		{ Angle = fltAngle - (float) (Math.PI / 2);
			Length = fltLength;
		}

		/// <summary>
		///		Tranforma las coordenadas polares en un <see cref="Vector2D"/>
		/// </summary>
		public Vector2D ToVector()
		{ if (Length == 0) // ... nos evitamos el cálculo de los senos / cosenos
				return new Vector2D();
			else
				return new Vector2D((float) (Length * Math.Cos(Angle)), (float) (Length * Math.Sin(Angle)));
		}

		/// <summary>
		///		Angulo
		/// </summary>
		public float Angle { get; set; }

		/// <summary>
		///		Longitud
		/// </summary>
		public float Length { get; set; }
	}
}
