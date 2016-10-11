using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Fondo
	/// </summary>
	public class BackgroundEntity : SpriteModel
	{
		public BackgroundEntity(IView objView, string strKey, int intZOrder = 0, int intX = 0, int intY = 0) 
								: base(null, strKey, new GameObjectDimensions(intX, intY, 0, 0, 1, 0, null, intZOrder))
		{ View = objView;
		}

		/// <summary>
		///		Inicializa el fondo
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{	Dimensions.Resize(View.ViewPortScreen.Width, View.ViewPortScreen.Height);
			FullScreen = true;
		}

		/// <summary>
		///		Modifica el fondo --> en este caso no hace nada
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // ... no hace nada: es un fondo
		}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctView)
		{ objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this, rctView);
		}

		/// <summary>
		///		Vista
		/// </summary>
		private IView View { get; }
	}
}
