using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.Asteroids.Game.Logic.Model.Entities;

namespace Bau.Libraries.Asteroids.Game.Logic.Model.Scenes
{
	/// <summary>
	///		Escena con un nivel del juego
	/// </summary>
	internal class GameScene : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractSceneModel
	{
		internal GameScene(ScoresModel objScores)
		{ Scores = objScores;
		}

		/// <summary>
		///		Añade las entidades a la escena
		/// </summary>
		public override void LoadContentScene(IGameContext objContext)
		{	
		}

		/// <summary>
		///		Inicializa la escena
		/// </summary>
		public override void InitializeScene(IGameContext objContext)
		{ // Añade el fondo parallax
				Map.AddGameEntity(ViewDefault, Layer.Background.ToString(), 
													new BackgroundParallaxEntity(ViewDefault, "Stars1Background", Configuration.TimeSpanParallax,
																											 -2, 0, 0, 1));
			// Añade los textos
				Map.AddGameEntity(ViewDefault, Layer.UserInterface.ToString(), 
													new UserInterfaceModel(this, Scores, Configuration.TimeSpanUserInterface));
			// Añade las entidades
				Map.AddGameEntity(ViewDefault, LayerGame, 
													new PlayerModel(this, 
																					new GameObjectDimensions(ViewDefault.ViewPortScreen.Width / 2, 
																																	 ViewDefault.ViewPortScreen.Height / 2)));
				Map.AddGameEntity(ViewDefault, LayerGame, 
													new EnemySpawner(this, Scores, Configuration.TimeSpawnMine));
		}

		/// <summary>
		///		Actualiza los datos de la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ // ... simplemente implementa la interface
		}

		/// <summary>
		///		Puntuación
		/// </summary>
		public ScoresModel Scores { get; }

		/// <summary>
		///		Capa del juego
		/// </summary>
		public static string LayerGame { get; } = "Game";
	}
}
