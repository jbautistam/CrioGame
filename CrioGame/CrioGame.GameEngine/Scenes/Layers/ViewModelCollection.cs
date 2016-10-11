using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Colección de <see cref="ViewModel"/>
	/// </summary>
	public class ViewModelCollection : Common.Models.Collections.ListKey<ViewModel>
	{
		/// <summary>
		///		Añade una vista a la colección
		/// </summary>
		public ViewModel Add(string strKey, Rectangle rctPercentScreen, Rectangle rctWorld,
												 Rectangle rctCamera, int intZOrder)
		{ return Add(strKey, new CameraView(rctPercentScreen, rctWorld, rctCamera), intZOrder);
		}

		///		Añade una vista a la colección
		/// </summary>
		public ViewModel Add(string strKey, CameraView objCamera, int intZOrder)
		{ return Add(new ViewModel(strKey, objCamera, intZOrder));
		}

		/// <summary>
		///		Añade 
		/// </summary>
		public new ViewModel Add(ViewModel objViewModel)
		{ // Añade la vista a la colección
				base.Add(objViewModel);
			// Ordena por el ZOrder
				SortByZOrder();
			// Devuelve la vista que se acaba de añadir
				return objViewModel;
		}

		/// <summary>
		///		Ordena por el ZOrder de las vistas
		/// </summary>
		private void SortByZOrder()
		{ Sort((objFirst, objSecond) => objFirst.ZOrder.CompareTo(objSecond.ZOrder));
		}

		/// <summary>
		///		Inicializa los datos de las vistas
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{ for (int intIndex = 0; intIndex < Count; intIndex++)
				this[intIndex].Initialize(objContext);
		}
	}
}
