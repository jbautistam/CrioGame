using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Círculo
	/// </summary>
	public struct Circle
	{
		public Circle(float fltX, float fltY, float fltRadius)
		{ Center = new Vector2D(fltX, fltY);
			Radius = fltRadius;
		}

		/// <summary>
		///		Centro del círculo
		/// </summary>
		public Vector2D Center { get; set; }

		/// <summary>
		///		Radio del círculo
		/// </summary>
		public float Radius { get; set; }

		/// <summary>
		///		Indica si el círculo está vacío
		/// </summary>
		public bool IsEmpty 
		{ get { return Radius == 0; }
		}

		/// <summary>
		///		Punto más a la izquierda
		/// </summary>
		public float Left 
		{ get { return Center.X - Radius; }
		}

		/// <summary>
		///		Punto más a la derecha
		/// </summary>
		public float Right
		{ get { return Center.X + Radius; }
		}

		/// <summary>
		///		Punto superior
		/// </summary>
		public float Top
		{ get { return Center.Y - Radius; }
		}

		/// <summary>
		///		Punto inferior
		/// </summary>
		public float Bottom
		{ get { return Center.Y + Radius; }
		}
	}
}
