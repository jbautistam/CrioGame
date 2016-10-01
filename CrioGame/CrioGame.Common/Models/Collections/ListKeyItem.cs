using System;

namespace Bau.Libraries.CrioGame.Common.Models.Collections
{
	/// <summary>
	///		Elemento de una <see cref="ListKey{TypeData}"/>
	/// </summary>
	public class ListKeyItem
	{
		public ListKeyItem(string strKey)
		{ Key = strKey;
		}

		/// <summary>
		///		Clave del elemento
		/// </summary>
		public string Key { get; }
	}
}
