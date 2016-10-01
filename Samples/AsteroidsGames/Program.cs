using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Contents.Sounds;
using Bau.Libraries.CrioGame.Common.Models.Resources;
using Bau.Libraries.CrioGame.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace AsteroidsGame
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
						objEngine.GameEngine.SceneController.SetScene(new Model.Scenes.MainMenuScene());
					// Ejecuta el motor
						objEngine.Start();
				}
		}

		/// <summary>
		///		Añade los contenidos
		/// </summary>
		private static void AddContent(IGameContentDictionary objContentController)
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
						objColResources = new Bau.Libraries.CrioGame.Common.Repository.ResourcesRepository().Load(System.IO.File.ReadAllText(strFileName));
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
