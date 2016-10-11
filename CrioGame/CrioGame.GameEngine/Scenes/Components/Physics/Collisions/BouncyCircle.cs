using System;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions
{
	/// <summary>
	///		Cículo para evaluación de colisiones
	/// </summary>
	public class BouncyCircle : AbstractBouncyCollision
	{	// Variables privadas
			private Common.Models.Structs.Circle crcSource = new Common.Models.Structs.Circle(0, 0, 0);

		public BouncyCircle(Entities.Graphics.AbstractActorModel objActor) : base(objActor) {}

		/// <summary>
		///		Comprueba si existe una colisión
		/// </summary>
		public override bool ExistCollision(AbstractBouncyCollision objTarget)
		{ if (objTarget is BouncyBox)
				return base.ExistCollision((objTarget as BouncyBox).Source, Source);
			else if (objTarget is BouncyCircle)
				return base.ExistCollision(Source, (objTarget as BouncyCircle).Source);
			else
				return false;
		}

		/// <summary>
		///		Obtiene la posición
		/// </summary>
		public override Common.Models.Structs.Vector2D GetPosition()
		{ return Source.Center;
		}

		/// <summary>
		///		Círculo origen del actor para la evaluación
		/// </summary>
		public Common.Models.Structs.Circle Source
		{ get
				{ return new Common.Models.Structs.Circle(Actor.Dimensions.Position.X + Actor.Dimensions.Position.Width / 2,
																									Actor.Dimensions.Position.Y + Actor.Dimensions.Position.Height / 2,
																									Actor.Dimensions.Position.Width / 2);
				}
		}
	}
}
