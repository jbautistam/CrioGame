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
		public EnemySpawner(IView objView, TimeSpan tsSpawnTime, int intProbability = 75) : base(tsSpawnTime, intProbability) 
		{ View = objView;
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
		{ View.AddEntity(Configuration.LayerGame, 
										 new MineModel(View, 
																	 new GameObjectDimensions(View.ViewPortScreen.Width, 
																														objContext.MathHelper.Random((int) View.ViewPortScreen.Height)),
																	 new Vector2D(-3, 0), Configuration.TimeSpanMineUpdate, Configuration.TimeSpanMineFire));
		}

		/// <summary>
		///		Vista en la que se añaden los enemigos
		/// </summary>
		private IView View { get; }
	}
}
