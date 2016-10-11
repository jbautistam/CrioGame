﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Scenes
{
	/// <summary>
	///		Escena con el menú de siguiente nivel
	/// </summary>
	internal class MainNextLevel : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractSceneModel
	{
		public MainNextLevel(Entities.ScoresModel objScores)
		{ Scores = objScores;
		}

		/// <summary>
		///		Añade las entidades a la escena
		/// </summary>
		public override void LoadContentScene(IGameContext objContext)
		{	// ... simplemente implementa la interface
		}

		/// <summary>
		///		Inicializa la escena
		/// </summary>
		public override void InitializeScene(IGameContext objContext)
		{ Map.AddGameEntity(ViewDefault, Layer.Background.ToString(), 
												new BackgroundEntity(ViewDefault, "MenuBackground", 0));
			Map.AddGameEntity(ViewDefault, Layer.UserInterface.ToString(),
												new TextModel(null, "Font", "Enhorabuena, pulse una tecla para pasar de nivel",
																			new GameObjectDimensions(ViewDefault.ViewPortScreen.Width / 2 - 200,
																															 ViewDefault.ViewPortScreen.Height / 2,
																															 ColorEngine.White)));
		}

		/// <summary>
		///		Modifica la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyKey() ||
					objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyMouseButton())
				{ // Incrementa al nivel
						Scores.Level++;
					// Cambia la escena
						objContext.GameController.SceneController.SetScene(new GameScene(Scores));
				}
		}

		/// <summary>
		///		Puntuaciones
		/// </summary>
		public Entities.ScoresModel Scores { get; }
	}
}
