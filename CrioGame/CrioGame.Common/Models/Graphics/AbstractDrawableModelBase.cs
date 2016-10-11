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
		public AbstractDrawableModelBase(string strContentKey, GameObjectDimensions objDimensions)
		{ ContentKey = strContentKey;
			Dimensions = objDimensions;
		}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public abstract void Draw(IGameContext objContext, Rectangle rctCamera);

		/// <summary>
		///		Clave del contenido
		/// </summary>
		public string ContentKey { get; protected set; }

		/// <summary>
		///		Dimensiones del objeto
		/// </summary>
		public GameObjectDimensions Dimensions { get; set; }

		/// <summary>
		///		Indica si el elemento está activo
		/// </summary>
		public bool Active { get; set; } = true;
	}
}
