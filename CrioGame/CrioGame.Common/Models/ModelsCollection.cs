using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models
{
	/// <summary>
	///		Colección de modelos
	/// </summary>
	public class ModelsCollection
	{
		/// <summary>
		///		Añade un componente
		/// </summary>
		public void Add(AbstractModelBase objComponent)
		{ Items.Add(objComponent);
		}

		/// <summary>
		///		Inicializa los componentes
		/// </summary>
		public void Initialize(IGameContext objContext)
		{ for (int intIndex = 0; intIndex < Items.Count; intIndex++)
				Items[intIndex].Initialize(objContext);
		}

		/// <summary>
		///		Modifica los componentes
		/// </summary>
		public void Update(IGameContext objContext)
		{ for (int intIndex = 0; intIndex < Items.Count; intIndex++)
				//if (Items[intIndex].Active)
					Items[intIndex].Update(objContext);
		}

		/// <summary>
		///		Dibuja los elementos
		/// </summary>
		protected virtual void Draw(IGameContext objContext)
		{ for (int intIndex = 0; intIndex < Items.Count; intIndex++)
				//if (Items[intIndex].Active)
					(Items[intIndex] as Graphics.AbstractImageModelBase)?.Draw(objContext);
		}

		/// <summary>
		///		Elementos
		/// </summary>
		private	List<AbstractModelBase> Items { get; } = new List<AbstractModelBase>();
	}
}
