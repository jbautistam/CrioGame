using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Contents;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Controlador del bucle del juego
	/// </summary>
	internal class GameLoopController : IGameLoopController
	{
		internal GameLoopController(Common.Interfaces.IGameEngineManager objGameController)
		{ GameController = objGameController;
		}

		/// <summary>
		///		Arranca la ejecución del juego
		/// </summary>
		public void Start(int intWindowsWidth = 0, int intWindowsHeight = 0)
		{	// Inicializa el objeto de juego principal
				GameController.MainManager.GraphicsEngine.Initialize(GameController);
			// Arranca el juego
				GameController.MainManager.GraphicsEngine.Start(intWindowsWidth, intWindowsHeight);
		}

		/// <summary>
		///		Detiene el juego
		/// </summary>
		public void Stop()
		{ GameController.MainManager.GraphicsEngine.Stop();
		}

		/// <summary>
		///		Inicializa el bucle de juego
		/// </summary>
		public void Initialize()
		{ // ... en este caso no hace nada
		}

		/// <summary>
		///		Carga el contenido
		/// </summary>
		public void LoadContent()
		{ List<KeyValuePair<string, AbstractContentBase>> objColContents = (GameController.ContentController as GameRepository).ToList();

				// Inicializa el manager
					GameController.MainManager.GraphicsEngine.ContentController.Initialize(GameController.ContentController.ContentRoot);
				// Añae el contenido
					foreach (KeyValuePair<string, AbstractContentBase> objContent in objColContents)
						GameController.MainManager.GraphicsEngine.ContentController.Load(objContent.Value);
				// Inicializa las entidades
					GameController.SceneController.ActualScene.Initialize(GameController.GameContext);
				// Toca la música
					GameController.MainManager.GraphicsEngine.SoundController.Play(GameController.MainManager.GameParameters.Configuration.ActualSong);
		}

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		public void UnloadContent()
		{ GameController.MainManager.GraphicsEngine.ContentController.Unload();
		}

		/// <summary>
		///		Modifica el juego
		/// </summary>
		public void Update(TimeSpan tsActual)
		{	// Recoge los datos de entrada (teclado, ratón...)
				GameController.MainManager.GraphicsEngine.InputManager.Update();
			// Modifica las entidades
				GameController.SceneController.ActualScene.Update(GameController.GameContext);
			// Guarda el momento de la última modificación
				GameController.GameContext.EndUpdate(tsActual);
		}

		/// <summary>
		///		Dibuja el juego
		/// </summary>
		public void Draw(TimeSpan tsActual)
		{	// Comienza el dibujo de los sprites
				GameController.MainManager.GraphicsEngine.SpriteBatch.Begin();
			// Dibuja los gráficos
				GameController.SceneController.ActualScene.Draw(GameController.GameContext);
			// Finaliza el dibujo de los sprites
				GameController.MainManager.GraphicsEngine.SpriteBatch.End();
			// Guarda el momento de último dibujo
				GameController.GameContext.EndDraw(tsActual);
		}

		/// <summary>
		///		Motor de juegos
		/// </summary>
		internal Common.Interfaces.IGameEngineManager GameController { get; }
	}
}
