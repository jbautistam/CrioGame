using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models.Collections
{
	/// <summary>
	///		Colección de entidades: trata las claves, recicla los elementos...
	/// </summary>
	public class ObjectPool<TypeData> where TypeData : class
	{ // Variables privadas
			private TimeSpan tsLastRecover = TimeSpan.Zero;
			private int intLastActive = 0;

		/// <summary>
		///		Añade una entidad a la colección
		/// </summary>
		public void Add(TypeData objEntity)
		{ // Busca un elemento inactivo donde asociar la entidad
				if (Entities.Count <= intLastActive)
					Entities.Insert(intLastActive, objEntity);
				else
					this[intLastActive] = objEntity;
			// Incrementa el índice de los elementos activos
				intLastActive++;
		}

		/// <summary>
		///		Elimina una entidad
		/// </summary>
		public void RemoveEntity(int intIndex)
		{ if (intLastActive > 0)
				this[intIndex] = this[--intLastActive];
		}

		///// <summary>
		/////		Ordena las entidades
		///// </summary>
		//protected void Sort(Func<TypeData, TypeData, int> fncSort)
		//{ Entities.Sort((objFirst, objSecond) => fncSort(objFirst, objSecond));
		//}

		/// <summary>
		///		Recupera la memoria
		/// </summary>
		protected void RecoverMemory(IGameContext objContext)
		{ if (objContext.MathHelper.IsElapsed(objContext.ActualTime, TimeRecoverMemory, ref tsLastRecover))
				for (int intIndex = Entities.Count - 1; intIndex > intLastActive; intIndex--)
					Entities.RemoveAt(intIndex);
		}

		/// <summary>
		///		Limpia las entidades
		/// </summary>
		public void Clear()
		{ Entities.Clear();
		}

		/// <summary>
		///		Obtiene un elemento de la lista
		/// </summary>
		public TypeData this[int intIndex]
		{ get { return Entities[intIndex]; }
			private set { Entities[intIndex] = value; }
		}

		/// <summary>
		///		Cuenta el número de elementos
		/// </summary>
		public int Count 
		{ get { return intLastActive; } 
		}

		/// <summary>
		///		Entidades
		/// </summary>
		private System.Collections.Generic.List<TypeData> Entities { get; } = new System.Collections.Generic.List<TypeData>();

		/// <summary>
		///		Tiempo que pasa entre recuperaciones de memoria (inicialmente 2 segundos)
		/// </summary>
		public TimeSpan TimeRecoverMemory { get; set; } = TimeSpan.FromMilliseconds(2000);
	}
}
