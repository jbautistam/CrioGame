using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Models.Graphics
{
	/// <summary>
	///		Clase base para los elementos dibujables
	/// </summary>
	public abstract class AbstractDrawableModelBase : AbstractModelBase
	{
		public AbstractDrawableModelBase(AbstractModelBase objParent, string strContentKey, TimeSpan tsBetweenUpdate,
																		 int intX, int intY, ColorEngine? clrColor = null, int intZOrder = 0)
									: base(tsBetweenUpdate)
		{ Parent = objParent;
			ContentKey = strContentKey;
			X = intX;
			Y = intY; 
			Color = clrColor ?? ColorEngine.White;
			ZOrder = intZOrder;
		}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public abstract void Draw(IGameContext objContext);

		/// <summary>
		///		Dibuja el elemento en una vista
		/// </summary>
		[Obsolete("Esto habría que hacerlo abstracto")]
		public virtual void Draw(IGameContext objContext, Rectangle rctCamera)
		{
		}

		/// <summary>
		///		Elemento padre
		/// </summary>
		protected AbstractModelBase Parent { get; }

		/// <summary>
		///		Clave del contenido
		/// </summary>
		public string ContentKey { get; }

		/// <summary>
		///		Posición X
		/// </summary>
		public int X { get; set; }

		/// <summary>
		///		Posición Y
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		///		Ancho de dibujo
		/// </summary>
		public int Width { get; set; } = 0;

		/// <summary>
		///		Alto de dibujo
		/// </summary>
		public int Height { get; set; } = 0;

		/// <summary>
		///		Ancho del elemento una vez aplicado el escalado
		/// </summary>
		public int ScaledWidth
		{ get { return (int) (Width * Scale); } 
		}

		/// <summary>
		///		Alto del elemento una vez aplicado el escalado
		/// </summary>
		public int ScaledHeight
		{ get { return (int) (Height * Scale); } 
		}

		/// <summary>
		///		Escala
		/// </summary>
		public float Scale { get; set; } = 1;

		/// <summary>
		///		Angulo (en radianes)
		/// </summary>
		public float Angle { get; set; } = 0;

		/// <summary>
		///		Color
		/// </summary>
		public ColorEngine Color { get; set; }

		/// <summary>
		///		Orden de dibujo
		/// </summary>
		public int ZOrder { get; set; }
	}
}
