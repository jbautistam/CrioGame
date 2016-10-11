using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models
{
	/// <summary>
	///		Base para los modelos de entidades que puede tratar el motor
	/// </summary>
	public abstract class AbstractModelBase 
	{
		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public abstract void Initialize(IGameContext objContext);

		/// <summary>
		///		Modifica el objeto
		/// </summary>
		public abstract void Update(IGameContext objContext);
	}
}
