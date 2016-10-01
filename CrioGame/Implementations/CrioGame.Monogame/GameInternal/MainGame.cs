using System;

using Microsoft.Xna.Framework;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;
using Bau.Libraries.CrioGame.MonogameImpl.Input;

namespace Bau.Libraries.CrioGame.MonogameImpl.GameInternal
{
	/// <summary>
	///		Juego principal
	/// </summary>
	internal class MainGame : Game
	{	
		internal MainGame(MonogameController objController, IGameLoopController objLoopController)
		{ Manager = objController;
			GameLoopManager = objLoopController;
		}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		internal void InitializeModel()
		{	// Inicializa la pantalla y el controlador de contenido
				ScreenController = new Graphics.ScreenController();
				ScreenController.Initialize(this);
			// Inicializa el manager de contenido
				ContentManager = new Content.ContentManager(this);
				ContentManager.Initialize("Content");
			// Inicializa el manager de los dispositivos de entrada
				InputManager = new InputController();
			// Incializa el controlador de sonido
				SoundController = new Sounds.SoundController(this);
			// Indica que ya es seguro llamar a las funciones del motor
				Manager.IsStarted = true;
		}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		protected override void Initialize()
		{	// Inicializa el manejador de sprites
				SpriteBatch = new Graphics.SpriteBatchController(this, ScreenController.Graphics.GraphicsDevice);
			// Inicializa el bucle del juego en el motor de animación
				GameLoopManager.Initialize();
			// Llama al método base
				base.Initialize();
		}

		/// <summary>
		///		Carga el contenido
		/// </summary>
		protected override void LoadContent()
		{ // Carga el contenido en el motor de animación
				GameLoopManager.LoadContent();
			// Llama al método base
				base.LoadContent();
		}

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		protected override void UnloadContent()
		{ // Llama al manager para descargar el contenido
				GameLoopManager.UnloadContent();
			// Llama al método base
				base.UnloadContent();
		}

		/// <summary>
		///		Modifica el juego
		/// </summary>
		protected override void Update(GameTime objGameTime)
		{	// Llama al manager para modificar los datos
				GameLoopManager.Update(objGameTime.TotalGameTime);
			// Llama al método base
				base.Update(objGameTime);
		}

		/// <summary>
		///		Dibuja el juego
		/// </summary>
		protected override void Draw(GameTime objGameTime)
		{	// Limpia la pantalla
				GraphicsDevice.Clear(Color.CornflowerBlue);
			// Llama al manager para dibujar
				GameLoopManager.Draw(objGameTime.TotalGameTime);
			// Llama al método base
				base.Draw(objGameTime);
		}

		/// <summary>
		///		Motor del juego
		/// </summary>
		internal MonogameController Manager { get; }

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
	}
}
