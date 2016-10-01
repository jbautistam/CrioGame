using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Capa de una escena
	/// </summary>
	internal class LayerModel : Common.Models.Collections.ListKeyItem
	{ 
		internal LayerModel(string strKey, bool blnEvaluateCollisions) : base(strKey)
		{ MustEvaluateCollisions = blnEvaluateCollisions;
		}

		/// <summary>
		///		Añade una entidad
		/// </summary>
		internal void Add(AbstractModelBase objEntity)
		{ Entities.Add(objEntity);
		}

		/// <summary>
		///		Inicializa la capa
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{ Entities.Initialize(objContext);
		}

		/// <summary>
		///		Realiza las modificaciones sobre la capa
		/// </summary>
		internal void Update(IGameContext objContext)
		{ Entities.Update(objContext);
		}

		/// <summary>
		///		Dibuja las entidades de la capa
		/// </summary>
		internal void Draw(IGameContext objContext, Rectangle rctView)
		{ // Ordena por ZOrder
				Entities.SortByZOrder();
			// ... y dibuja
				Entities.Draw(objContext, rctView);
		}

		/// <summary>
		///		Indica si se van a evaluar las colisions
		/// </summary>
		internal bool MustEvaluateCollisions { get; }

		/// <summary>
		///		Entidades de la capa
		/// </summary>
		internal LayerEntitiesCollection Entities { get; } = new LayerEntitiesCollection();
	}
}
