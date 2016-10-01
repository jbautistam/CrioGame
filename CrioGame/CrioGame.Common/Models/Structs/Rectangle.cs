using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Rectángulo
	/// </summary>
	public struct Rectangle
	{
		public Rectangle(float fltX = 0, float fltY = 0, float fltWidth = 0, float fltHeight = 0)
		{ X = fltX;
			Y = fltY;
			Width = fltWidth;
			Height = fltHeight;
		}

		public Rectangle(Vector2D objPosition, Size2D objSize)
		{ X = objPosition.X;
			Y = objPosition.Y;
			Width = objSize.Width;
			Height = objSize.Height;
		}

		/// <summary>
		///		Comprueba si un punto está dentro del rectángulo
		/// </summary>
		public bool HasPoint(Vector2D pntPoint)
		{ return pntPoint.X >= Left && pntPoint.X <= Right &&
						 pntPoint.Y >= Top && pntPoint.Y <= Bottom;
		}

		/// <summary>
		///		Posición X
		/// </summary>
		public float X { get; set; }

		/// <summary>
		///		Posición Y
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		///		Ancho
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		///		Alto
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		///		Coordenada izquierda
		/// </summary>
		public float Left 
		{ get { return X; } 
		}

		/// <summary>
		///		Coordenada derecha
		/// </summary>
		public float Right
		{ get { return X + Width; }
		}

		/// <summary>
		///		Coordenada superior
		/// </summary>
		public float Top
		{ get { return Y;}
		}

		/// <summary>
		///		Coordenada inferior
		/// </summary>
		public float Bottom
		{ get { return Y + Height; }
		}

		/// <summary>
		///		Indica si tiene definido un tamaño
		/// </summary>
		public bool IsEmpty
		{ get { return Width == 0 || Height == 0; }
		}

		/// <summary>
		///		Cadena de depuración
		/// </summary>
		public string DebugString
		{ get { return $"[{Top},{Left} - {Bottom},{Right}]"; }
		}
	}
}
