using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl.Input
{
	/// <summary>
	///		Controlador de teclado, ratón, gamepad ...
	/// </summary>
	public class InputController : IInputController
	{ 
		/// <summary>
		///		Actualiza los datos de entrada
		/// </summary>
		public void Update()
		{	// Guarda el estado anterior del teclado y el game pad
				PreviousGamePadState = CurrentGamePadState;
				PreviousKeyboardState = CurrentKeyboardState;
				PreviousMouseState = CurrentMouseState;
			// Lee el estado actual del teclado y el gamepad y lo guarda
				CurrentKeyboardState = Keyboard.GetState();
				CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
				CurrentMouseState = Mouse.GetState();
			// Cambia las posiciones del ratón
				PreviousMousePosition = CurrentMousePosition;
				CurrentMousePosition = new Vector2D(CurrentMouseState.X, CurrentMouseState.Y);
			// Recoge los toques sobre la pantalla táctil
				while (TouchPanel.IsGestureAvailable)
					{	GestureSample objGesture = TouchPanel.ReadGesture();

							if (objGesture.GestureType == GestureType.FreeDrag)
								{ // player.Position += gesture.Delta;
								}
					}
			// Obtiene el delta del ratón
				if (PreviousMouseState != null)
					DeltaMouse = new Vector2D(PreviousMouseState.X - CurrentMouseState.X, PreviousMouseState.Y - CurrentMouseState.Y);
				else
					DeltaMouse = new Vector2D(0, 0);
		}

		/// <summary>
		///		Comprueba si se ha presionado una tecla
		/// </summary>
		public bool IsPressedKey(Common.Enums.Keys intKey)
		{ return CurrentKeyboardState.IsKeyDown(Convert(intKey));
		}

		/// <summary>
		///		Comprueba si se ha presionado una tecla (y antes no lo estaba)
		/// </summary>
		public bool ChangedPressedKey(Common.Enums.Keys intKey)
		{ Keys intConverted = Convert(intKey);

				return CurrentKeyboardState.IsKeyDown(intConverted) && PreviousKeyboardState.IsKeyUp(intConverted);
		}

		/// <summary>
		///		Convierte los datos del teclado
		/// </summary>
		/// <returns>
		///		No hace falta convertirlo en un switch porque los datos del enumerado son iguales a los del framework de MonoGame.
		///	En otras implementaciones, esto habrá que modificarlo
		///	</returns>
		private Keys Convert(Common.Enums.Keys intKey)
		{ return (Keys) ((int) intKey);
		}

		/// <summary>
		///		Comprueba si se ha presionado cualquier tecla
		/// </summary>
		public bool IsPressedAnyKey()
		{ // Comprueba si se a pulsado alguna tecla
				foreach (int intValue in Enum.GetValues(typeof(Common.Enums.Keys)))
					if (ChangedPressedKey((Common.Enums.Keys) intValue))
						return true;
			// Si ha llegado hasta aquí es porque no se ha presionado ninguna
				return false;
		}

		/// <summary>
		///		Comprueba si se ha pulsado algún botón del ratón
		/// </summary>
		public bool IsPressedAnyMouseButton()
		{ return IsPressedMouseButton(Common.Enums.MouseButtons.Left) || IsPressedMouseButton(Common.Enums.MouseButtons.Right) ||
						 IsPressedMouseButton(Common.Enums.MouseButtons.Middle) || IsPressedMouseButton(Common.Enums.MouseButtons.X1) ||
						 IsPressedMouseButton(Common.Enums.MouseButtons.X2);
		}

		/// <summary>
		///		Comprueba si se ha pulsado un botón del ratón
		/// </summary>
		public bool IsPressedMouseButton(Common.Enums.MouseButtons intButton)
		{ return CheckMouseButton(CurrentMouseState, intButton, ButtonState.Pressed);
		}

		/// <summary>
		///		Comprueba si se ha liberado un botón del ratón
		/// </summary>
		public bool IsReleasedMouseButton(Common.Enums.MouseButtons intButton)
		{ return CheckMouseButton(CurrentMouseState, intButton, ButtonState.Released);
		}

		/// <summary>
		///		Comprueba si un botón está pulsado o liberado
		/// </summary>
		private bool CheckMouseButton(MouseState objMouse, Common.Enums.MouseButtons intButton, ButtonState intState)
		{ switch (intButton)
				{ case Common.Enums.MouseButtons.Left:
						return objMouse.LeftButton == ButtonState.Pressed;
					case Common.Enums.MouseButtons.Middle:
						return objMouse.MiddleButton == ButtonState.Pressed;
					case Common.Enums.MouseButtons.Right:
						return objMouse.RightButton == ButtonState.Pressed;
					case Common.Enums.MouseButtons.X1:
						return objMouse.XButton1 == ButtonState.Pressed;
					case Common.Enums.MouseButtons.X2:
						return objMouse.XButton2 == ButtonState.Pressed;
					default:
						return false;
				}			
		}

		/// <summary>
		///		Movimiento del ratón
		/// </summary>
		public Vector2D DeltaMouse { get; private set; }

		/// <summary>
		///		Posición actual del ratón
		/// </summary>
		public Vector2D CurrentMousePosition { get; private set; } = new Vector2D();

		/// <summary>
		///		Posición anterior del ratón
		/// </summary>
		public Vector2D PreviousMousePosition { get; private set; } = new Vector2D();

		/// <summary>
		///		Estado actual del teclado
		/// </summary>
		private KeyboardState CurrentKeyboardState { get; set; }
		
		/// <summary>
		///		Estado anterior del teclado
		/// </summary>
		private KeyboardState PreviousKeyboardState { get; set; }

		/// <summary>
		///		Estado actual del gamePad
		/// </summary>
		private GamePadState CurrentGamePadState { get; set; }

		/// <summary>
		///		Estado anterior del gamePad
		/// </summary>
		private GamePadState PreviousGamePadState { get; set; }
		
		/// <summary>
		///		Estado actual del ratón
		/// </summary>
		private MouseState CurrentMouseState { get; set; }
		
		/// <summary>
		///		Estado anterior del ratón
		/// </summary>
		private MouseState PreviousMouseState { get; set; }
	}
}
