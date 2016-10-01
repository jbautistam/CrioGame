using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Mundo del juego
	/// </summary>
	public class SceneController : ISceneController
	{
		public SceneController(Common.Interfaces.IGameEngineManager objGameController)
		{ GameController = objGameController;
		}

		/// <summary>
		///		Cambia la escena actual
		/// </summary>
		public void SetScene(IScene objNewScene)
		{ // Carga el contenido e inicializa la escena
				objNewScene.LoadContent(GameController.GameContext);
				if (GameController.MainManager.GraphicsEngine.IsStarted)
					objNewScene.Initialize(GameController.GameContext);
			// Cambia la escena
				ActualScene = objNewScene;
		}

		/// <summary>
		///		Motor del juego
		/// </summary>
		internal Common.Interfaces.IGameEngineManager GameController { get; }

		/// <summary>
		///		Escena actual
		/// </summary>
		public IScene ActualScene { get; private set; } = new Scenes.NullSceneModel();
	}
}
