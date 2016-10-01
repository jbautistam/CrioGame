using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace MinesGame
{
	[Flags]
	public enum GroupGameObjects
		{ Player = 1,
			Enemy = 2,
			Other = 4
		}

#if WINDOWS || LINUX
	/// <summary>
	///		Clase principal de la aplicación
	/// </summary>
	public static class Program
	{	
		/// <summary>
		///		Punto de entrada a la aplicación
		/// </summary>
		[STAThread]
		static void Main()
		{	using (CrioEngine objEngine = new CrioEngine(new Bau.Libraries.CrioGame.MonogameImpl.MonogameController()))
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
		///		Añade los contenidos
		/// </summary>
		private static void AddContent(IGameContentDictionary objContentController)
		{	SpriteSheetContent objSpriteSheet;

				// Añade los fondos
					objContentController.AddImage("MainBackground", "Images\\Backgrounds\\mainbackground");
					objContentController.AddImage("Parallax1", "Images\\Backgrounds\\bgLayer1");
					objContentController.AddImage("Parallax2", "Images\\Backgrounds\\bgLayer2");
					objContentController.AddImage("Laser", "Images\\Sprites\\laser");
				// Animación de la nave
					objContentController.AddImage("PlayerImage", "Images\\Sprites\\shipAnimation");
					objSpriteSheet = new SpriteSheetContent("Player", "PlayerImage");
					objSpriteSheet.CreateSheet("Default", 1, 8, 115, 69)
							.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent("Player", objSpriteSheet);
				// Animación de la mina
					objContentController.AddImage("MineImage", "Images\\Sprites\\mineAnimation");
					objSpriteSheet = new SpriteSheetContent("Mine", "MineImage");
					objSpriteSheet.CreateSheet("Default", 1, 8, 47, 61)
								.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent(objSpriteSheet.Key, objSpriteSheet);
				// Animación de la explosión
					objContentController.AddImage("ExplosionImage", "Images\\Sprites\\explosion");
					objSpriteSheet = new SpriteSheetContent("Explosion", "ExplosionImage");
					objSpriteSheet.CreateSheet("Default", 1, 12, 133, 134)
								.CreateDefaultAnimation("Default", 30);
					objContentController.AddContent(objSpriteSheet.Key, objSpriteSheet);
				// Añade una fuente
					objContentController.AddFont("Font", "Fonts\\ScoreFont");
				// Añade los sonidos
					objContentController.AddSound("GameSong", "Sounds\\gameMusic", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Song);
					objContentController.AddSound(LaserSound, "Sounds\\laserFire", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
					objContentController.AddSound(ExplosionSound, "Sounds\\explosion", Bau.Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
		}

		/// <summary>
		///		Capa del juego
		/// </summary>
		public static string LayerGame { get; } = "Game";

		/// <summary>
		///		Sonido de una explosión
		/// </summary>
		public static string ExplosionSound { get; } =  "ExplosionSound";

		/// <summary>
		///		Sonido de un láser
		/// </summary>
		public static string LaserSound { get; } =  "LaserSound";

		/// <summary>
		///		Tiempo entre los Updates de los fondos parallax
		/// </summary>
		public static TimeSpan TimeSpanParallax { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre los Updates de la puntuación
		/// </summary>
		public static TimeSpan TimeSpanScore { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre las actualizaciones del interface de usuario
		/// </summary>
		public static TimeSpan TimeSpanUserInterface { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre los Spawns de las minas
		/// </summary>
		public static TimeSpan TimeSpawnMine { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de las minas
		/// </summary>
		public static TimeSpan TimeSpanMineUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos de las minas
		/// </summary>
		public static TimeSpan TimeSpanMineFire { get; } = TimeSpan.FromMilliseconds(1000);

		/// <summary>
		///		Tiempo entre los Updates de los disparos de las minas
		/// </summary>
		public static TimeSpan TimeSpanMineLaserUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos del jugador
		/// </summary>
		public static TimeSpan TimeSpanPlayerLaserFire { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de los disparos del jugador
		/// </summary>
		public static TimeSpan TimeSpanPlayerLaserUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los Updates de las explosiones de los enemigos
		/// </summary>
		public static TimeSpan TimeEnemyExplosion { get; } = TimeSpan.FromMilliseconds(10);
	}
#endif
}
