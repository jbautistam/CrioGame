using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Win2D.Graphics
{
	/// <summary>
	///		Controlador de pantalla
	/// </summary>
	public class ScreenController : IScreenController
	{
		/// <summary>
		///		Inicializa la pantalla
		/// </summary>
		internal void Initialize(GameInternal.MainGame objGame)
		{ MainGame = objGame;
		}
		
		/// <summary>
		///		Cambia el tamaño de la ventana
		/// </summary>
		public void SetWindowsSize(int intWidth, int intHeight)
		{ 
			//// Cambia los datos del buffer
			//	Graphics.PreferredBackBufferWidth = intWidth;
			//	Graphics.PreferredBackBufferHeight = intHeight;
			//	Graphics.GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, intWidth, intHeight);
			//// Aplica los cambios
			//	Graphics.ApplyChanges();
		}

		/// <summary>
		///		Cambia la ventana a pantalla completa
		/// </summary>
		public void SetWindowsFullScreen()
		{	
			//// Cambia los datos del buffer
			//	Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.DisplayMode.Width;
			//	Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.DisplayMode.Height;
			//// Indica que está a pantalla completa
			//	Graphics.IsFullScreen = true;
			//// Aplica los cambios
			//	Graphics.ApplyChanges();
		}

		/// <summary>
		///		Controlador del juego
		/// </summary>
		internal GameInternal.MainGame MainGame { get; private set; }

		/// <summary>
		///		Ventana de visualización
		/// </summary>
		public Common.Models.Structs.Rectangle ViewPort
		{ get 
				{ return new Common.Models.Structs.Rectangle(0, 0, 
																										 (float) MainGame.Manager.Canvas.Size.Width,
																										 (float) MainGame.Manager.Canvas.Size.Height); 
				}
		}
	}
}
