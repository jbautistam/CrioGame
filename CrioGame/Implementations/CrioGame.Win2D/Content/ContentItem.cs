using System;

namespace Bau.Libraries.CrioGame.Win2D.Content
{
	/// <summary>
	///		Elemento de contenido
	/// </summary>
	internal class ContentItem
	{
		internal ContentItem(string strContentKey, object objContent)
		{ ContentKey = strContentKey;
			Content = objContent;
		}

		/// <summary>
		///		Clave del contenido
		/// </summary>
		internal string ContentKey { get; }

		/// <summary>
		///		Contenido (imagen, textura, ...)
		/// </summary>
		internal object Content { get; }
	}
}
