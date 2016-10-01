using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes
{
	/// <summary>
	///		Escena vacía (para evitar los errores cuando aún no se ha definido ninguna escena
	/// </summary>
	internal class NullSceneModel : IScene
	{
		/// <summary>
		///		Implementa la interface
		/// </summary>
		public AbstractModelBase AddEntity(string strLayerKey, AbstractModelBase objEntity)
		{ return null;
		}

		/// <summary>
		///		Implementa la interface
		/// </summary>
		public IView CreateView(string strKey, Rectangle rctPercentScreen, Rectangle rctWorld, Rectangle rctCamera, int intZOrder)
		{ return null;
		}

		/// <summary>
		///		Implementa la interface
		/// </summary>
		public void LoadContent(IGameContext objContext)
		{
		}

		/// <summary>
		///		Implementa la interface
		/// </summary>
		public void Initialize(IGameContext objContext)
		{
		}

		/// <summary>
		///		Implementa la interface
		/// </summary>
		public void Update(IGameContext objContext)
		{
		}

		/// <summary>
		///		Implementa la interface
		/// </summary>
		public void Draw(IGameContext objContext)
		{
		}

		/// <summary>
		///		Vista predeterminada
		/// </summary>
		public IView ViewDefault { get; } = new Layers.ViewModel("Default", new CameraView(new Rectangle(0, 0, 0, 0),
																																											 new Rectangle(0, 0, 0, 0),
																																											 new Rectangle(0, 0, 0, 0)),
																														 0);											
	}
}
