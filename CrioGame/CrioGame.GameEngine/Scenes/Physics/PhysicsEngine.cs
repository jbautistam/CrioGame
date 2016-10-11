using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Layers;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Physics
{
	/// <summary>
	///		Motor de físicas
	/// </summary>
	internal class PhysicsEngine
	{
		/// <summary>
		///		Evalúa los diferentes controladores de físicas
		/// </summary>
		internal void Evaluate(IGameContext objContext, MapEntitiesCollection objColMapEntites)
		{ CollisionsEngine.Evaluate(objContext, objColMapEntites);
		}

		/// <summary>
		///		Motor para el cálculo de colisiones
		/// </summary>
		internal Collisions.CollisionsEngine CollisionsEngine { get; } = new Collisions.CollisionsEngine();
	}
}
