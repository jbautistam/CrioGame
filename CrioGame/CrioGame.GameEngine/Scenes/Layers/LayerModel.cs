using System;

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
		///		Indica si se van a evaluar las colisions
		/// </summary>
		internal bool MustEvaluateCollisions { get; }
	}
}
