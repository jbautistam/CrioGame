using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.Common.Interfaces;

namespace MinesGameWin2D
{
	[Flags]
	public enum GroupGameObjects
		{ Player = 1,
			Enemy = 2,
			Other = 4
		}

	/// <summary>
	///		Pantalla principal del juego
	/// </summary>
	public sealed partial class MainPage : Page
	{ // Variables privadas
			private string [] arrStrImages = { "Assets\\Images\\imageTiger.jpg",
																						"Assets\\Images\\ParticleSystemNarrow.png"
																					};

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
						objEngine.Initialize(new Parameters.GameParameters());
					// Desactiva la música
						objEngine.GameParameters.Configuration.PlayMusic = false;
						objEngine.GameParameters.Configuration.PlayEffects = false;
					// Asigna la música de fondo
						objEngine.GameParameters.Configuration.ActualSong = "GameSong";
					// Añade el contenido
						AddContent(objEngine.GameEngine.ContentController);
					// Crea la escena
						objEngine.GameEngine.SceneController.SetScene(new Model.Scenes.GameScene());
					// Ejecuta el motor
						objEngine.Start();
				}
		}

		/// <summary>
		///		Obtiene el controlador gráfico con Win2D
		/// </summary>
		private IGraphicsEngineManager GetWin2DController()
		{ Bau.Libraries.CrioGame.Win2D.Win2DController objWin2D = new Bau.Libraries.CrioGame.Win2D.Win2DController();

				// Inicializa los parámetros del controlador gráfico
					objWin2D.InitializeCanvas(Window.Current, cnvCanvas, DesignMode.DesignModeEnabled);
				// Devuelve el controlador
					return objWin2D;
		}

		/// <summary>
		///		Añade los contenidos
		/// </summary>
		private void AddContent(IGameContentDictionary objContentController)
		{	SpriteSheetContent objSpriteSheet;

				// Añade los fondos
					objContentController.AddImage("MainBackground", "Assets\\Images\\Backgrounds\\mainbackground.png");
					objContentController.AddImage("Parallax1", "Assets\\Images\\Backgrounds\\bgLayer1.png");
					objContentController.AddImage("Parallax2", "Assets\\Images\\Backgrounds\\bgLayer2.png");
					objContentController.AddImage("Laser", "Assets\\Images\\Sprites\\laser.png");
				// Animación de la nave
					objContentController.AddImage("PlayerImage", "Assets\\Images\\Sprites\\shipAnimation.png");
					objSpriteSheet = new SpriteSheetContent("Player", "PlayerImage");
					objSpriteSheet.CreateSheet("Default", 1, 8, 115, 69)
							.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent("Player", objSpriteSheet);
				// Animación de la mina
					objContentController.AddImage("MineImage", "Assets\\Images\\Sprites\\mineAnimation.png");
					objSpriteSheet = new SpriteSheetContent("Mine", "MineImage");
					objSpriteSheet.CreateSheet("Default", 1, 8, 47, 61)
								.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent(objSpriteSheet.Key, objSpriteSheet);
				// Animación de la explosión
					objContentController.AddImage("ExplosionImage", "Assets\\Images\\Sprites\\explosion.png");
					objSpriteSheet = new SpriteSheetContent("Explosion", "ExplosionImage");
					objSpriteSheet.CreateSheet("Default", 1, 12, 133, 134)
								.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent(objSpriteSheet.Key, objSpriteSheet);
				// Añade una fuente
					objContentController.AddFont("Font", new Bau.Libraries.CrioGame.Common.Models.Contents.Graphics.FontContent("Font", "Fonts\\ScoreFont")
																											{ Family = "Arial",
																												Size = 24
																											}
																			);
				// Añade los sonidos
					objContentController.AddSound("GameSong", "Assets\\Sounds\\gameMusic.mp3", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Song);
					objContentController.AddSound(Configuration.LaserSound, "Assets\\Sounds\\laserFire.wav", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
					objContentController.AddSound(Configuration.ExplosionSound, "Assets\\Sounds\\explosion.wav", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
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
