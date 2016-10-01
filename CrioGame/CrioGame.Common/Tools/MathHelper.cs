using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Tools
{
	/// <summary>
	///		Rutinas de ayuda
	/// </summary>
	public class MathHelper
	{ // Variables privadas
			private Random objRandom = new Random();

		/// <summary>
		///		Obtiene un valor aleatorio
		/// </summary>
		public int Random(int intMaxValue)
		{ return objRandom.Next(intMaxValue);
		}

		/// <summary>
		///		Obtiene un valor aleatorio entre dos números
		/// </summary>
		public int Random(int intMinimum, int intMaximum)
		{ return objRandom.Next(intMinimum, intMaximum);
		}

		/// <summary>
		///		Obtiene un valor en un rango
		/// </summary>
		public float Clamp(float fltValue, float fltMinimum, float fltMaximum)
		{ if (fltValue < fltMinimum)
				return fltMinimum;
			else if (fltValue > fltMaximum)
				return fltMaximum;
			else
				return fltValue;
		}

		/// <summary>
		///		Obtiene un vector normalizado dentro de la pantalla
		/// </summary>
		public Vector2D ClampScreen(Vector2D objPosition, IView objView)
		{ return new Vector2D(Clamp(objPosition.X, 0, objView.ViewPortScreen.Width),
													Clamp(objPosition.Y, 0, objView.ViewPortScreen.Height));
		}

		/// <summary>
		///		Normaliza la coordenada X en el ancho de la pantalla
		/// </summary>
		public float ClampScreenWidth(float fltX, float fltWidth, IView objView)
		{ return Clamp(fltX, 0, objView.ViewPortScreen.Width - fltWidth);
		}

		/// <summary>
		///		Normaliza la coordenada Y en el ancho de la pantalla
		/// </summary>
		public float ClampScreenHeight(float fltY, float fltHeight, IView objView)
		{ return Clamp(fltY, 0, objView.ViewPortScreen.Height - fltHeight);
		}

		/// <summary>
		///		Indica si una posición está en pantalla
		/// </summary>
		public bool IsAtScreen(Vector2D objPosition, IView objView)
		{ return IsAtScreen(objPosition.X, objPosition.Y, objView);
		}

		/// <summary>
		///		Indica si un punto está en pantalla
		/// </summary>
		public bool IsAtScreen(float fltX, float fltY, IView objView)
		{ return fltX >= 0 && fltX <= objView.ViewPortScreen.Width &&
						 fltY >= 0 && fltY <= objView.ViewPortScreen.Height;
		}

		/// <summary>
		///		Normaliza un rectángulo en la pantalla
		/// </summary>
		public Rectangle ClampScreen(Rectangle rctSource, IView objView)
		{	return new Rectangle(Clamp(rctSource.X, 0, objView.ViewPortScreen.Width),
													 Clamp(rctSource.Y, 0, objView.ViewPortScreen.Height),
													 rctSource.Width, rctSource.Height);
		}

		/// <summary>
		///		Comprueba si ha pasado cierto tiempo (y si es así, guarda el momento actual)
		/// </summary>
		public bool IsElapsed(TimeSpan tsActualTime, TimeSpan tsSpawnTime, ref TimeSpan tsPreviousTime)
		{	if (tsActualTime - tsPreviousTime > tsSpawnTime)
				{ // Guarda el momento actual
						tsPreviousTime = tsActualTime;
					// Indica que se ha pasado el tiempo
						return true;
				}
			else
				return false;
		}
	}
}
