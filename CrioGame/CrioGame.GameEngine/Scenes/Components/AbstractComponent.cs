using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components
{
	/// <summary>
	///		Clase abstracta para los componentes
	/// </summary>
	public abstract class AbstractComponent
	{
		/// <summary>
		///		Actualiza el componente
		/// </summary>
		public abstract void Update(IGameContext objContext, IView objView);
	}
}
