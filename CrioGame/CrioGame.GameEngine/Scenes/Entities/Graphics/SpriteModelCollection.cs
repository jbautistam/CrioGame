using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Colección de <see cref="SpriteModel"/>
	/// </summary>
	public class SpriteModelCollection : System.Collections.Generic.List<AbstractDrawableModelBase>
	{
		/// <summary>
		///		Dibuja el sprite con la información de la vista
		/// </summary>
		public void Draw(IGameContext objContext, Rectangle rctView)
		{	for (int intIndex = 0; intIndex < Count; intIndex++)
				if (this[intIndex].Active)
					this[intIndex].Draw(objContext, rctView);
		}

		/// <summary>
		///		Activa o desactiva una animación
		/// </summary>
		public void EnableAnimation(string strAnimationKey, bool blnEnabled)
		{ for (int intIndex = 0; intIndex < Count; intIndex++)
				if (((this[intIndex] as SpriteAnimableModel)?.AnimationKey ?? "").Equals(strAnimationKey, StringComparison.CurrentCultureIgnoreCase))
					this[intIndex].Active = blnEnabled;
		}
	}
}
