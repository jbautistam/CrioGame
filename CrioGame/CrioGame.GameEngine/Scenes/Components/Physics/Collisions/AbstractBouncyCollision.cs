using System;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions
{
	/// <summary>
	///		Clase base para los elementos que pueden comparar colisiones
	/// </summary>
	public abstract class AbstractBouncyCollision
	{
		public AbstractBouncyCollision(Entities.Graphics.AbstractActorModel objActor)
		{ Actor = objActor;
		}

		/// <summary>
		///		Indica si existe una colisión con el objeto destino
		/// </summary>
		public abstract bool ExistCollision(AbstractBouncyCollision objTarget);

		/// <summary>
		///		Comprueba si existe una colisión entre un rectángulo y un círculo
		/// </summary>
		protected bool ExistCollision(Rectangle rctSource, Circle crcTarget)
		{ return rctSource.Right >= crcTarget.Left && 
						 rctSource.Left <= crcTarget.Right && 
						 rctSource.Top <= crcTarget.Bottom && 
						 rctSource.Bottom >= crcTarget.Top;
		}

		/// <summary>
		///		Comprueba si existe una colisión entre dos rectángulos
		/// </summary>
		protected bool ExistCollision(Rectangle rctSource, Rectangle rctTarget)
		{ return rctSource.Right >= rctTarget.Left && 
						 rctSource.Left <= rctTarget.Right && 
						 rctSource.Top <= rctTarget.Bottom && 
						 rctSource.Bottom >= rctTarget.Top;
		}

		/// <summary>
		///		Comprueba si existe una colisión entre dos círculos
		/// </summary>
		protected bool ExistCollision(Circle crcSource, Circle crcTarget)
		{ float fltDistance = crcSource.Center.ComputeSquaredDistance(crcTarget.Center);
				
				// Comprueba si existe la colisión
					return Math.Pow(crcSource.Radius + crcTarget.Radius, 2) > fltDistance;
		}

		/// <summary>
		///		Obtiene la posición
		/// </summary>
		public abstract Vector2D GetPosition();

		/// <summary>
		///		Actor que se está evaluando
		/// </summary>
		public Entities.Graphics.AbstractActorModel Actor { get; }
	}
}
