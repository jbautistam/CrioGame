using System;

using MinesGameWin2D.Model.Entities;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace MinesGameWin2D.Model.Scenes
{
	/// <summary>
	///		Escena con un nivel del juego
	/// </summary>
	internal class GameScene : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
	{
		/// <summary>
		///		Añade las entidades a la escena
		/// </summary>
		public override void LoadContentScene(IGameContext objContext)
		{	ScoresModel objScore = new ScoresModel(0, 3);

				// Añade un fondo fijo
					ViewDefault.AddEntity(Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "MainBackground", 0));
				// Añade el fondo parallax
					ViewDefault.AddEntity(Layer.Background.ToString(), 
																new BackgroundParallaxEntity(ViewDefault, "Parallax1", Configuration.TimeSpanParallax,
																														 -2, 0, 0, 1));
					ViewDefault.AddEntity(Layer.Background.ToString(), 
																new BackgroundParallaxEntity(ViewDefault, "Parallax2", Configuration.TimeSpanParallax,
																														 -4, 0, 0, 2));
				// Añade los textos
					ViewDefault.AddEntity(Layer.UserInterface.ToString(), 
																new UserInterfaceModel(ViewDefault, objScore, Configuration.TimeSpanUserInterface));
				// Añade las entidades
					ViewDefault.AddEntity(LayerGame, new PlayerModel(ViewDefault, new GameObjectDimensions(0, 0)));
					ViewDefault.AddEntity(LayerGame, new EnemySpawner(ViewDefault, Configuration.TimeSpawnMine));
		}

		/// <summary>
		///		Inicializa la escena
		/// </summary>
		public override void InitializeScene(IGameContext objContext)
		{ // ... simplemente implementa la interface
		}

		/// <summary>
		///		Actualiza los datos de la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ // ... simplemente implementa la interface
		}

		/// <summary>
		///		Capa del juego
		/// </summary>
		public static string LayerGame { get; } = "Game";
	}
}
