using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Dimensiones de un objeto del juego
	/// </summary>
	public class GameObjectDimensions
	{
		public GameObjectDimensions(float fltX, float fltY, float fltWidth = 0, float fltHeight = 0,
																float fltScale = 1, float fltAngle = 0, ColorEngine? clrColor = null,
																int intZOrder = 0)
		{ Position = new Rectangle(fltX, fltY, fltWidth, fltHeight);
			Scale = fltScale;
			Angle = fltAngle;
			Color = clrColor ?? ColorEngine.White;
			ZOrder = intZOrder;
		}

		/// <summary>
		///		Comprueba si un punto está en un rectángulo
		/// </summary>
		public bool HasPoint(Vector2D vctPosition)
		{ return HasPoint(vctPosition.X, vctPosition.Y);
		}

		/// <summary>
		///		Normaliza las posiciones en pantalla
		/// </summary>
		public void ClampToView(float fltWidth, float fltHeight)
		{ ClampToView(0, 0, fltWidth, fltHeight);
		}

		/// <summary>
		///		Normaliza las posiciones en pantalla
		/// </summary>
		public void ClampToView(float fltX, float fltY, float fltWidth, float fltHeight)
		{ float fltNewX = Clamp(Position.X, fltX, fltWidth - ScaledDimensions.Width);
			float fltNewY = Clamp(Position.Y, fltY, fltHeight - ScaledDimensions.Height);

				// Cambia la posición
					Position = new Rectangle(fltNewX, fltNewY, Position.Width, Position.Height);
		}

		/// <summary>
		///		Reduce 
		/// </summary>
		private float Clamp(float fltValue, float fltMinimum, float fltMaximum)
		{ if (fltValue < fltMinimum)
				return fltMinimum;
			else if (fltValue > fltMaximum)
				return fltMaximum;
			else
				return fltValue;
		}

		/// <summary>
		///		Comprueba si un punto está en un rectángulo
		/// </summary>
		public bool HasPoint(float fltX, float fltY)
		{ return fltX >= Position.X && fltX <= Position.X + ScaledDimensions.Width &&
						 fltY >= Position.Y && fltY <= Position.Y + ScaledDimensions.Height;
		}

		/// <summary>
		///		Comprueba si el objeto está dentro de un rectángulo
		/// </summary>
		public bool IsAtRectangle(Rectangle rctRectangle)
		{ return Position.X >= rctRectangle.Left && Position.X <= rctRectangle.Right &&
						 Position.Y >= rctRectangle.Top && Position.Y <= rctRectangle.Bottom;
		}

		/// <summary>
		///		Desplaza el objeto
		/// </summary>
		public void Translate(Vector2D vctPosition)
		{ Translate(vctPosition.X, vctPosition.Y);
		}

		/// <summary>
		///		Desplaza el objeto
		/// </summary>
		public void Translate(float fltX, float fltY)
		{	Position = new Rectangle(Position.X + fltX, Position.Y + fltY, Position.Width, Position.Height);
		}

		/// <summary>
		///		Mueve el objeto a una posición
		/// </summary>
		public void MoveTo(float fltX, float fltY)
		{ Position = new Rectangle(fltX, fltY, Position.Width, Position.Height);
		}

		/// <summary>
		///		Posición del objeto
		/// </summary>
		public Rectangle Position { get; set; }

		/// <summary>
		///		Dimensiones scaladas del objeto
		/// </summary>
		public Size2D ScaledDimensions
		{ get { return new Size2D(Position.Width * Scale, Position.Height * Scale); }
		}

		/// <summary>
		///		Escala del objeto
		/// </summary>
		public float Scale { get; set; } = 1;

		/// <summary>
		///		Angulo del objeto
		/// </summary>
		public float Angle { get; set; } = 0;

		/// <summary>
		///		Color del objeto
		/// </summary>
		public ColorEngine Color { get; set; } = ColorEngine.White;

		/// <summary>
		///		ZOrder del objeto
		/// </summary>
		public int ZOrder { get; set; } = 0;
	}
}
