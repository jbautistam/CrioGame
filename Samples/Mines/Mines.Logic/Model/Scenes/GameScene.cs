using System;

using Bau.Libraries.Mines.Logic.Model.Entities;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Mines.Logic.Model.Scenes
{
	/// <summary>
	///		Escena con un nivel del juego
	/// </summary>
	internal class GameScene : CrioGame.GameEngine.Scenes.AbstractSceneModel
	{
		/// <summary>
		///		Añade las entidades a la escena
		/// </summary>
		public override void LoadContentScene(IGameContext objContext)
		{	// Añade un fondo fijo
				Map.AddGameEntity(ViewDefault, Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "MainBackground", 0));
			// Añade el fondo parallax
				Map.AddGameEntity(ViewDefault, Layer.Background.ToString(), 
													new BackgroundParallaxEntity(ViewDefault, "Parallax1", -2, 0, 0, 1), 
													Configuration.TimeSpanParallax);
				Map.AddGameEntity(ViewDefault, Layer.Background.ToString(), 
													new BackgroundParallaxEntity(ViewDefault, "Parallax2", -4, 0, 0, 2), 
													Configuration.TimeSpanParallax);
			// Añade los textos
				Map.AddGameEntity(ViewDefault, Layer.UserInterface.ToString(), 
													new UserInterfaceModel(this, 0, 3), 
													Configuration.TimeSpanUserInterface);
			// Añade las entidades
				Map.AddGameEntity(ViewDefault, LayerGame, 
													new PlayerModel(this, new GameObjectDimensions(0, ViewDefault.ViewPortScreen.Height / 2)));
				Map.AddControlEntity(new EnemySpawner(this), Configuration.TimeSpawnMine);
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
