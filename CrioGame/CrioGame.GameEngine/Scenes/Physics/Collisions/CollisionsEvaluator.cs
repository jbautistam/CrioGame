using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Physics.Collisions
{
	/// <summary>
	///		Resultado de las colisiones
	/// </summary>
	internal class CollisionsEvaluator
	{
		/// <summary>
		///		Evalúa las colisiones
		/// </summary>
		internal void Evaluate(IView objView, Layers.LayerModelCollection objColLayers, IGameContext objContext)
		{ List<CollisionTargets> objColEntities = new List<CollisionTargets>();
		
				// Obtiene las entidades que pueden colisionar
					for (int intLayer = 0; intLayer < objColLayers.Count; intLayer++)
						if (objColLayers[intLayer].MustEvaluateCollisions)
							for (int intEntity = 0; intEntity < objColLayers[intLayer].Entities.Count; intEntity++)
								if (objColLayers[intLayer].Entities[intEntity].Active && 
										objColLayers[intLayer].Entities[intEntity] is AbstractActorModel)
									{ AbstractActorModel objSprite = objColLayers[intLayer].Entities[intEntity] as AbstractActorModel;

											// Si realmente tiene un componente para evaluar las colisiones
												if (objSprite.CollisionEvaluator != null)
													{ // Actualiza el evaluador de colisiones
															objSprite.CollisionEvaluator.Update(objContext, objView);
														// Añade el sprite a la colección de entidades
															objColEntities.Add(objSprite.CollisionEvaluator);
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
