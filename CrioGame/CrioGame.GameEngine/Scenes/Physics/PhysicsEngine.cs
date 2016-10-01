using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

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
		internal void Evaluate(IView objView, Layers.LayerModelCollection objColLayers, IGameContext objContext)
		{ CollisionsEngine.Evaluate(objView, objColLayers, objContext);
		}

		/// <summary>
		///		Motor para el cálculo de colisiones
		/// </summary>
		internal Collisions.CollisionsEvaluator CollisionsEngine { get; } = new Collisions.CollisionsEvaluator();
	}
}
