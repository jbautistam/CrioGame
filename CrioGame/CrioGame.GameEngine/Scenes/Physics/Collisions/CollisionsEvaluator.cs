using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Layers;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Physics.Collisions
{
	/// <summary>
	///		Evaluador de colisiones
	/// </summary>
	internal class CollisionsEvaluator
	{
		/// <summary>
		///		Evalúa las colisiciones entre objetos
		/// </summary>
		internal void Evaluate(IGameContext objContext, MapEntitiesCollection objColMapEntites)
		{ List<CollisionTargets> objColEntities = new List<CollisionTargets>();
		
				// Obtiene las entidades que pueden colisionar
					for (int intEntity = 0; intEntity < objColMapEntites.Count; intEntity++)
						if (// objColMapEntites[intEntity].IsActive &&
								objColMapEntites[intEntity].IsInitialized &&
								objColMapEntites[intEntity].Entity is AbstractActorModel &&
								objColMapEntites[intEntity].IsAtLayerEvaluateCollisions)
							{ AbstractActorModel objActor = objColMapEntites[intEntity].Entity as AbstractActorModel;

									// Si realmente tiene un componente para evaluar las colisiones
										if (objActor.CollisionEvaluator != null)
											{ // Actualiza el evaluador de colisiones
													objActor.CollisionEvaluator.Update(objContext, null);
												// Añade el sprite a la colección de entidades
													objColEntities.Add(objActor.CollisionEvaluator);
											}
							}
				// Comprueba las colisiones
					for (int intSource = 0; intSource < objColEntities.Count; intSource++)
						for (int intTarget = intSource + 1; intTarget < objColEntities.Count; intTarget++)
							if (objColEntities[intSource].Evaluate(objColEntities[intTarget]))
								{ objColEntities[intSource].Targets.Add(objColEntities[intTarget]);
									objColEntities[intTarget].Targets.Add(objColEntities[intSource]);
								}
		}
	}
}
