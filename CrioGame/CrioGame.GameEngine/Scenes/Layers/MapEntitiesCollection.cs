using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Colección de entidades del mapa: trata las claves, recicla los elementos...
	/// </summary>
	internal class MapEntitiesCollection : Common.Models.Collections.ObjectPool<MapEntityModel>
	{ // Variables privadas
			private TimeSpan tsLastRecover = TimeSpan.Zero;

		internal MapEntitiesCollection(MapModel objMap, bool blnIsDrawable)
		{ Map = objMap;
			IsDrawable = blnIsDrawable;
		}

		/// <summary>
		///		Añade una entidad a la colección (sin vistas)
		/// </summary>
		internal void Add(AbstractModelBase objEntity, TimeSpan tsBetweenUpdate)
		{ Add(new MapEntityModel(new List<ViewLayerModel>(), objEntity, tsBetweenUpdate));
		}

		/// <summary>
		///		Añade una entidad a la colección (con vistas)
		/// </summary>
		internal void Add(List<ViewLayerModel> objColViewLayers, AbstractModelBase objEntity, TimeSpan tsBetweenUpdate)
		{ Add(new MapEntityModel(objColViewLayers, objEntity, tsBetweenUpdate));
		}

		/// <summary>
		///		Elimina una entidad de control
		/// </summary>
		internal void Remove(AbstractModelBase objEntity)
		{	bool blnDeleted = false;

				// Busca el elemento en la colección y lo borra
					for (int intIndex = 0; intIndex < Count && !blnDeleted; intIndex++)
						if (ReferenceEquals(this[intIndex].Entity, objEntity))
							{ // Elimina la entidad
									base.RemoveEntity(intIndex);
								// Indica que se ha borrado un elemento
									blnDeleted = true;
							}
					if (!blnDeleted)
						 System.Diagnostics.Debug.WriteLine("oops");
		}

		///// <summary>
		/////		Ordena por el ZOrder
		///// </summary>
		//internal void SortByZOrder()
		//{ base.Sort((objFirst, objSecond) => 
		//											{ if (objFirst != null && objSecond != null && 
		//														objFirst.Entity is AbstractDrawableModelBase &&
		//														objSecond.Entity is AbstractDrawableModelBase)
		//													return (objFirst.Entity as AbstractDrawableModelBase).ZOrder.CompareTo((objSecond.Entity as AbstractDrawableModelBase).ZOrder);
		//												else
		//													return 0;
		//											});
		//}

		/// <summary>
		///		Inicializa la colección de entidades
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{	for (int intIndex = 0; intIndex < Count; intIndex++)
				if (!this[intIndex].IsInitialized)
					this[intIndex].Initialize(objContext);
		}

		/// <summary>
		///		Modifica las entidades
		/// </summary>
		internal void Update(IGameContext objContext)
		{	// Recupera la memoria si es necesario
				RecoverMemory(objContext);
			// Trata las colisiones (sólo en los mapas "dibujables", las entidades de control no pueden colisionar entre sí)
				if (IsDrawable)
					PhysicsEngine.Evaluate(objContext, this);
			// Modifica los elementos
				for (int intIndex = 0; intIndex < Count; intIndex++)
					if (this[intIndex].IsInitialized &&
							(this[intIndex].TimeBetweenUpdate == TimeSpan.Zero ||
								 (objContext.ActualTime - this[intIndex].TimeLastUpdate).TotalMilliseconds > this[intIndex].TimeBetweenUpdate.TotalMilliseconds))
						{ // Guarda el momento actual
								this[intIndex].TimeLastUpdate = objContext.ActualTime;
							// Modifica la entidad
								this[intIndex].Update(objContext);
						}
		}

		/// <summary>
		///		Dibuja las entidades que se encuentran en las vistas y capas adecuadas
		/// </summary>
		internal void Draw(IGameContext objContext)
		{ for (int intView = 0; intView < Map.Scene.Views.Count; intView++)
				{ // Dibuja la capa de fondo
						Draw(objContext, Map.Scene.Views[intView], Map.Scene.Views[intView].Background);
					// Dibuja las capas de juego
						for (int intLayer = 0; intLayer < Map.Scene.Views[intView].GameLayers.Count; intLayer++)
							Draw(objContext, Map.Scene.Views[intView], Map.Scene.Views[intView].GameLayers[intLayer]);
					// Dibuja la capa de interface de usuario
						Draw(objContext, Map.Scene.Views[intView], Map.Scene.Views[intView].UserInterfaceLayer);
				}
		}

		/// <summary>
		///		Dibuja las entidades de una capa
		/// </summary>
		private void Draw(IGameContext objContext, ViewModel objView, LayerModel objLayer)
		{ for (int intEntity = 0; intEntity < Count; intEntity++)
				if (this[intEntity].IsInitialized &&
						this[intEntity].IsAtView(objView, objLayer))
					this[intEntity].Draw(objContext, objView.ViewPortScreen);
		}

		/// <summary>
		///		Mapa al que pertenecen las entidades
		/// </summary>
		internal MapModel Map { get; }

		/// <summary>
		///		Indica si las entidades de esta colección son "dibujables" o son simplemente entidades
		///	que lanzan otras entidades
		/// </summary>
		private bool IsDrawable { get; }

		/// <summary>
		///		Motor de física
		/// </summary>
		private Physics.PhysicsEngine PhysicsEngine { get; } = new Physics.PhysicsEngine();
	}
}
