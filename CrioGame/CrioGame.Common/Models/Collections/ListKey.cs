using System;
using System.Collections.Generic;

namespace Bau.Libraries.CrioGame.Common.Models.Collections
{
	/// <summary>
	///		Lista genérica de <see cref="ListKeyItem"/>
	/// </summary>
	public class ListKey<TypeData> : List<TypeData> where TypeData : ListKeyItem
	{
		/// <summary>
		///		Obtiene el elemento correspondiente a una clave
		/// </summary>
		public TypeData Search(string strKey)
		{ // Busca el elemento por su clave
				foreach (TypeData objItem in this)
					if (objItem.Key.Equals(strKey, StringComparison.CurrentCultureIgnoreCase))
						return objItem;
			// Si ha llegado hasta aquí, no existía la capa y la crea
				return null;
		}
	}
}
