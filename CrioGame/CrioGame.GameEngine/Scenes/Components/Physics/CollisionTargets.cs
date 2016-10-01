using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics
{
	/// <summary>
	///		Componente para obtener los objetos con los que se ha detectado una colisión
	/// </summary>
	public class CollisionTargets : AbstractComponent
	{
		/// <summary>
		///		Dirección donde se ha localizado la colisión
		/// </summary>
		public enum CollisionFrom
			{ Right,
				Left,
				Up,
				Down
			}

		public CollisionTargets(Entities.Graphics.AbstractActorModel objParentDraw, int intFlagsSource, int intFlagsTarget)
		{ ParentDraw = objParentDraw;
			FlagsSource = intFlagsSource;
			FlagsTarget = intFlagsTarget;
		}

		/// <summary>
		///		Inicializa el comprobador de colisiones
		/// </summary>
		public override void Update(IGameContext objContext, IView objView)
		{	BouncyBox = new Rectangle(ParentDraw.Dimensions.Position.X, ParentDraw.Dimensions.Position.Y, 
																ParentDraw.Dimensions.ScaledDimensions.Width, 
																ParentDraw.Dimensions.ScaledDimensions.Height);
			Targets.Clear();
		}

		/// <summary>
		///		Evalúa la colisión
		/// </summary>
		public virtual bool Evaluate(CollisionTargets objTarget)
		{ return (FlagsTarget & objTarget.FlagsSource) > 0 &&
						 BouncyBox.Right >= objTarget.BouncyBox.Left && 
						 BouncyBox.Left <= objTarget.BouncyBox.Right && 
						 BouncyBox.Top <= objTarget.BouncyBox.Bottom && 
						 BouncyBox.Bottom >= objTarget.BouncyBox.Top;
		}

		/// <summary>
		///		Calcula el ángulo de choque con el destino
		/// </summary>
		public float ComputeAngle(CollisionTargets objTarget)
		{ Vector2D vctTarget = new Vector2D(objTarget.BouncyBox.Left, objTarget.BouncyBox.Top);

				return new Vector2D(ParentDraw.Dimensions.Position.X, ParentDraw.Dimensions.Position.Y).ComputeAngle(vctTarget);
		}

		/// <summary>
		///		Calcula de la dirección con la que se ha colisionado con un objeto
		/// </summary>
		/// <remarks>
		///		0 grados: encima
		///		90 grados: a la derecha
		///		180 grados debajo
		///		270 grados izquierda
		/// </remarks>
		public CollisionFrom ComputeDirection(float fltAngle)
		{ if (fltAngle > 270)
				return CollisionFrom.Left;
			else if (fltAngle > 180)
				return CollisionFrom.Down;
			else if (fltAngle > 90)
				return CollisionFrom.Right;
			else
				return CollisionFrom.Up;
		}

		/// <summary>
		///		Objeto padre
		/// </summary>
		public Entities.Graphics.AbstractActorModel ParentDraw { get; }

		/// <summary>
		///		Grupo al que pertenece
		/// </summary>
		/// <remarks>
		///		 Las colisiones se evalúan entre elementos de diferente grupo por ejemplo "jugadores / laser jugador" sería grupo 1 mientras
		///	que "enemigos / lasers enemigos" sería grupo 2. Así evitamos hacer demasiadas comparaciones
		/// </remarks>
		public int FlagsSource { get; }

		/// <summary>
		///		Grupos con los que puede colisionar
		/// </summary>
		/// <remarks>
		///		 Las colisiones se evalúan entre elementos de diferente grupo por ejemplo "jugadores / laser jugador" sería grupo 1 mientras
		///	que "enemigos / lasers enemigos" sería grupo 2. Así evitamos hacer demasiadas comparaciones
		/// </remarks>
		public int FlagsTarget { get; }		

		/// <summary>
		///		Dimensiones
		/// </summary>
		public Rectangle BouncyBox { get; protected set; }

		/// <summary>
		///		Objetos con los que se ha detectado una colisión
		/// </summary>
		public System.Collections.Generic.List<CollisionTargets> Targets { get; } = new System.Collections.Generic.List<CollisionTargets>();
	}
}
