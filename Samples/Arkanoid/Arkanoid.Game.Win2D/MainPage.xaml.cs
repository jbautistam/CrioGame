using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Contents.Sounds;
using Bau.Libraries.CrioGame.Common.Models.Resources;
using Bau.Libraries.CrioGame.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Libraries.ArkanoidGame.Win2D
{
  /// <summary>
  ///		Pantalla principal del juego
  /// </summary>
  public sealed partial class MainPage : Page
  {
		public MainPage()
		{	InitializeComponent();
		}

		/// <summary>
		///		Inicializa la página
		/// </summary>
		private void InitializePage()
		{
		}

		/// <summary>
		///		Arranca el juego
		/// </summary>
		private void StartGame()
		{	new Logic.GameController(PathData).Start(GetWin2DController(), new Repository.LevelsRepository(PathData));
		}

		/// <summary>
		///		Obtiene el controlador gráfico con Win2D
		/// </summary>
		private IGraphicsEngineManager GetWin2DController()
		{ CrioGame.Win2D.Win2DController objWin2D = new CrioGame.Win2D.Win2DController();

				// Inicializa los parámetros del controlador gráfico
					objWin2D.InitializeCanvas(Window.Current, cnvCanvas, DesignMode.DesignModeEnabled);
				// Devuelve el controlador
					return objWin2D;
		}

		/// <summary>
		///		Descarga la aplicación
		/// </summary>
		private void UnloadApp()
		{ // Elimina las referencias para que los controles de Win2D pasen por el Garbage Collector
				if (cnvCanvas != null)
					{ cnvCanvas.RemoveFromVisualTree();
						cnvCanvas = null;
					}
		}

		/// <summary>
		///		Directorio 
		/// </summary>
		public string PathData
		{ get { return System.IO.Path.Combine(Package.Current.InstalledLocation.Path, "Assets\\Data"); }
		}

		private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{ InitializePage();
		}

		private void Page_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{ UnloadApp();
		}

		private void cmdStart_Click(object sender, RoutedEventArgs e)
		{ StartGame();
			cmdStart.Visibility = Visibility.Collapsed;
		}
	}
}
