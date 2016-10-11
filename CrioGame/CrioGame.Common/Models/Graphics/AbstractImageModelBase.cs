using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Models.Graphics
{
	/// <summary>
	///		Clase base para los elementos dibujables
	/// </summary>
	public abstract class AbstractImageModelBase : AbstractDrawableModelBase
	{
		public AbstractImageModelBase(AbstractModelBase objParent, string strContentKey, 
																	GameObjectDimensions objDimensions, Rectangle rctSource)
									: base(objParent, strContentKey, objDimensions)
		{ RectangleSource = rctSource;
		}

		public AbstractImageModelBase(AbstractModelBase objParent, string strContentKey, 
																	int intX, int intY, ColorEngine? clrTile = null,
																	int intZOrder = 0)
									: base(objParent, strContentKey, new GameObjectDimensions(intX, intY, 0, 0, 1, 0, clrTile, intZOrder))
		{
		}

		/// <summary>
		///		Rectángulo origen del gráfico que se tiene que dibujar
		/// </summary>
		public Rectangle RectangleSource { get; set; }

		/// <summary>
		///		Rectángulos para dibujar al mismo tiempo a partir de una textura (por ejemplo, en los parallax)
		/// </summary>
		public Rectangle[] RectangleDraws { get; set; }

		/// <summary>
		///		Indica si se debe dibujar a toda pantalla
		/// </summary>
		public bool FullScreen { get; set; }
	}
}
