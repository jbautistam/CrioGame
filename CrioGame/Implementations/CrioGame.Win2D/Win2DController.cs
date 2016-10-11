using System;
using Windows.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Win2D
{
	/// <summary>
	///		Controlador de la implementación del motor gráfico utilizando Win2D
	/// </summary>
	public class Win2DController : IGraphicsEngineManager
	{
		/// <summary>
		///		Inicializa el controlador
		/// </summary>
		public void Initialize(IGameEngineManager objGameEngine)
		{ Game = new GameInternal.MainGame(this, objGameEngine.GameLoopController);
		}

		/// <summary>
		///		Inicializa el controlador
		/// </summary>
		public void InitializeCanvas(Window wndWindow, ICanvasAnimatedControl cnvCanvas, bool blnDesignMode)
		{ MainWindow = wndWindow;
			Canvas = cnvCanvas;
			IsDesignMode = blnDesignMode;
		}

		/// <summary>
		///		Arranca el juego
		/// </summary>
		public void Start(int intWindowsWidth = 0, int intWindowsHeight = 0)
		{ // Cambia el ancho y alto de la ventana
				if (intWindowsWidth > 0 && intWindowsHeight > 0)
					ScreenController.SetWindowsSize(intWindowsWidth, intWindowsHeight);
			// Ejecuta el juego
				Game.Run();
		}

		/// <summary>
		///		Detiene el juego
		/// </summary>
		public void Stop()
		{ Game.Exit();
		}

		/// <summary>
		///		Motor del juego
		/// </summary>
		internal IGameEngineManager GameEngine { get; private set; }

		/// <summary>
		///		Motor del juego. Implementa el GameLoop
		/// </summary>
		internal GameInternal.MainGame Game { get; private set; }

		/// <summary>
		///		Ventana principal donde se encuentra el Canvas
		/// </summary>
		internal Window MainWindow { get; private set; }

		/// <summary>
		///		Control de dibujo
		/// </summary>
		internal ICanvasAnimatedControl Canvas { get; private set; }

		/// <summary>
		///		Indica si estamos en modo de diseño
		/// </summary>
		internal bool IsDesignMode { get; private set; }

		/// <summary>
		///		Controlador de pantalla
		/// </summary>
		public IScreenController ScreenController 
		{ get { return Game.ScreenController; }
		}

		/// <summary>
		///		Controlador de contenido
		/// </summary>
		public IContentManager ContentController
		{ get { return Game.ContentManager; }
		}

		/// <summary>
		///		Manejador para el dibujo por lotes
		/// </summary>
		public ISpriteBatch SpriteBatch 
		{ get { return Game.SpriteBatch; }
		}

		/// <summary>
		///		Manejador de los dispositivos de entrada
		/// </summary>
		public IInputController InputManager
		{ get { return Game.InputManager; }
		}

		/// <summary>
		///		Controlador de sonido
		/// </summary>
		public ISoundController SoundController
		{ get { return Game.SoundController; }
		}

		/// <summary>
		///		Indica que se ha iniciado el motor gráfico y es seguro realizar llamadas
		/// </summary>
		public bool IsStarted { get; internal set; }
	}
}
