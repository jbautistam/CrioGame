using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Fondo
	/// </summary>
	public class BackgroundEntity : SpriteModel
	{
		public BackgroundEntity(IView objView, string strKey, int intZOrder = 0, int intX = 0, int intY = 0) 
								: base(null, strKey, TimeSpan.FromDays(1), intX, intY)
		{ View = objView;
		}

		/// <summary>
		///		Inicializa el fondo
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{	Width = (int) View.ViewPortScreen.Width;
			Height = (int) View.ViewPortScreen.Height;
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
		public override void Draw(IGameContext objContext)
		{ objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this);
		}

		/// <summary>
		///		Vista a la que se asocia la entidad
		/// </summary>
		public IView View { get; }
	}
}
