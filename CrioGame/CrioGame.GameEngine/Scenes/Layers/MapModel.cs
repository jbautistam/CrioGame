using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Mapa con las entidades del juego
	/// </summary>
	public class MapModel : IMap
	{ // Constantes privadas
			private TimeSpan tsSpanBase = TimeSpan.FromMilliseconds(10);

		public MapModel(AbstractSceneModel objScene)
		{ Scene = objScene;
			ControlEntities = new MapEntitiesCollection(this, false);
			GameEntities = new MapEntitiesCollection(this, true);
		}

		/// <summary>
		///		Añade una entidad de control
		/// </summary>
		public void AddControlEntity(AbstractModelBase objEntity, TimeSpan? tsBetweenUpdate = null)
		{ ControlEntities.Add(objEntity, tsBetweenUpdate ?? tsSpanBase);
		}

		/// <summary>
		///		Elimina una entidad de control
		/// </summary>
		public void RemoveControlEntity(AbstractModelBase objEntity)
		{ ControlEntities.Remove(objEntity);
		}

		/// <summary>
		///		Añade una entidad al mapa
		/// </summary>
		public void AddGameEntity(IView objView, string strLayerKey, AbstractModelBase objEntity,
															TimeSpan? tsBetweenUpdate = null)
		{ AddGameEntity(new List<ViewLayerModel> { new ViewLayerModel(objView, strLayerKey) },
										objEntity, tsBetweenUpdate);
		}

		/// <summary>
		///		Añade una entidad de juego
		/// </summary>
		public void AddGameEntity(List<ViewLayerModel> objColViewLayer, AbstractModelBase objEntity, 
															TimeSpan? tsBetweenUpdate = null)
		{ // Añade la capa si es necesario
				foreach (ViewLayerModel objViewLayer in objColViewLayer)
					if (!objViewLayer.LayerKey.Equals(AbstractSceneModel.Layer.Background.ToString(), StringComparison.CurrentCultureIgnoreCase) && 
							!objViewLayer.LayerKey.Equals(AbstractSceneModel.Layer.UserInterface.ToString(), StringComparison.CurrentCultureIgnoreCase))
						objViewLayer.View.CreateLayer(objViewLayer.LayerKey, true);
			// Añade la entidad
				GameEntities.Add(objColViewLayer, objEntity, tsBetweenUpdate ?? tsSpanBase);
		}

		/// <summary>
		///		Elimina una entidad de control
		/// </summary>
		public void RemoveGameEntity(AbstractModelBase objEntity)
		{ GameEntities.Remove(objEntity);
		}

		/// <summary>
		///		Inicializa las entidades del mapa
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{ InitializeNewEntities(objContext);
		}

		/// <summary>
		///		Inicializa las entidades que se hayan dado de alta desde el último Update o al principio del juego
		///	(es decir, aquellas que no estén inicializadas)
		/// </summary>
		private void InitializeNewEntities(IGameContext objContext)
		{ ControlEntities.Initialize(objContext);
			GameEntities.Initialize(objContext);
		}

		/// <summary>
		///		Modifica las entidades del mapa
		/// </summary>
		internal void Update(IGameContext objContext)
		{ // Inicializa las entidades que se hayan dado de alta desde el último Update
				InitializeNewEntities(objContext);
			// Modifica las entidades
				ControlEntities.Update(objContext);
				GameEntities.Update(objContext);
		}

		/// <summary>
		///		Dibuja las entidades del mapa
		/// </summary>
		internal void Draw(IGameContext objContext)
		{ GameEntities.Draw(objContext);
		}

		/// <summary>
		///		Escena a la que se asocia el mapa
		/// </summary>
		public AbstractSceneModel Scene { get; }

		/// <summary>
		///		Entidades de control (no dibujables)
		/// </summary>
		internal MapEntitiesCollection ControlEntities { get; }

		/// <summary>
		///		Entidades del juego (dibujables)
		/// </summary>
		internal MapEntitiesCollection GameEntities { get; }
	}
}
