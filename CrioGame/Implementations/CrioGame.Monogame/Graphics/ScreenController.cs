using System;

using Microsoft.Xna.Framework;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl.Graphics
{
	/// <summary>
	///		Controlador de pantalla
	/// </summary>
	public class ScreenController : IScreenController
	{
		/// <summary>
		///		Inicliaza la pantalla
		/// </summary>
		internal void Initialize(Game objGame)
		{ Graphics = new GraphicsDeviceManager(objGame);
		}
		
		/// <summary>
		///		Cambia el tamaño de la ventana
		/// </summary>
		public void SetWindowsSize(int intWidth, int intHeight)
		{ // Cambia los datos del buffer
				Graphics.PreferredBackBufferWidth = intWidth;
				Graphics.PreferredBackBufferHeight = intHeight;
				Graphics.GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, intWidth, intHeight);
			// Aplica los cambios
				Graphics.ApplyChanges();
		}

		/// <summary>
		///		Cambia la ventana a pantalla completa
		/// </summary>
		public void SetWindowsFullScreen()
		{	// Cambia los datos del buffer
				Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.DisplayMode.Width;
				Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.DisplayMode.Height;
			// Indica que está a pantalla completa
				Graphics.IsFullScreen = true;
			// Aplica los cambios
				Graphics.ApplyChanges();
		}

		/// <summary>
		///		Manager de gráficos del dispositivo
		/// </summary>
		internal GraphicsDeviceManager Graphics { get; private set; }

		/// <summary>
		///		Ventana de visualización
		/// </summary>
		public Common.Models.Structs.Rectangle ViewPort
		{ get 
				{ return new Common.Models.Structs.Rectangle(0, 0, 
																										 Graphics.GraphicsDevice.Viewport.Width, 
																										 Graphics.GraphicsDevice.Viewport.Height); 
				}
		}
	}
}
