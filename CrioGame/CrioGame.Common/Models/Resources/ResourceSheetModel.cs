using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Models.Resources
{
	/// <summary>
	///		Recurso con los rectángulos de una hoja de estilos
	/// </summary>
	public class ResourceSheetModel : Collections.ListKeyItem
	{
		public ResourceSheetModel(string strKey) : base(strKey)
		{
		}

		/// <summary>
		///		Lista de rectángulos
		/// </summary>
		public List<Rectangle> Rectangles { get; } = new List<Rectangle>();

		/// <summary>
		///		Animaciones
		/// </summary>
		public Collections.ListKey<ResourceAnimationModel> Animations { get; } = new Collections.ListKey<ResourceAnimationModel>();
	}
}
