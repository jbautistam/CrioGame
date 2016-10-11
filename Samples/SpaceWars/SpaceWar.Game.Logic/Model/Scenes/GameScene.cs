using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.SpaceWar.Game.Logic.Model.Entities;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Scenes
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
													new BackgroundParallaxEntity(ViewDefault, "Stars1Background",
																											 -2, 0, 0, 1), 
													Configuration.TimeSpanParallax);
			// Añade los textos
				Map.AddGameEntity(ViewDefault, Layer.UserInterface.ToString(), 
													new UserInterfaceModel(this, Scores), 
													Configuration.TimeSpanUserInterface);
			// Añade el jugador y el creador de entidades
				Map.AddGameEntity(ViewDefault, LayerGame, 
													new PlayerModel(this, 
																					new GameObjectDimensions(ViewDefault.ViewPortScreen.Width / 2, 
																																	 ViewDefault.ViewPortScreen.Height - 80)));
				Map.AddControlEntity(new EnemySpawner(this, Scores), 
														 Configuration.TimeSpawnShip);
			// Crea las naves básicas
				for (int intRow = 0; intRow < 3; intRow++)
					for (int intColumn = 0; intColumn < 8; intColumn++)
						{ // Añade la entidad
								Map.AddGameEntity(ViewDefault, LayerGame,
																	new ShipModel(this, new GameObjectDimensions(100 + intColumn * 80, 35 + intRow * 60, 0, 0, 0.5f),
																								objContext.MathHelper.Random(1, 3), 
																								new Vector2D(3, 3), 80, GetColor(intRow, intColumn)), 
																	TimeSpan.FromMilliseconds(20));
							// Indica que hay una nave más
								Scores.Ships++;
						}
		}

		/// <summary>
		///		Obtiene el color de una nave en una fila y columna
		/// </summary>
		private ColorEngine GetColor(int intRow, int intColumn)
		{ switch ((intRow * intColumn) % 3)
				{	case 0:
						return ColorEngine.Green;
					case 1:
						return ColorEngine.Yellow;
					default:
						return ColorEngine.Navy;
				}
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
