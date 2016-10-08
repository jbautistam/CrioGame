using System;
using Windows.System;
using Windows.UI.Core;

using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Win2D.Input
{
	/// <summary>
	///		Controlador de teclado, ratón, gamepad ...
	/// </summary>
	public class InputController : IInputController
	{ // Variables privadas
			private System.Collections.Generic.Dictionary<VirtualKey, Common.Enums.Keys> objColDictionary = new System.Collections.Generic.Dictionary<VirtualKey, Common.Enums.Keys>
					{ { VirtualKey.None, Common.Enums.Keys.None },
						{ VirtualKey.Back, Common.Enums.Keys.Back },
						{ VirtualKey.Tab, Common.Enums.Keys.Tab },
						{ VirtualKey.Clear, Common.Enums.Keys.OemClear },
						{ VirtualKey.Enter, Common.Enums.Keys.Enter },
						{ VirtualKey.Shift, Common.Enums.Keys.LeftShift },
						{ VirtualKey.Control, Common.Enums.Keys.LeftControl },
						{ VirtualKey.Pause, Common.Enums.Keys.Pause },
						{ VirtualKey.CapitalLock, Common.Enums.Keys.CapsLock },
						{ VirtualKey.Kana, Common.Enums.Keys.Kana },
						{ VirtualKey.Kanji, Common.Enums.Keys.Kanji },
						{ VirtualKey.Escape, Common.Enums.Keys.Escape },
						{ VirtualKey.Convert, Common.Enums.Keys.ImeConvert },
						{ VirtualKey.NonConvert, Common.Enums.Keys.ImeNoConvert },
						{ VirtualKey.Space, Common.Enums.Keys.Space },
						{ VirtualKey.PageUp, Common.Enums.Keys.PageUp },
						{ VirtualKey.PageDown, Common.Enums.Keys.PageDown },
						{ VirtualKey.End, Common.Enums.Keys.End },
						{ VirtualKey.Home, Common.Enums.Keys.Home },
						{ VirtualKey.Left, Common.Enums.Keys.Left },
						{ VirtualKey.Up, Common.Enums.Keys.Up },
						{ VirtualKey.Right, Common.Enums.Keys.Right },
						{ VirtualKey.Down, Common.Enums.Keys.Down },
						{ VirtualKey.Select, Common.Enums.Keys.Select },
						{ VirtualKey.Print, Common.Enums.Keys.Print },
						{ VirtualKey.Execute, Common.Enums.Keys.Execute },
						{ VirtualKey.Insert, Common.Enums.Keys.Insert },
						{ VirtualKey.Delete, Common.Enums.Keys.Delete },
						{ VirtualKey.Help, Common.Enums.Keys.Help },
						{ VirtualKey.Number0, Common.Enums.Keys.D0 },
						{ VirtualKey.Number1, Common.Enums.Keys.D1 },
						{ VirtualKey.Number2, Common.Enums.Keys.D2 },
						{ VirtualKey.Number3, Common.Enums.Keys.D3 },
						{ VirtualKey.Number4, Common.Enums.Keys.D4 },
						{ VirtualKey.Number5, Common.Enums.Keys.D5 },
						{ VirtualKey.Number6, Common.Enums.Keys.D6 },
						{ VirtualKey.Number7, Common.Enums.Keys.D7 },
						{ VirtualKey.Number8, Common.Enums.Keys.D8 },
						{ VirtualKey.Number9, Common.Enums.Keys.D9 },
						{ VirtualKey.A, Common.Enums.Keys.A },
						{ VirtualKey.B, Common.Enums.Keys.B },
						{ VirtualKey.C, Common.Enums.Keys.C },
						{ VirtualKey.D, Common.Enums.Keys.D },
						{ VirtualKey.E, Common.Enums.Keys.E },
						{ VirtualKey.F, Common.Enums.Keys.F },
						{ VirtualKey.G, Common.Enums.Keys.G },
						{ VirtualKey.H, Common.Enums.Keys.H },
						{ VirtualKey.I, Common.Enums.Keys.I },
						{ VirtualKey.J, Common.Enums.Keys.J },
						{ VirtualKey.K, Common.Enums.Keys.K },
						{ VirtualKey.L, Common.Enums.Keys.L },
						{ VirtualKey.M, Common.Enums.Keys.M },
						{ VirtualKey.N, Common.Enums.Keys.N },
						{ VirtualKey.O, Common.Enums.Keys.O },
						{ VirtualKey.P, Common.Enums.Keys.P },
						{ VirtualKey.Q, Common.Enums.Keys.Q },
						{ VirtualKey.R, Common.Enums.Keys.R },
						{ VirtualKey.S, Common.Enums.Keys.S },
						{ VirtualKey.T, Common.Enums.Keys.T },
						{ VirtualKey.U, Common.Enums.Keys.U },
						{ VirtualKey.V, Common.Enums.Keys.V },
						{ VirtualKey.W, Common.Enums.Keys.W },
						{ VirtualKey.X, Common.Enums.Keys.X },
						{ VirtualKey.Y, Common.Enums.Keys.Y },
						{ VirtualKey.Z, Common.Enums.Keys.Z },
						{ VirtualKey.LeftWindows, Common.Enums.Keys.LeftWindows },
						{ VirtualKey.RightWindows, Common.Enums.Keys.RightWindows },
						{ VirtualKey.Application, Common.Enums.Keys.Apps },
						{ VirtualKey.Sleep, Common.Enums.Keys.Sleep },
						{ VirtualKey.NumberPad0, Common.Enums.Keys.NumPad0 },
						{ VirtualKey.NumberPad1, Common.Enums.Keys.NumPad1 },
						{ VirtualKey.NumberPad2, Common.Enums.Keys.NumPad2 },
						{ VirtualKey.NumberPad3, Common.Enums.Keys.NumPad3 },
						{ VirtualKey.NumberPad4, Common.Enums.Keys.NumPad4 },
						{ VirtualKey.NumberPad5, Common.Enums.Keys.NumPad5 },
						{ VirtualKey.NumberPad6, Common.Enums.Keys.NumPad6 },
						{ VirtualKey.NumberPad7, Common.Enums.Keys.NumPad7 },
						{ VirtualKey.NumberPad8, Common.Enums.Keys.NumPad8 },
						{ VirtualKey.NumberPad9, Common.Enums.Keys.NumPad9 },
						{ VirtualKey.Multiply, Common.Enums.Keys.Multiply },
						{ VirtualKey.Add, Common.Enums.Keys.Add },
						{ VirtualKey.Separator, Common.Enums.Keys.Separator },
						{ VirtualKey.Subtract, Common.Enums.Keys.Subtract },
						{ VirtualKey.Decimal, Common.Enums.Keys.Decimal },
						{ VirtualKey.Divide, Common.Enums.Keys.Divide },
						{ VirtualKey.F1, Common.Enums.Keys.F1 },
						{ VirtualKey.F2, Common.Enums.Keys.F2 },
						{ VirtualKey.F3, Common.Enums.Keys.F3 },
						{ VirtualKey.F4, Common.Enums.Keys.F4 },
						{ VirtualKey.F5, Common.Enums.Keys.F5 },
						{ VirtualKey.F6, Common.Enums.Keys.F6 },
						{ VirtualKey.F7, Common.Enums.Keys.F7 },
						{ VirtualKey.F8, Common.Enums.Keys.F8 },
						{ VirtualKey.F9, Common.Enums.Keys.F9 },
						{ VirtualKey.F10, Common.Enums.Keys.F10 },
						{ VirtualKey.F11, Common.Enums.Keys.F11 },
						{ VirtualKey.F12, Common.Enums.Keys.F12 },
						{ VirtualKey.F13, Common.Enums.Keys.F13 },
						{ VirtualKey.F14, Common.Enums.Keys.F14 },
						{ VirtualKey.F15, Common.Enums.Keys.F15 },
						{ VirtualKey.F16, Common.Enums.Keys.F16 },
						{ VirtualKey.F17, Common.Enums.Keys.F17 },
						{ VirtualKey.F18, Common.Enums.Keys.F18 },
						{ VirtualKey.F19, Common.Enums.Keys.F19 },
						{ VirtualKey.F20, Common.Enums.Keys.F20 },
						{ VirtualKey.F21, Common.Enums.Keys.F21 },
						{ VirtualKey.F22, Common.Enums.Keys.F22 },
						{ VirtualKey.F23, Common.Enums.Keys.F23 },
						{ VirtualKey.F24, Common.Enums.Keys.F24 },
						{ VirtualKey.NumberKeyLock, Common.Enums.Keys.NumLock },
						{ VirtualKey.Scroll, Common.Enums.Keys.Scroll },
						{ VirtualKey.LeftShift, Common.Enums.Keys.LeftShift },
						{ VirtualKey.RightShift, Common.Enums.Keys.RightShift },
						{ VirtualKey.LeftControl, Common.Enums.Keys.LeftControl },
						{ VirtualKey.RightControl, Common.Enums.Keys.RightControl },
						{ VirtualKey.LeftMenu, Common.Enums.Keys.LeftWindows },
						{ VirtualKey.RightMenu, Common.Enums.Keys.RightWindows },
						{ VirtualKey.GoBack, Common.Enums.Keys.BrowserBack },
						{ VirtualKey.GoForward, Common.Enums.Keys.BrowserForward },
						{ VirtualKey.Refresh, Common.Enums.Keys.BrowserRefresh },
						{ VirtualKey.Stop, Common.Enums.Keys.BrowserStop },
						{ VirtualKey.Search, Common.Enums.Keys.BrowserSearch },
						{ VirtualKey.Favorites, Common.Enums.Keys.BrowserFavorites },
						{ VirtualKey.GoHome, Common.Enums.Keys.BrowserHome }
					};

		internal InputController(GameInternal.MainGame objMainGame)
		{ MainGame = objMainGame;
			MainGame.Manager.MainWindow.CoreWindow.KeyDown += (objSender, objEvntArgs) => TreatKey(objEvntArgs, true);
			MainGame.Manager.MainWindow.CoreWindow.KeyUp += (objSender, objEvntArgs) => TreatKey(objEvntArgs, false);
			MainGame.Manager.MainWindow.CoreWindow.PointerMoved += (objSender, objEvntArgs)=> TreatPointer(objEvntArgs);
			MainGame.Manager.MainWindow.CoreWindow.PointerPressed += (objSender, objEvntArgs)=> TreatPointer(objEvntArgs);
		}

		/// <summary>
		///		Trata un evento de teclado
		/// </summary>
		private void TreatKey(KeyEventArgs objEvntArgs, bool blnKeyDown)
		{ if (blnKeyDown)
				CurrentKeyboardState.Add(Convert(objEvntArgs.VirtualKey));
			else
				CurrentKeyboardState.Remove(Convert(objEvntArgs.VirtualKey));
		}

		/// <summary>
		///		Trata un evento de ratón / puntero
		/// </summary>
		private void TreatPointer(PointerEventArgs objEvntArgs)
		{ Windows.UI.Input.PointerPointProperties objProperties = objEvntArgs.CurrentPoint.Properties;

				// Obtiene la posición
					CurrentMouseState.Position = new Vector2D((float) objEvntArgs.CurrentPoint.Position.X, (float) objEvntArgs.CurrentPoint.Position.Y);
				// Comprueba los botones del ratón
					CurrentMouseState.IsLeftButtonPressed = objProperties.IsLeftButtonPressed;
					CurrentMouseState.IsRightButtonPressed = objProperties.IsRightButtonPressed;
					CurrentMouseState.IsMiddleButtonPressed = objProperties.IsMiddleButtonPressed;
					CurrentMouseState.IsX1ButtonPressed = objProperties.IsXButton1Pressed;
					CurrentMouseState.IsX2ButtonPressed = objProperties.IsXButton2Pressed;
		}

		/// <summary>
		///		Actualiza los datos de entrada
		/// </summary>
		public void Update()
		{	// Cambia las posiciones del ratón
				PreviousMousePosition = CurrentMousePosition;
				CurrentMousePosition = new Vector2D(CurrentMouseState.Position.X, CurrentMouseState.Position.Y);
			// Obtiene el delta del ratón
				DeltaMouse = PreviousMousePosition - CurrentMousePosition;
			// Guarda el estado anterior del teclado y el ratón
				PreviousKeyboardState = CurrentKeyboardState;
				PreviousMouseState = CurrentMouseState;
			//// Limpia los estados actuales para poder empezar de nuevo
			//	CurrentKeyboardState = new KeyBoardState();
			//	CurrentMouseState = new MouseState();
		}

		/// <summary>
		///		Comprueba si se ha presionado una tecla
		/// </summary>
		public bool IsPressedKey(Common.Enums.Keys intKey)
		{ return CurrentKeyboardState.IsPressed(intKey);
		}

		/// <summary>
		///		Comprueba si se ha presionado una tecla (y antes no lo estaba)
		/// </summary>
		public bool ChangedPressedKey(Common.Enums.Keys intKey)
		{ return CurrentKeyboardState.IsPressed(intKey) && !PreviousKeyboardState.IsPressed(intKey);
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
		{ return CheckMouseButton(CurrentMouseState, intButton, true);
		}

		/// <summary>
		///		Comprueba si se ha liberado un botón del ratón
		/// </summary>
		public bool IsReleasedMouseButton(Common.Enums.MouseButtons intButton)
		{ return CheckMouseButton(CurrentMouseState, intButton, false);
		}

		/// <summary>
		///		Comprueba si un botón está pulsado o liberado
		/// </summary>
		private bool CheckMouseButton(MouseState objMouse, Common.Enums.MouseButtons intButton, bool blnIsPressed)
		{ switch (intButton)
				{ case Common.Enums.MouseButtons.Left:
						return CurrentMouseState.IsLeftButtonPressed;
					case Common.Enums.MouseButtons.Middle:
						return CurrentMouseState.IsMiddleButtonPressed;
					case Common.Enums.MouseButtons.Right:
						return CurrentMouseState.IsRightButtonPressed;
					case Common.Enums.MouseButtons.X1:
						return CurrentMouseState.IsX1ButtonPressed;
					case Common.Enums.MouseButtons.X2:
						return CurrentMouseState.IsX2ButtonPressed;
					default:
						return false;
				}			
		}

		/// <summary>
		///		Convierte una tecla virtual en el enumerado
		/// </summary>
		private Common.Enums.Keys Convert(VirtualKey intKey)
		{	Common.Enums.Keys intConverted = Common.Enums.Keys.None;

				if (objColDictionary.TryGetValue(intKey, out intConverted))
					return intConverted;
				else
					return Common.Enums.Keys.None;
		}

		/// <summary>
		///		Juego principal
		/// </summary>
		private GameInternal.MainGame MainGame { get; }

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
		private KeyBoardState CurrentKeyboardState { get; set; } = new KeyBoardState();
		
		/// <summary>
		///		Estado anterior del teclado
		/// </summary>
		private KeyBoardState PreviousKeyboardState { get; set; } = new KeyBoardState();
		
		/// <summary>
		///		Estado actual del ratón
		/// </summary>
		private MouseState CurrentMouseState { get; set; } = new MouseState();
		
		/// <summary>
		///		Estado anterior del ratón
		/// </summary>
		private MouseState PreviousMouseState { get; set; } = new MouseState();
	}
}
