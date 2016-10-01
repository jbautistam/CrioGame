using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities;

namespace AsteroidsGame.Model.Entities
{
	/// <summary>
	///		Creador de minas
	/// </summary>
	internal class EnemySpawner : AbstractEntitySpawner
	{
		public EnemySpawner(IView objView, ScoresModel objScore, TimeSpan tsSpawnTime, int intProbability = 75) : base(tsSpawnTime, intProbability) 
		{ View = objView;
			Scores = objScore;
		}

		/// <summary>
		///		Inicializa la entidad
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // ... no hace nada, simplemente implementa la interface
		}

		/// <summary>
		///		Crea una nueva entidad
		/// </summary>
		protected override void Create(IGameContext objContext)
		{ if (Scores.Asteroids < Scores.Level * 10)
				{ GameObjectDimensions objDimensions = new GameObjectDimensions(0, 0);

						// Selecciona las dimensiones
							switch (objContext.MathHelper.Random(4))
								{	case 0: // Superior hacia abajo
											objDimensions = new GameObjectDimensions(objContext.MathHelper.Random((int) View.ViewPortScreen.Width), 
																															 0);
										break;
									case 1: // Inferior hacia arriba
											objDimensions = new GameObjectDimensions(objContext.MathHelper.Random((int) View.ViewPortScreen.Width), 
																															 View.ViewPortScreen.Height);
										break;
									case 2: // Derecha hacia la izquierda
											objDimensions = new GameObjectDimensions(View.ViewPortScreen.Width,
																															 objContext.MathHelper.Random((int) View.ViewPortScreen.Height));
										break;
									case 3: // Izquierda hacia la derecha
											objDimensions = new GameObjectDimensions(0,
																															 objContext.MathHelper.Random((int) View.ViewPortScreen.Height));
										break;
								}
						// Crea el meteorito
							View.AddEntity(Program.LayerGame, 
														 new RockModel(View, this, objDimensions, 
																					 new Polar2D(objContext.MathHelper.Random((int) (Math.PI * 2)), objContext.MathHelper.Random(2, 10)),
																					 $"Rock{objContext.MathHelper.Random(5) + 1}",
																					 Program.TimeSpanMineUpdate));
				}
		}

		/// <summary>
		///		Indica que se ha eliminado un asteroide
		/// </summary>
		internal void ComputeAsteroidKill()
		{ Scores.Asteroids--;
		}

		/// <summary>
		///		Vista en la que se añaden los enemigos
		/// </summary>
		private IView View { get; }

		/// <summary>
		///		Puntuación
		/// </summary>
		private ScoresModel Scores { get; }
	}
}
