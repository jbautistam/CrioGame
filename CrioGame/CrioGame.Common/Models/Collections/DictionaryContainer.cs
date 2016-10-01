using System;
using System.Collections;
using System.Collections.Generic;

namespace Bau.Libraries.CrioGame.Common.Models.Collections
{
	/// <summary>
	///		Diccionario
	/// </summary>
	public class DictionaryContainer<TypeData> : IEnumerable<TypeData> where TypeData : class
	{
		/// <summary>
		///		Añade un elemento al diccionario
		/// </summary>
		public TypeData Add(string strKey, TypeData objItem)
		{ // Normaliza la calve
				strKey = ComputeKey(strKey);
			// Si existe la clave, la elimina
				if (Exists(strKey))
					Remove(strKey);
			// Añade la clave
				Items.Add(strKey, objItem);
			// Devuelve el elemento añadido
				return objItem;
		}

		/// <summary>
		///		Comprueba si existe un elemento
		/// </summary>
		public bool Exists(string strKey)
		{ return Items.ContainsKey(ComputeKey(strKey));				
		}

		/// <summary>
		///		Busca un elemento en el diccionario
		/// </summary>
		public TypeData Search(string strKey)
		{ TypeData objItem = null;

				// Normaliza la clave
					strKey = ComputeKey(strKey);
				// Obtiene la clave
					if (!Items.TryGetValue(strKey, out objItem))
						return null;
					else
						return objItem;	
		}

		/// <summary>
		///		Elimina un elemento
		/// </summary>
		public void Remove(string strKey)
		{ // Normaliza la clave
				strKey = ComputeKey(strKey);
			// Elimina el elemento
				if (Exists(strKey))
					Items.Remove(strKey);
		}

		/// <summary>
		///		Convierte el diccionario en una lista
		/// </summary>
		public List<KeyValuePair<string, TypeData>> ToList()
		{ List<KeyValuePair<string, TypeData>> objColItems = new List<KeyValuePair<string, TypeData>>();

				// Añade los elementos del diccionario
					foreach (KeyValuePair<string, TypeData> objPair in Items)
						objColItems.Add(objPair);
				// Devuelve la lista
					return objColItems;
		}

		/// <summary>
		///		Calcula la clave
		/// </summary>
		private string ComputeKey(string strKey)
		{ if (string.IsNullOrEmpty(strKey))
				return strKey;
			else
				return strKey.Trim().ToUpper();
		}

		/// <summary>
		///		Obtiene el enumerador
		/// </summary>
		public IEnumerator<TypeData> GetEnumerator()
		{ foreach (string objKey in Items.Keys)
				yield return Items[objKey];
		}

		/// <summary>
		///		Obtiene el enumerador
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{ return this.GetEnumerator();
		}

	 /// <summary>
	 ///	Número de elementos
	 /// </summary>
		public int Count 
		{ get { return Items.Count; }  
		}

		/// <summary>
		///		Diccionario
		/// </summary>
		private Dictionary<string, TypeData> Items { get; } = new Dictionary<string, TypeData>();
	}
}
