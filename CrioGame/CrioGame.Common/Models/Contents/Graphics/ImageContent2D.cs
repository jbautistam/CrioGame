using System;

namespace Bau.Libraries.CrioGame.Common.Models.Contents.Graphics
{
	/// <summary>
	///		Imagen
	/// </summary>
	public class ImageContent2D : AbstractContentBase
	{
		public ImageContent2D(string strKey, string strContentKey) : base(strKey, strContentKey) { }

		/// <summary>
		///		Inicializa la imagen
		/// </summary>
		public void Initialize(int intWidth, int intHeight)
		{ Size = new Structs.Size2D(intWidth, intHeight);
		}

		/// <summary>
		///		Tamaño
		/// </summary>
		public Structs.Size2D Size { get; private set; } = new Structs.Size2D(0, 0);
	}
}
