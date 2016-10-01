using System;

namespace Bau.Libraries.CrioGame.Common.Models.Contents
{
	/// <summary>
	///		Base para los elementos de contenido
	/// </summary>
	public abstract class AbstractContentBase
	{
		public AbstractContentBase(string strKey, string strContentKey)
		{ Key = strKey;
			ContentKey = strContentKey;
		}

		/// <summary>
		///		Clave de contenido
		/// </summary>
		public string Key { get; }

		/// <summary>
		///		Contenido específico del motor (por ejemplo, en MonoGame la ruta a una imagen o un sonido...)
		/// </summary>
		public string ContentKey { get; }
	}
}
