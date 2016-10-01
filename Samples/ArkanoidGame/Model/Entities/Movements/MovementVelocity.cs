using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace ArkanoidGame.Model.Entities.Movements
{
	/// <summary>
	///		Movimiento sólo con velocidad (se termina cuando se sale de la pantalla)
	/// </summary>
	public class MovementVelocity : Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements.MovementSimple
	{
		public MovementVelocity(AbstractActorModel objParent, Vector2D vctVelocity) : base(objParent, vctVelocity)
		{
		}

		/// <summary>
		///		Indica si el elemento está inactivo
		/// </summary>
		public override bool IsDisabled(IGameContext objContext, IView objView, float fltNextX, float fltNextY)
		{ return !objContext.MathHelper.IsAtScreen(fltNextX, fltNextY, objView);
		}
	}
}
