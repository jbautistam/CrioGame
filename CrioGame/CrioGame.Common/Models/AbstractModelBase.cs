using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models
{
	/// <summary>
	///		Base para los modelos de entidades que puede tratar el motor
	/// </summary>
	public abstract class AbstractModelBase 
	{
		public AbstractModelBase(TimeSpan tsBetweenUpdate) 
		{ TimeBetweenUpdate = tsBetweenUpdate;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public abstract void Initialize(IGameContext objContext);

		/// <summary>
		///		Modifica el objeto
		/// </summary>
		public abstract void Update(IGameContext objContext);

		///// <summary>
		/////		Indica si el elemento está activo
		///// </summary>
		//public bool Active { get; set; } = true;

		/// <summary>
		///		Tiempo entre modificaciones (TimeSpan.Zero: se modifica siempre)
		/// </summary>
		public TimeSpan TimeBetweenUpdate { get; protected set; }

		/// <summary>
		///		Momento de la última modificación
		/// </summary>
		public TimeSpan TimeLastUpdate { get; set; }
	}
}
