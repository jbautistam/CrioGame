using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine
{
	/// <summary>
	///		Controlador de los dispositivos de entrada: teclado, gamepad, ratón...
	/// </summary>
	public interface IInputController
	{
		/// <summary>
		///		Actualiza los datos de entrada
		/// </summary>
		void Update();

		/// <summary>
		///		Comprueba si se ha presionado una tecla
		/// </summary>
		bool IsPressedKey(Enums.Keys intKey);

		/// <summary>
		///		Comprueba si se ha presionado una tecla (y antes no lo estaba)
		/// </summary>
		bool ChangedPressedKey(Enums.Keys intKey);

		/// <summary>
		///		Comprueba si se ha presionado cualquier tecla
		/// </summary>
		bool IsPressedAnyKey();

		/// <summary>
		///		Comprueba si se ha pulsado algún botón del ratón
		/// </summary>
		bool IsPressedAnyMouseButton();

		/// <summary>
		///		Comprueba si se ha pulsado un botón del ratón
		/// </summary>
		bool IsPressedMouseButton(Enums.MouseButtons intButton);

		/// <summary>
		///		Comprueba si se ha liberado un botón del ratón
		/// </summary>
		bool IsReleasedMouseButton(Enums.MouseButtons intButton);

		///// <summary>
		/////		Estado actual del teclado
		///// </summary>
		//public KeyboardState CurrentKeyboardState { get; private set; }
		
		///// <summary>
		/////		Estado anterior del teclado
		///// </summary>
		//public KeyboardState PreviousKeyboardState { get; private set; }

		///// <summary>
		/////		Estado actual del gamePad
		///// </summary>
		//public GamePadState CurrentGamePadState { get; private set; }

		///// <summary>
		/////		Estado anterior del gamePad
		///// </summary>
		//public GamePadState PreviousGamePadState { get; private set; }

		/// <summary>
		///		Movimiento del ratón
		/// </summary>
		Models.Structs.Vector2D DeltaMouse { get; }

		/// <summary>
		///		Posición actual del ratón
		/// </summary>
		Models.Structs.Vector2D CurrentMousePosition { get; }
	}
}
