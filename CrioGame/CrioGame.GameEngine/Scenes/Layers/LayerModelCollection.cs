using System;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Colección de <see cref="LayerModel"/>
	/// </summary>
	internal class LayerModelCollection : Common.Models.Collections.ListKey<LayerModel>
	{
		/// <summary>
		///		Añade una capa a la colección
		/// </summary>
		internal LayerModel Add(string strKey, bool blnEvaluateCollisions)
		{ LayerModel objLayer = Search(strKey);

				// Si no existía la capa, se crea
					if (objLayer == null)
						{ // Crea la capa
								objLayer = new LayerModel(strKey, blnEvaluateCollisions);
							// ... y la añade a la colección
								base.Add(objLayer);
						}
				// Devuelve la capa
					return objLayer;
		}

		internal bool MustEvaluateCollisions(string layerKey)
		{
			throw new NotImplementedException();
		}
	}
}
