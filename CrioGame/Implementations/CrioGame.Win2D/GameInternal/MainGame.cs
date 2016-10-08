using System;
using Windows.UI.Xaml;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;
using Bau.Libraries.CrioGame.Win2D.Input;

namespace Bau.Libraries.CrioGame.Win2D.GameInternal
{
	/// <summary>
	///		Juego principal
	/// </summary>
	internal class MainGame
	{	// Variables privadas
			private bool blnDesignMode = false, blnWithSpriteBatch = false;

		internal MainGame(Win2DController objController, IGameLoopController objLoopController)
		{ // Inicializa los controladores
				Manager = objController;
				GameLoopManager = objLoopController;
			// Inicializa el tiempo del juego
				StartTime = new TimeSpan(DateTime.Now.Ticks);
		}

		///// <summary>
		/////		Inicializa los datos propios del canvas
		///// </summary>
		//internal void InitCanvas(Window wndWindow, ICanvasAnimatedControl cnvCanvas, bool blnDesignMode = false)
		//{	
		//		Canvas = cnvCanvas;
		//		MainWindow = wndWindow;
		//	// Indica si estamos en modo de diseño
		//		this.blnDesignMode = blnDesignMode;
		//}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		internal void Run()
		{	// Crea el manejador de eventos para la creación de recursos
				Manager.Canvas.CreateResources += (objSender, objEventArgs) => CreateResources(objSender, objEventArgs);
			// Inicializa la pantalla y el controlador de contenido
				ScreenController = new Graphics.ScreenController();
				ScreenController.Initialize(this);
			// Inicializa el manager de contenido
				ContentManager = new Content.ContentManager(this);
				ContentManager.Initialize("Content");
			// Inicializa el manager de los dispositivos de entrada
				InputManager = new InputController(this);
			// Incializa el controlador de sonido
				SoundController = new Sounds.SoundController(this);
			// Indica que ya es seguro llamar a las funciones del motor
				Manager.IsStarted = true;
			// Inicializa el manejador de sprites
				SpriteBatch = new Graphics.SpriteBatchController(this);
			// Carga el contenido
				GameLoopManager.LoadContent();
			// Inicializa el bucle del juego en el motor de animación
				GameLoopManager.Initialize();
			// Crea los manejadores de eventos (cuando ya ha terminado con el resto)
				Manager.Canvas.Draw += (objSender, objEventArgs) => Draw(objSender, objEventArgs);
				Manager.Canvas.Update += (objSender, objEventArgs) => Update(objSender, objEventArgs);
		}

		/// <summary>
		///		Sale del juego
		/// </summary>
		internal void Exit()
		{ // Descarga el contenido
				GameLoopManager.UnloadContent();
		}

		/// <summary>
		///		Crea los recursos
		/// </summary>
		private void CreateResources(ICanvasAnimatedControl cnvCanvas, CanvasCreateResourcesEventArgs objEventArgs)
		{ // Comprueba si se permite utilizar SpriteBatch en el dispositivo
				blnWithSpriteBatch = CanvasSpriteBatch.IsSupported(cnvCanvas.Device);
			//// Carga las imágenes
			//	if (!blnDesignMode && blnWithSpriteBatch)
			//		objEventArgs.TrackAsyncAction(LoadImages(cnvCanvas.Device).AsAsyncAction());
		}

		/// <summary>
		///		Actualiza el contenido del canvas
		/// </summary>
		private void Update(ICanvasAnimatedControl objSender, CanvasAnimatedUpdateEventArgs objEventArgs)
		{	GameLoopManager.Update(TotalGameTime);
			System.Diagnostics.Debug.WriteLine("Update");
		}

		/// <summary>
		///		Dibuja el juego
		/// </summary>
		private void Draw(ICanvasAnimatedControl cnvCanvas, CanvasAnimatedDrawEventArgs objEventArgs)
		{	if (!blnDesignMode)
				{	// Inicializa los datos de dibujo
						SpriteBatch.InitDrawingSession(objEventArgs.DrawingSession);
					//if (bmpTiger != null)
					//	objEventArgs.DrawingSession.DrawImage(bmpTiger, intX, intY);
					System.Diagnostics.Debug.WriteLine("Draw");
					// Llama al manager para dibujar
						GameLoopManager.Draw(TotalGameTime);
				}
		}

		/// <summary>
		///		Motor del juego
		/// </summary>
		internal Win2DController Manager { get; }

		/// <summary>
		///		Manager del bucle del juego
		/// </summary>
		internal IGameLoopController GameLoopManager { get; }

		/// <summary>
		///		Manejador de pantalla
		/// </summary>
		internal Graphics.ScreenController ScreenController { get; private set; }

		/// <summary>
		///		Manager de contenido
		/// </summary>
		internal Content.ContentManager ContentManager { get; private set; }

		/// <summary>
		///		Manejador de dibujo de sprites
		/// </summary>
		internal Graphics.SpriteBatchController SpriteBatch { get; private set; }

		/// <summary>
		///		Manejador de los dispositivos de entrada
		/// </summary>
		internal IInputController InputManager { get; private set; }

		/// <summary>
		///		Controlador de sonido
		/// </summary>
		internal ISoundController SoundController { get; private set; }

		/// <summary>
		///		Momento de inicio del juego
		/// </summary>
		private TimeSpan StartTime { get; }

		/// <summary>
		///		Total del tiempo del juego
		/// </summary>
		private TimeSpan TotalGameTime
		{ get { return TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime.Ticks); }
		}
	}
}
