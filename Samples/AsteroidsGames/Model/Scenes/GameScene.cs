﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using AsteroidsGame.Model.Entities;

namespace AsteroidsGame.Model.Scenes
{
	/// <summary>
	///		Escena con un nivel del juego
	/// </summary>
	internal class GameScene : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
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
				ViewDefault.AddEntity(Layer.Background.ToString(), 
															new BackgroundParallaxEntity(ViewDefault, "Stars1Background", Program.TimeSpanParallax,
																													 -2, 0, 0, 1));
			// Añade los textos
				ViewDefault.AddEntity(Layer.UserInterface.ToString(), 
															new UserInterfaceModel(ViewDefault, Scores, Program.TimeSpanUserInterface));
			// Añade las entidades
				ViewDefault.AddEntity(LayerGame, new PlayerModel(ViewDefault, 
																													new GameObjectDimensions(ViewDefault.ViewPortScreen.Width / 2, 
																																									ViewDefault.ViewPortScreen.Height / 2)));
				ViewDefault.AddEntity(LayerGame, new EnemySpawner(ViewDefault, Scores, Program.TimeSpawnMine));
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
