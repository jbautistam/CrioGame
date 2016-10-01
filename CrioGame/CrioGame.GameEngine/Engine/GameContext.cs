using System;

using Bau.Libraries.CrioGame.Common.Interfaces;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Contexto del juego
	/// </summary>
	public class GameContext : IGameContext
	{
		internal GameContext(IGameEngineManager objMainEngine)
		{ GameController = objMainEngine;
			LastUpdateTime = TimeSpan.Zero;
			LastDrawTime = TimeSpan.Zero;
		}

		/// <summary>
		///		Actualiza el contexto tras la modificación
		/// </summary>
		public void EndUpdate(TimeSpan tsActual)
		{ LastUpdateTime = ActualTime;
			ActualTime = tsActual;
		}

		/// <summary>
		///		Actualiza el contexto tras el dibujo
		/// </summary>
		public void EndDraw(TimeSpan tsActual)
		{ LastDrawTime = tsActual;
		}

		/// <summary>
		///		Motor principal del juego
		/// </summary>
		public IGameEngineManager GameController { get; }

		/// <summary>
		///		Herramientas matemáticas de ayuda
		/// </summary>
		public Common.Tools.MathHelper MathHelper { get; } = new Common.Tools.MathHelper();

		/// <summary>
		///		Tiempo actual
		/// </summary>
		public TimeSpan ActualTime { get; private set; }

		/// <summary>
		///		Tiempo de última modificación
		/// </summary>
		public TimeSpan LastUpdateTime { get; private set; }

		/// <summary>
		///		Tiempo de último dibujo
		/// </summary>
		public TimeSpan LastDrawTime { get; private set; }
	}
}
