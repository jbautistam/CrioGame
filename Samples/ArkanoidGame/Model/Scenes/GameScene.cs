﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using ArkanoidGame.Model.Entities;

namespace ArkanoidGame.Model.Scenes
{
	/// <summary>
	///		Escena con un nivel del juego
	/// </summary>
	internal class GameScene : Bau.Libraries.CrioGame.GameEngine.Scenes.AbstractEngineSceneModel
	{
		public GameScene(ScoresModel objScores)
		{ Score = objScores;
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
		{ Vector2D objPosition;
			BallModel objBall;
			PaddleModel objPaddle;
			int intBricks;
			
				// Cambia la vista predeterminada
					ViewDefault.Camera.ViewPortPercentScreen = new Rectangle(0, 0, 80, 100);
				// Crea la vista de interface de usuario
					ViewUserInterface = CreateView("UserInterface", new Rectangle(80, 0, 20, 100),
																				 new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, 100, 100), 1);
				// Calcula las coordenadas (después de cambiar la cámara) y las entidades principales
					objPosition = new Vector2D(ViewDefault.ViewPortScreen.Width / 2, ViewDefault.ViewPortScreen.Height - 25);
					objBall = new BallModel(ViewDefault, new GameObjectDimensions(objPosition.X, ViewDefault.ViewPortScreen.Height / 2), 
																	new Vector2D(0, 5), TimeSpanBall);
					objPaddle = new PaddleModel(ViewDefault, new GameObjectDimensions(objPosition.X, objPosition.Y));
				// Añade un fondo fijo
					ViewDefault.AddEntity(Layer.Background.ToString(), new BackgroundEntity(ViewDefault, "UserInterfaceBackground", 0));
				// Añade los ladrillos
					intBricks = ShowBricks(ViewDefault, objContext, Score.Level);
				// Añade la pelota
					ViewDefault.AddEntity(LayerGame, objBall);
				// Añade el paddle
					ViewDefault.AddEntity(LayerGame, objPaddle);
				// Añade los textos
					ViewUserInterface.AddEntity(Layer.UserInterface.ToString(), 
																			new GameUserInterfaceModel(ViewUserInterface, ViewDefault, 
																																 Score, objPaddle, objBall, 
																																 intBricks, TimeSpanUserInterface));
		}

		/// <summary>
		///		Carga los ladrillos del nivel y los muestra en pantalla
		/// </summary>
		private int ShowBricks(IView objView, IGameContext objContext, int intLevel)
		{ System.Collections.Generic.List<BrickModel> objColBricks;

				// Carga los ladrillos
					objColBricks = new Repository.LevelsRepository().LoadBricks(objView, objContext, intLevel, TimeSpanBall);
				// Añade los ladrillos a la vista
					foreach (BrickModel objBrick in objColBricks)
						ViewDefault.AddEntity(LayerGame, objBrick);
				// Devuelve el número de ladrillos cargados
					return objColBricks.Count;
		}

		/// <summary>
		///		Actualiza los datos de la escena
		/// </summary>
		public override void UpdateScene(IGameContext objContext)
		{ // ... simplemente implementa la interface
		}

		/// <summary>
		///		Vista de interface de usuario
		/// </summary>
		public IView ViewUserInterface { get; private set; }

		/// <summary>
		///		Puntuación
		/// </summary>
		public ScoresModel Score { get; }

		/// <summary>
		///		Capa del juego
		/// </summary>
		public static string LayerGame { get; } = "Game";

		/// <summary>
		///		Tiempo entre los Updates de la puntuación
		/// </summary>
		public static TimeSpan TimeSpanScore { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre los Updates de la pelota
		/// </summary>
		public static TimeSpan TimeSpanBall { get; } = TimeSpan.FromMilliseconds(15);

		/// <summary>
		///		Tiempo entre las actualizaciones del interface de usuario
		/// </summary>
		public static TimeSpan TimeSpanUserInterface { get; } = TimeSpan.FromMilliseconds(30);
	}
}
