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
		{ using (CrioEngine objEngine = new CrioEngine(GetWin2DController()))
				{ // Inicializa el motor de juego
						objEngine.Initialize(new Logic.Parameters.GameParameters());
					// Desactiva la música
						objEngine.GameParameters.Configuration.PlayMusic = false;
						objEngine.GameParameters.Configuration.PlayEffects = false;
					// Asigna la música de fondo
						objEngine.GameParameters.Configuration.ActualSong = "GameSong";
					// Añade el contenido
						AddContent(objEngine.GameEngine.ContentController);
					// Crea la escena
						objEngine.GameEngine.SceneController.SetScene(new Logic.Model.Scenes.MainMenuScene(new Repository.LevelsRepository(PathData)));
					// Ejecuta el motor
						objEngine.Start();
				}
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
		///		Añade los contenidos
		/// </summary>
		private void AddContent(IGameContentDictionary objContentController)
		{	List<ResourceModel> objColResources = LoadResources(System.IO.Path.Combine(PathData, "Resources.txt"));

				// Añade los recursos
					foreach (ResourceModel objResource in objColResources)
						switch (objResource.Type)
							{	case ResourceModel.ResourceType.Image:
										objContentController.AddImage(objResource.Key, objResource.Path);
									break;
								case ResourceModel.ResourceType.Font:
										objContentController.AddFont(objResource.Key, objResource.Path);
									break;
								case ResourceModel.ResourceType.Song:
										objContentController.AddSound(objResource.Key, objResource.Path, SoundContent.SoundType.Song);
									break;
								case ResourceModel.ResourceType.Effect:
										objContentController.AddSound(objResource.Key, objResource.Path, SoundContent.SoundType.Effect);
									break;
								case ResourceModel.ResourceType.SpriteSheet:
										objContentController.AddContent(objResource.Key, GetSpriteSheet(objResource));
									break;
							}
		}

		/// <summary>
		///		Carga los recursos de un archivo
		/// </summary>
		private static List<ResourceModel> LoadResources(string strFileName)
		{	List<ResourceModel> objColResources;

				// Carga el archivo
					if (System.IO.File.Exists(strFileName))
						objColResources = new CrioGame.Common.Repository.ResourcesRepository().Load(System.IO.File.ReadAllText(strFileName));
					else
						objColResources = new List<ResourceModel>();
				// Devuelve la colección de recursos
					return objColResources;
		}

		/// <summary>
		///		Obtiene un <see cref="SpriteSheetContent"/> a partir de un <see cref="ResourceModel"/>
		/// </summary>
		private static SpriteSheetContent GetSpriteSheet(ResourceModel objResource)
		{ SpriteSheetContent objSpriteSheet = new SpriteSheetContent(objResource.Key, objResource.Path);

				// Añade los rectángulos
					foreach (ResourceSheetModel objSheet in objResource.Sheets)
						{ SpriteSheetFrames objFrames = objSpriteSheet.CreateSheet(objSheet.Key, objSheet.Rectangles.ToArray());

								// Añade las animaciones
									foreach (ResourceAnimationModel objAnimation in objSheet.Animations)
										if (objAnimation.Frames.Count > 0)
											objFrames.CreateAnimation(objAnimation.Key, objAnimation.Frames.ToArray(), objAnimation.FrameTime);
										else
											objFrames.CreateDefaultAnimation(objAnimation.Key, objAnimation.FrameTime);
						}
				// Devuelve el spriteSheet
					return objSpriteSheet;
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
