using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models.Collections
{
	/// <summary>
	///		Colección de entidades: trata las claves, recicla los elementos...
	/// </summary>
	public class ObjectPool<TypeData> where TypeData : AbstractModelBase
	{ // Variables privadas
			private TimeSpan tsLastRecover = TimeSpan.Zero;

		/// <summary>
		///		Añade una entidad a la colección
		/// </summary>
		public void Add(TypeData objEntity)
		{ bool blnAdded = false;

				// Busca un elemento inactivo donde asociar la entidad
					for (int intIndex = 0; intIndex < Entities.Count && !blnAdded; intIndex++)
						if (!Entities[intIndex].Active)
							{ // Cambia la entidad
									Entities[intIndex] = objEntity;
								// Indica que se ha añadido
									blnAdded = true;
							}
				// Añade la entidad
					if (!blnAdded)
						Entities.Add(objEntity);
		}

		/// <summary>
		///		Ordena las entidades
		/// </summary>
		protected void Sort(Func<TypeData, TypeData, int> fncSort)
		{ Entities.Sort((objFirst, objSecond) => fncSort(objFirst, objSecond));
		}

		/// <summary>
		///		Recupera la memoria
		/// </summary>
		protected void RecoverMemory(IGameContext objContext)
		{ if (objContext.MathHelper.IsElapsed(objContext.ActualTime, TimeRecoverMemory, ref tsLastRecover))
				for (int intIndex = Entities.Count - 1; intIndex >= 0; intIndex--)
					if (!Entities[intIndex].Active)
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
		}

		/// <summary>
		///		Cuenta el número de elementos activos
		/// </summary>
		public int CountEnabled
		{ get 
				{ int intCount = 0;

						// Obtiene el número de elementos activos
							for (int intIndex = 0; intIndex < Entities.Count; intIndex++)
								if (Entities[intIndex].Active)
									intCount++;
						// Devuelve el número de elementos
							return intCount;
				}
		}

		/// <summary>
		///		Cuenta el número de elementos
		/// </summary>
		public int Count 
		{ get { return Entities.Count; } 
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
