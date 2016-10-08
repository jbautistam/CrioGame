using System;

namespace Bau.Libraries.CrioGame.Win2D.Input
{
	/// <summary>
	///		Clase con el estado del ratón
	/// </summary>
	internal class MouseState
	{
		/// <summary>
		///		Posición actual del ratón
		/// </summary>
		internal Common.Models.Structs.Vector2D Position { get; set; } = new Common.Models.Structs.Vector2D();

		/// <summary>
		///		Indica si está pulsado el botón derecho
		/// </summary>
		internal bool IsRightButtonPressed { get; set; }

		/// <summary>
		///		Indica si está pulsado el botón izquierdo
		/// </summary>
		internal bool IsLeftButtonPressed { get; set; }

		/// <summary>
		///		Indica si está pulsado el botón central
		/// </summary>
		internal bool IsMiddleButtonPressed { get; set; }

		/// <summary>
		///		Indica si está pulsado el botón X1
		/// </summary>
		internal bool IsX1ButtonPressed { get; set; }

		/// <summary>
		///		Indica si está pulsado el botón X2
		/// </summary>
		internal bool IsX2ButtonPressed { get; set; }
	}
}
