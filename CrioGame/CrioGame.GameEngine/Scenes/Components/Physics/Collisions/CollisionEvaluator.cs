using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Collisions
{
	/// <summary>
	///		Componente para obtener los objetos con los que se ha detectado una colisión
	/// </summary>
	public class CollisionEvaluator : AbstractComponent
	{
		/// <summary>
		///		Modo en que se evalúan las dimensiones del objeto para las colisiones
		/// </summary>
		public enum BouncyMode
			{ Rectangle,
				Circle
			}

		/// <summary>
		///		Dirección donde se ha localizado la colisión
		/// </summary>
		public enum CollisionFrom
			{ Right,
				Left,
				Up,
				Down
			}

		public CollisionEvaluator(Entities.Graphics.AbstractActorModel objActor, int intFlagsSource, int intFlagsTarget,
															BouncyMode intMode)
		{ Actor = objActor;
			FlagsSource = intFlagsSource;
			FlagsTarget = intFlagsTarget;
			switch (intMode)
				{	case BouncyMode.Circle:
							BouncyBox = new BouncyCircle(objActor);
						break;
					default:
							BouncyBox = new BouncyBox(objActor);
						break;
				}
		}

		/// <summary>
		///		Inicializa el comprobador de colisiones
		/// </summary>
		public override void Update(IGameContext objContext, IView objView)
		{	Targets.Clear();
		}

		/// <summary>
		///		Evalúa la colisión
		/// </summary>
		public virtual bool Evaluate(CollisionEvaluator objTarget)
		{ return (FlagsTarget & objTarget.FlagsSource) > 0 && BouncyBox.ExistCollision(objTarget.BouncyBox);
		}

		/// <summary>
		///		Calcula el ángulo de choque con el destino
		/// </summary>
		public float ComputeAngle(CollisionEvaluator objTarget)
		{ return new Vector2D(Actor.Dimensions.Position.X, Actor.Dimensions.Position.Y).ComputeAngle(objTarget.BouncyBox.GetPosition());
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
		///		Actor
		/// </summary>
		public Entities.Graphics.AbstractActorModel Actor { get; }

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
		///		Figura límite con la que calcular las colisiones
		/// </summary>
		public AbstractBouncyCollision BouncyBox { get; protected set; }

		/// <summary>
		///		Objetos con los que se ha detectado una colisión
		/// </summary>
		public System.Collections.Generic.List<CollisionEvaluator> Targets { get; } = new System.Collections.Generic.List<CollisionEvaluator>();
	}
}
