using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
{
	/// <summary>
	///		Creador de minas
	/// </summary>
	internal class EnemySpawner : AbstractEntitySpawner
	{
		public EnemySpawner(IScene objScene, ScoresModel objScore, TimeSpan tsSpawnTime, int intProbability = 75) : base(objScene, tsSpawnTime, intProbability) 
		{ Scores = objScore;
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
		{ // Crea asteroides de fondo
				if (Scores.Asteroids < Scores.Level * 10)
					Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame, 
																  new RockModel(Scene, this, 
																							  new GameObjectDimensions(objContext.MathHelper.Random((int) Scene.ViewDefault.ViewPortScreen.Width - 50), 0), 
																							  new Vector2D(0, 5),
																							  $"Rock{objContext.MathHelper.Random(5) + 1}",
																							  Configuration.TimeSpanShipUpdate));
			// Crea naves enemigas adicionales
				if ((objContext.ActualTime - TimeLastBoss).Seconds > 5)
					{	// Añade la nave
							Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame, 
																		  new ShipBossModel(Scene, 
																											  new GameObjectDimensions(objContext.MathHelper.Random((int) Scene.ViewDefault.ViewPortScreen.Width - 50), 0),
																											  objContext.MathHelper.Random(4, 9),
																											  new Vector2D(0, objContext.MathHelper.Random(2, 5)),
																											  ColorEngine.AntiqueWhite, Configuration.TimeSpanShipUpdate));
						// Guarda el momento en que se ha añadido la nvae
							TimeLastBoss = objContext.ActualTime;
					}
		}

		/// <summary>
		///		Indica que se ha eliminado un asteroide
		/// </summary>
		internal void ComputeAsteroidKill()
		{ Scores.Asteroids--;
		}

		/// <summary>
		///		Indica que se ha eliminado una nave enemiga
		/// </summary>
		internal void ComputeShipKill()
		{ Scores.Ships--;
		}

		/// <summary>
		///		Puntuación
		/// </summary>
		private ScoresModel Scores { get; }

		/// <summary>
		///		Momento en que se lanzó la última nave adicional
		/// </summary>
		private TimeSpan TimeLastBoss { get; set; } = TimeSpan.Zero;
	}
}
