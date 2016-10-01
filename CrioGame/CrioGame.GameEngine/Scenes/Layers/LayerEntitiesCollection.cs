using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Colección de entidades: trata las claves, recicla los elementos...
	/// </summary>
	internal class LayerEntitiesCollection : Common.Models.Collections.ObjectPool<AbstractModelBase>
	{ // Variables privadas
			private TimeSpan tsLastRecover = TimeSpan.Zero;

		/// <summary>
		///		Ordena por el ZOrder
		/// </summary>
		internal void SortByZOrder()
		{ base.Sort((objFirst, objSecond) => 
													{ if (objFirst != null && objSecond != null && 
																objFirst is AbstractDrawableModelBase &&
																objSecond is AbstractDrawableModelBase)
															return (objFirst as AbstractDrawableModelBase).ZOrder.CompareTo((objSecond as AbstractDrawableModelBase).ZOrder);
														else
															return 0;
													});
		}

		/// <summary>
		///		Inicializa la colección de entidades
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{	for (int intIndex = 0; intIndex < Count; intIndex++)
				this[intIndex].Initialize(objContext);
		}

		/// <summary>
		///		Modifica las entidades
		/// </summary>
		internal void Update(IGameContext objContext)
		{	// Recupera la memoria si es necesario
				RecoverMemory(objContext);
			// Modifica los elementos
				for (int intIndex = 0; intIndex < Count; intIndex++)
					if (this[intIndex].Active && 
								(this[intIndex].TimeBetweenUpdate == TimeSpan.Zero ||
								 (objContext.ActualTime - this[intIndex].TimeLastUpdate).TotalMilliseconds > this[intIndex].TimeBetweenUpdate.TotalMilliseconds))
						{ // Guarda el momento actual
								this[intIndex].TimeLastUpdate = objContext.ActualTime;
							// Modifica la entidad
								this[intIndex].Update(objContext);
						}
		}

		/// <summary>
		///		Dibuja las entidades
		/// </summary>
		internal void Draw(IGameContext objContext, Rectangle rctCamera)
		{	for (int intIndex = 0; intIndex < Count; intIndex++)
				if (this[intIndex].Active)
					{ if (this[intIndex] is Entities.Graphics.AbstractActorModel)
							{ Entities.Graphics.AbstractActorModel objGameObjet = this[intIndex] as Entities.Graphics.AbstractActorModel;

									if (objGameObjet != null)
										objGameObjet.Draw(objContext);
							}
						else if (this[intIndex] is AbstractDrawableModelBase)
							{ AbstractDrawableModelBase objGameObject = this[intIndex] as AbstractDrawableModelBase;

									if (objGameObject != null)
										objGameObject.Draw(objContext, rctCamera);
							}
					}
		}
	}
}
