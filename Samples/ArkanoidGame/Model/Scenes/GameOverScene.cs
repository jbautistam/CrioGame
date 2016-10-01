﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace ArkanoidGame.Model.Scenes
{
	/// <summary>
	///		Escena con el final del juego
	/// </summary>
	internal class GameOverScene : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
	{
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
		{ ViewDefault.AddEntity(Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "GameOverBackground", 0));
		}

		/// <summary>
		///		Modifica la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyKey() ||
					objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedAnyMouseButton())
				objContext.GameController.SceneController.SetScene(new MainMenuScene());
		}
	}
}
