using System;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions
{
	/// <summary>
	///		Rectángulo para evaluación de colisiones
	/// </summary>
	public class BouncyBox : AbstractBouncyCollision
	{	
		public BouncyBox(Entities.Graphics.AbstractActorModel objActor) : base(objActor) {}

		/// <summary>
		///		Comprueba si existe una colisión con otro elemento
		/// </summary>
		public override bool ExistCollision(AbstractBouncyCollision objTarget)
		{ if (objTarget is BouncyCircle)
				return base.ExistCollision(Source, (objTarget as BouncyCircle).Source);
			else if (objTarget is BouncyBox)
				return base.ExistCollision(Source, (objTarget as BouncyBox).Source);
			else
				return false;
		}

		/// <summary>
		///		Obtiene la posición
		/// </summary>
		public override Common.Models.Structs.Vector2D GetPosition()
		{ return new Common.Models.Structs.Vector2D(Source.X, Source.Y);
		}

		/// <summary>
		///		Rectángulo origen del actor para la evaluación
		/// </summary>
		public Common.Models.Structs.Rectangle Source
		{ get
				{ return Actor.Dimensions.Position;
				}
		}
	}
}
