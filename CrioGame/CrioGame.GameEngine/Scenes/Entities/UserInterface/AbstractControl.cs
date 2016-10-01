using System;

using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface
{
	/// <summary>
	///		Clase base para los controles
	/// </summary>
	public abstract class AbstractControl : AbstractDrawableModelBase
	{
		public AbstractControl(Rectangle rctPosition, TimeSpan tsBetweenUpdate, int intZOrder = 0) 
								: base(null, null, tsBetweenUpdate, (int) rctPosition.X, (int) rctPosition.Y, null, intZOrder)
		{ Position = rctPosition;
		}

		/// <summary>
		///		Posición del control
		/// </summary>
		public Rectangle Position { get; set; }

		/// <summary>
		///		Fondo
		/// </summary>
		public Graphics.SpriteModel Background { get; set; }
	}
}
