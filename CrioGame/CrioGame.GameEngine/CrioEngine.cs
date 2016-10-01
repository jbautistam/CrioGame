using System;

using Bau.Libraries.CrioGame.Common.Interfaces;

namespace Bau.Libraries.CrioGame.GameEngine
{
	/// <summary>
	///		Motor para creación de juegos
	/// </summary>
	public class CrioEngine : Common.ICrioController, IDisposable
	{ 
		public CrioEngine(IGraphicsEngineManager objGraphicsEngine)
		{	GraphicsEngine = objGraphicsEngine;
			GameEngine = new Engine.GameController(this);
		}

		/// <summary>
		///		Inicializa el motor
		/// </summary>
		public void Initialize(IGameParameters objGameParameters)
		{ GameParameters = objGameParameters;
		}

		/// <summary>
		///		Arranca el motor
		/// </summary>
		public void Start(int intWindowsWidth = 0, int intWindowsHeight = 0)
		{ GameEngine.GameLoopController.Start(intWindowsWidth, intWindowsHeight);
		}

		/// <summary>
		///		Detiene el juego
		/// </summary>
		public void Stop()
		{ GameEngine.GameLoopController.Stop();
		}

		/// <summary>
		///		Libera la memoria (patrón Dispose)
		/// </summary>
		protected virtual void Dispose(bool blnDisposing)
		{ if (!IsDisposed)
				{	// Si se ha indicado que se debe liberar la memoria
						if (blnDisposing)
							{	// Libera la memoria del juego
									//MainGame.Dispose();
									//MainGame = null;
							}
					//TODO: libera los recursos no administrados
					//TODO: asignar el valor null a campos grandes
					// Indica que se ha liberado la memoria
						IsDisposed = true;
				}
		}

		// This code added to correctly implement the disposable pattern.
		/// <summary>
		///		Implementa el patrón Disposable
		/// </summary>
		public void Dispose()
		{	// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
				Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~CoconousEngine() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		/// <summary>
		///		Indica si se ha liberado la memoria
		/// </summary>
		public bool IsDisposed { get; private set; }

		/// <summary>
		///		Parámetros del juego
		/// </summary>
		public IGameParameters GameParameters { get; private set; }

		/// <summary>
		///		Motor de juego
		/// </summary>
		public IGameEngineManager GameEngine { get; private set; }

		/// <summary>
		///		Motor de gráficos
		/// </summary>
		public IGraphicsEngineManager GraphicsEngine { get; private set; }
	}
}
