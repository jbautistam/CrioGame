using System;

using Bau.Libraries.CrioGame.GameEngine;

namespace SpaceWarGame
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
						objEngine.GameEngine.ContentController.LoadResources(System.IO.File.ReadAllText(System.IO.Path.Combine(PathData, "Resources.txt")));
					// Crea la escena
						objEngine.GameEngine.SceneController.SetScene(new Model.Scenes.MainMenuScene());
					// Ejecuta el motor
						objEngine.Start();
				}
		}

		/// <summary>
		///		Directorio 
		/// </summary>
		public static string PathData
		{ get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"); }
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
		///		Tiempo entre los Spawns de las naves enemigas
		/// </summary>
		public static TimeSpan TimeSpawnShip { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de las naves enemigas
		/// </summary>
		public static TimeSpan TimeSpanShipUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos de las naves enemigas
		/// </summary>
		public static TimeSpan TimeSpanShipFire { get; } = TimeSpan.FromMilliseconds(1000);

		/// <summary>
		///		Tiempo entre los Updates de los disparos de las naves enemigas
		/// </summary>
		public static TimeSpan TimeSpanMineLaserUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos del jugador
		/// </summary>
		public static TimeSpan TimeSpanPlayerLaserFire { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de los disparos del jugador
		/// </summary>
		public static TimeSpan TimeSpanPlayerLaserUpdate { get; } = TimeSpan.FromMilliseconds(3);

		/// <summary>
		///		Tiempo entre los Updates de las explosiones de los enemigos
		/// </summary>
		public static TimeSpan TimeEnemyExplosion { get; } = TimeSpan.FromMilliseconds(10);
	}
#endif
}
