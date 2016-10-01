using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface;

namespace SpaceWarGame.Model.Scenes
{
	/// <summary>
	///		Escena con el menú de configuración del juego
	/// </summary>
	internal class ConfigurationScene: Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
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
		{ // Crea los checkbox
				SongCheckBox = CheckBoxControl.Create(objContext, 
																							objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic, 
																							ViewDefault.ViewPortScreen.Width / 2 - 200, 50, 
																							"UIImage", "Controls",
																							"", "WithSound", "WithoutSound",
																							"Font", "Con musica de fondo", "Sin musica de fondo",
																							ColorEngine.White, ColorEngine.White);
				EffectsCheckBox = CheckBoxControl.Create(objContext, objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects, 
																								 ViewDefault.ViewPortScreen.Width / 2 - 200, 150, 
																								 "UIImage", "Controls",
																								 "", "WithSound", "WithoutSound",
																								 "Font", "Con efectos de sonido", "Sin efectos de sonido",
																								 ColorEngine.White, ColorEngine.White);
			// Crea los botones
				AcceptButton = ButtonControl.Create(objContext, ViewDefault.ViewPortScreen.Width / 3, 3 * ViewDefault.ViewPortScreen.Height / 4,
																						"UIImage", "Controls",
																						"ButtonBlue1", "ButtonYellow1", 
																						"Font", "Aceptar", ColorEngine.Black, ColorEngine.Red);
				CancelButton = ButtonControl.Create(objContext, 2 * ViewDefault.ViewPortScreen.Width / 3, 3 * ViewDefault.ViewPortScreen.Height / 4,
																						"UIImage", "Controls",
																						"ButtonRed1", "ButtonRed2", 
																						"Font", "Cancelar", ColorEngine.Black, ColorEngine.White);
			// Añade el fondo
				ViewDefault.AddEntity(Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "Stars1Background", 0, 0, 0));
			// Añade los botones
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), SongCheckBox);
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), EffectsCheckBox);
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), AcceptButton);
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), CancelButton);
		}

		/// <summary>
		///		Modifica la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Escape) ||
					CancelButton.Clicked)
				objContext.GameController.SceneController.SetScene(new MainMenuScene());
			else if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedKey(Bau.Libraries.CrioGame.Common.Enums.Keys.Enter) ||
								AcceptButton.Clicked)
				{	// Configura el juego
						objContext.GameController.MainManager.GameParameters.Configuration.PlayMusic = SongCheckBox.IsChecked;
						objContext.GameController.MainManager.GameParameters.Configuration.PlayEffects = EffectsCheckBox.IsChecked;
					// Toca o detiene la música
						if (SongCheckBox.IsChecked)
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Play(objContext.GameController.MainManager.GameParameters.Configuration.ActualSong);
						else
							objContext.GameController.MainManager.GraphicsEngine.SoundController.Stop();
					// Vuelve al menú principal
						objContext.GameController.SceneController.SetScene(new MainMenuScene());
				}
		}

		/// <summary>
		///		Botón para aceptar
		/// </summary>
		private ButtonControl AcceptButton { get; set; }

		/// <summary>
		///		Botón para cancelar
		/// </summary>
		private ButtonControl CancelButton { get; set; }

		/// <summary>
		///		Checkbox para tocar la música de fondo
		/// </summary>
		private CheckBoxControl SongCheckBox { get; set; }

		/// <summary>
		///		Checkbox para tocar los efectos de sonido
		/// </summary>
		private CheckBoxControl EffectsCheckBox { get; set; }
	}
}
