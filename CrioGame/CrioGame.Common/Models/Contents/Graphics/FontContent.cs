using System;

namespace Bau.Libraries.CrioGame.Common.Models.Contents.Graphics
{
	/// <summary>
	///		Fuente almacenada
	/// </summary>
	public class FontContent : AbstractContentBase
	{
		public FontContent(string strKey, string strContentKey) : base(strKey, strContentKey) {}

		/// <summary>
		///		Familia de la fuente
		/// </summary>
		public string Family { get; set; }

		/// <summary>
		///		Tamaño de la fuente
		/// </summary>
		public int Size { get; set; }
	}
}
