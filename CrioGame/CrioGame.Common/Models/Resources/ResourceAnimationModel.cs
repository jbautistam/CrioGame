using System;
using System.Collections.Generic;

namespace Bau.Libraries.CrioGame.Common.Models.Resources
{
	/// <summary>
	///		Recurso con la animación de una hoja de estilos
	/// </summary>
	public class ResourceAnimationModel : Collections.ListKeyItem
	{
		public ResourceAnimationModel(string strKey) : base(strKey)
		{
		}

		/// <summary>
		///		Tiempo de las animaciones
		/// </summary>
		public int FrameTime { get; set; }

		/// <summary>
		///		Lista de animaciones
		/// </summary>
		public List<int> Frames { get; } = new List<int>();
	}
}
