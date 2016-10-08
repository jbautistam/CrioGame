using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Mines.Game.MonoGame
{
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
		{	using (CrioEngine objEngine = new CrioEngine(new Libraries.CrioGame.MonogameImpl.MonogameController()))
				{ // Inicializa el motor de juego
						objEngine.Initialize(new Libraries.Mines.Logic.Parameters.GameParameters());
					// Desactiva la música
						objEngine.GameParameters.Configuration.PlayMusic = false;
						objEngine.GameParameters.Configuration.PlayEffects = false;
					// Asigna la música de fondo
						objEngine.GameParameters.Configuration.ActualSong = "GameSong";
					// Añade el contenido
						AddContent(objEngine.GameEngine.ContentController);
					// Crea la escena
						objEngine.GameEngine.SceneController.SetScene(new Libraries.Mines.Logic.Model.Scenes.GameScene());
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
					objContentController.AddSound("GameSong", "Sounds\\gameMusic", 
																				Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Song);
					objContentController.AddSound(Libraries.Mines.Logic.Configuration.LaserSound, "Sounds\\laserFire", 
																				Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
					objContentController.AddSound(Libraries.Mines.Logic.Configuration.ExplosionSound, "Sounds\\explosion", 
																				Libraries.CrioGame.Common.Models.Contents.Sounds.SoundContent.SoundType.Effect);
		}
	}
#endif
}
