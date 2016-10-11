using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Layers;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Physics.Collisions
{
	/// <summary>
	///		Evaluador de colisiones
	/// </summary>
	internal class CollisionsEngine
	{
		/// <summary>
		///		Evalúa las colisiones entre objetos
		/// </summary>
		internal void Evaluate(IGameContext objContext, MapEntitiesCollection objColMapEntites)
		{ List<CollisionEvaluator> objColEvaluator = new List<CollisionEvaluator>();
		
				// Obtiene las entidades que pueden colisionar
					for (int intEntity = 0; intEntity < objColMapEntites.Count; intEntity++)
						if (objColMapEntites[intEntity].IsInitialized &&
								objColMapEntites[intEntity].IsAtLayerEvaluateCollisions)
							{ AbstractActorModel objActor = objColMapEntites[intEntity].Entity as AbstractActorModel;

									// Si realmente tiene un componente para evaluar las colisiones
										if (objActor != null && objActor.CollisionEvaluator != null)
											{ // Actualiza el evaluador de colisiones
													objActor.CollisionEvaluator.Update(objContext, null);
												// Añade el sprite a la colección de entidades
													objColEvaluator.Add(objActor.CollisionEvaluator);
											}
							}
				// Comprueba las colisiones
					for (int intSource = 0; intSource < objColEvaluator.Count; intSource++)
						for (int intTarget = intSource + 1; intTarget < objColEvaluator.Count; intTarget++)
							if (objColEvaluator[intSource].Evaluate(objColEvaluator[intTarget]))
								{ objColEvaluator[intSource].Targets.Add(objColEvaluator[intTarget]);
									objColEvaluator[intTarget].Targets.Add(objColEvaluator[intSource]);
								}
		}
	}
}
