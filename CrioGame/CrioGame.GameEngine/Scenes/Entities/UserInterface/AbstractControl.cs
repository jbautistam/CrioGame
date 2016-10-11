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
		public AbstractControl(GameObjectDimensions objDimensions) : base(null, objDimensions)
		{ Dimensions = objDimensions;
		}

		/// <summary>
		///		Fondo
		/// </summary>
		public Graphics.SpriteModel Background { get; set; }
	}
}
