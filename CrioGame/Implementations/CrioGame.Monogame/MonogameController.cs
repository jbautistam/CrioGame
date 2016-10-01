using System;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl
{
	/// <summary>
	///		Controlador de la implementación del motor gráfico utilizando MonoGame
	/// </summary>
	public class MonogameController : IGraphicsEngineManager
	{
		/// <summary>
		///		Inicializa el controlador
		/// </summary>
		public void Initialize(IGameEngineManager objGameEngine)
		{ GameEngine = objGameEngine;
			Game = new GameInternal.MainGame(this, GameEngine.GameLoopController);
			Game.InitializeModel();
		}

		/// <summary>
		///		Arranca el juego
		/// </summary>
		public void Start(int intWindowsWidth = 0, int intWindowsHeight = 0)
		{ // Inicializa los datos para Windows
				Game.Window.AllowAltF4 = true;
				Game.Window.AllowUserResizing = true;
				Game.IsMouseVisible = true;
			// Cambia el ancho y alto de la ventana
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
		///		Motor del juego (necesario para la implementación de MonoGame)
		/// </summary>
		internal GameInternal.MainGame Game { get; private set; }

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
		public bool IsStarted { get; set; }
	}
}
