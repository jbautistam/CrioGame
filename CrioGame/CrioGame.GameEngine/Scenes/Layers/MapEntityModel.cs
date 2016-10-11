using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Entidad de un mapa
	/// </summary>
	internal class MapEntityModel : AbstractModelBase
	{
		internal MapEntityModel(List<ViewLayerModel> objColViewLayers, AbstractModelBase objEntity, TimeSpan tsBetweenUpdate)
		{ ViewLayers = objColViewLayers;
			Entity = objEntity;
			TimeBetweenUpdate = tsBetweenUpdate;
		}

		/// <summary>
		///		Inicializa la entidad
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Inicializa la entidad
				Entity.Initialize(objContext);
			// Comprueba si está en una capa en la que se deben evaluar colisiones
				IsAtLayerEvaluateCollisions = CheckIsAtLayerEvaluateCollisions();
			// Indica que se ha inicializado
				IsInitialized = true;
		}

		/// <summary>
		///		Comprueba si alguna de las capas en las que se encuentra la entidad, debe comprobar las colisiones
		/// </summary>
		public bool CheckIsAtLayerEvaluateCollisions()
		{ // Recorre las capas
				foreach (ViewLayerModel objViewLayer in ViewLayers)
					{ LayerModel objLayer = (objViewLayer.View as ViewModel).GameLayers.Search(objViewLayer.LayerKey);
					
							if (objLayer != null && objLayer.MustEvaluateCollisions)
								return true;
					}
			// Si ha llegado hasta aquí es porque no ha encontrado ninguna
				return false;
		}

		/// <summary>
		///		Modifica la entidad
		/// </summary>
		public override void Update(IGameContext objContext)
		{ Entity.Update(objContext);
		}

		/// <summary>
		///		Comprueba si una entidad está en una vista / capa
		/// </summary>
		internal bool IsAtView(ViewModel objView, LayerModel objLayer)
		{ // Comprueba si una entidad está en una vista / capa
				for (int intIndex = 0; intIndex < ViewLayers.Count; intIndex++)
					if (ViewLayers[intIndex].View.Equals(objView) &&
							ViewLayers[intIndex].LayerKey.Equals(objLayer.Key))
						return true;
			// Si ha llegado hasta aquí es porque no pertenece a la vista / capa
				return false;
		}

		/// <summary>
		///		Dibuja la entidad
		/// </summary>
		internal void Draw(IGameContext objContext, Rectangle rctCamera)
		{	if (Entity is Entities.Graphics.AbstractActorModel)
				{ Entities.Graphics.AbstractActorModel objGameObject = Entity as Entities.Graphics.AbstractActorModel;

						if (objGameObject != null)
							objGameObject.Draw(objContext, rctCamera);
				}
			else if (Entity is Common.Models.Graphics.AbstractDrawableModelBase)
				{ Common.Models.Graphics.AbstractDrawableModelBase objGameObject = Entity as Common.Models.Graphics.AbstractDrawableModelBase;

						if (objGameObject != null)
							objGameObject.Draw(objContext, rctCamera);
				}
		}

		/// <summary>
		///		Vistas y capas a la que pertenece la entidad
		/// </summary>
		internal List<ViewLayerModel> ViewLayers { get; }

		/// <summary>
		///		Entidad
		/// </summary>
		internal AbstractModelBase Entity { get; }

		/// <summary>
		///		Indica si la entidad se ha inicializado
		/// </summary>
		internal bool IsInitialized { get; set; } = false;

		/// <summary>
		///		Indica si está en alguna capa en la que se deben evaluar colisiones
		/// </summary>
		public bool IsAtLayerEvaluateCollisions { get; private set; }

		/// <summary>
		///		Tiempo entre modificaciones (TimeSpan.Zero: se modifica siempre)
		/// </summary>
		public TimeSpan TimeBetweenUpdate { get; set; }

		/// <summary>
		///		Momento de la última modificación
		/// </summary>
		public TimeSpan TimeLastUpdate { get; set; }
	}
}
