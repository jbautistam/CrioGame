using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Relación entre capa y vista
	/// </summary>
	public struct ViewLayerModel
	{
		public ViewLayerModel(Interfaces.GameEngine.IView objView, string strLayerKey)
		{ View = objView;
			LayerKey = strLayerKey;
		}

		/// <summary>
		///		Vista
		/// </summary>
		public Interfaces.GameEngine.IView View { get; }

		/// <summary>
		///		Clave de la capa
		/// </summary>
		public string LayerKey { get; }
	}
}
