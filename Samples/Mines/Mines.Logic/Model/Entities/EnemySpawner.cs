using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities;

namespace Bau.Libraries.Mines.Logic.Model.Entities
{
	/// <summary>
	///		Creador de minas
	/// </summary>
	internal class EnemySpawner : AbstractEntitySpawner
	{
		public EnemySpawner(IScene objScene, TimeSpan tsSpawnTime, int intProbability = 75) : base(objScene, tsSpawnTime, intProbability) 
		{ 
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
		{ Scene.Map.AddGameEntity(Scene.ViewDefault, Configuration.LayerGame, 
															new MineModel(Scene, 
																						new GameObjectDimensions(Scene.ViewDefault.ViewPortScreen.Width, 
																																		 objContext.MathHelper.Random((int) Scene.ViewDefault.ViewPortScreen.Height)),
																						new Vector2D(-3, 0), 
																						Configuration.TimeSpanMineUpdate, 
																						Configuration.TimeSpanMineFire));
		}
	}
}
