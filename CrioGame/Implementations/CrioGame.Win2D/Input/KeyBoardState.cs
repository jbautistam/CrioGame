using System;

namespace Bau.Libraries.CrioGame.Win2D.Input
{
	/// <summary>
	///		Estado del teclado
	/// </summary>
	internal class KeyBoardState
	{	
		/// <summary>
		///		Añade una tecla
		/// </summary>
		internal void Add(Common.Enums.Keys intKey)
		{ if (intKey != Common.Enums.Keys.None)	
				KeysPressed.Add(intKey);
		}

		/// <summary>
		///		Elimina una tecla
		/// </summary>
		internal void Remove(Common.Enums.Keys intKey)
		{ if (intKey != Common.Enums.Keys.None) // ... esta no se puede insertar
				for (int intIndex = KeysPressed.Count - 1; intIndex >= 0; intIndex--)
					if (KeysPressed[intIndex] == intKey)
						KeysPressed.RemoveAt(intIndex);
		}

		/// <summary>
		///		Comprueba si una tecla está presionada
		/// </summary>
		internal bool IsPressed(Common.Enums.Keys intKey)
		{ // Comprueba las teclas
				foreach (Common.Enums.Keys intKeyPressed in KeysPressed)
					if (intKey == intKeyPressed)
						return true;
			// Si ha llegado hasta aquí es que no se ha pulsado
				return false;
		}

		/// <summary>
		///		Teclas presionadas
		/// </summary>
		private System.Collections.Generic.List<Common.Enums.Keys> KeysPressed { get; } = new System.Collections.Generic.List<Common.Enums.Keys>();
	}
}
