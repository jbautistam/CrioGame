using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Models.Graphics
{
	/// <summary>
	///		Clase base para los elementos dibujables
	/// </summary>
	public abstract class AbstractImageModelBase : AbstractDrawableModelBase
	{
		public AbstractImageModelBase(AbstractModelBase objParent, string strContentKey, TimeSpan tsBetweenUpdate,
																	int intX, int intY, Rectangle rctSource, ColorEngine? clrTile = null,
																	int intZOrder = 0)
									: base(objParent, strContentKey, tsBetweenUpdate, intX, intY, clrTile, intZOrder)
		{ RectangleSource = rctSource;
		}

		public AbstractImageModelBase(AbstractModelBase objParent, string strContentKey, TimeSpan tsBetweenUpdate,
																	int intX, int intY, ColorEngine? clrTile = null,
																	int intZOrder = 0)
									: base(objParent, strContentKey, tsBetweenUpdate, intX, intY, clrTile, intZOrder)
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
	}
}
