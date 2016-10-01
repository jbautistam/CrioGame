using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface;

namespace SpaceWarGame.Model.Scenes
{
	/// <summary>
	///		Escena con el menú principal del juego
	/// </summary>
	internal class MainMenuScene: Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
	{
		/// <summary>
		///		Añade las entidades a la escena
		/// </summary>
		public override void LoadContentScene(IGameContext objContext)
		{	// ... simplemente implementa la interface
		}

		/// <summary>
		///		Inicializa la escena
		/// </summary>
		public override void InitializeScene(IGameContext objContext)
		{ // Crea los botones
				ConfigurationButton = ButtonControl.Create(objContext, 20, 20,
																									 "UIImage", "Controls", 
																									 "ButtonTools", "ButtonTools", 
																									 "Font", "", ColorEngine.Black, ColorEngine.Red);
				PlayButton = ButtonControl.Create(objContext, ViewDefault.ViewPortScreen.Width / 3, 2 * ViewDefault.ViewPortScreen.Height / 3,
																					"UIImage", "Controls",
																					"ButtonBlue1", "ButtonYellow1", 
																					"Font", "Jugar", ColorEngine.Black, ColorEngine.Red);
				CloseButton = ButtonControl.Create(objContext, 2 * ViewDefault.ViewPortScreen.Width / 3, 2 * ViewDefault.ViewPortScreen.Height / 3,
																					 "UIImage", "Controls",
																					 "ButtonRed1", "ButtonRed2", 
																					 "Font", "Salir", ColorEngine.Black, ColorEngine.White);
			// Añade el fondo
				ViewDefault.AddEntity(Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "MenuBackground", 0));
			// Añade los botones
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), ConfigurationButton);
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), PlayButton);
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), CloseButton);
		}

		/// <summary>
		///		Modifica la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Escape) ||
					CloseButton.Clicked)
				objContext.GameController.MainManager.Stop();
			else if (ConfigurationButton.Clicked)
				objContext.GameController.SceneController.SetScene(new ConfigurationScene());
			else if (PlayButton.Clicked)
				objContext.GameController.SceneController.SetScene(new GameScene(new Entities.ScoresModel(1, 0, 3)));
		}

		/// <summary>
		///		Botón de configuración
		/// </summary>
		private ButtonControl ConfigurationButton { get; set; }

		/// <summary>
		///		Botón para comenzar a jugar
		/// </summary>
		private ButtonControl PlayButton { get; set; }

		/// <summary>
		///		Botón para salir del juego
		/// </summary>
		private ButtonControl CloseButton { get; set; }
	}
}
