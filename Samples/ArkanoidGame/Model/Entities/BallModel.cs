using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace ArkanoidGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de la pelota
	/// </summary>
	public class BallModel : AbstractActorModel
	{ // Constantes privadas
			private const int cnstMinVelocity = 5;
			private const int cnstMaxVelocity = 8;

		public BallModel(IView objView, GameObjectDimensions objDimensions, Vector2D vctVelocity, TimeSpan tsBetweenUpdate) 
							: base(objView, tsBetweenUpdate, objDimensions)
		{ Velocity = vctVelocity;
			StartPosition = new Vector2D(objDimensions.Position.X, objDimensions.Position.Y);
			StartVelocity = vctVelocity;
			CollisionEvaluator = new CollisionTargets(this, 
																								(int) GroupCollisionObjects.Ball,
																								(int) (GroupCollisionObjects.Player | GroupCollisionObjects.Brick));
		}

		/// <summary>
		///		Inicializa la pelota
		/// </summary>
		internal void Reset()
		{ Dimensions.MoveTo(StartPosition.X, StartPosition.Y);
			Velocity = StartVelocity;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddSprite(objContext.GameController.ContentController.GetContent("Paddle") as SpriteSheetContent,
								"Ball", 0, 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ // Controla las colisiones
				UpdateCollisions(objContext);
			// Controla los bordes
				if (Dimensions.Position.X + Velocity.X <= 0 || 
						Dimensions.Position.X + Velocity.X + Dimensions.ScaledDimensions.Width >= View.ViewPortScreen.Width)
					TreatCollision(objContext, CollisionTargets.CollisionFrom.Left);
				else if (Dimensions.Position.Y + Velocity.Y <= 0)
						TreatCollision(objContext, CollisionTargets.CollisionFrom.Down);
				else if (Dimensions.Position.Y + Velocity.Y + Dimensions.ScaledDimensions.Width >= View.ViewPortScreen.Height)
					{ // Coloca la pelota y la detiene (si no lo hiciera, en cada Update mandaría otro mensaje de eliminar el jugador)
							Dimensions.MoveTo(StartPosition.X, StartPosition.Y);
							Velocity = new Vector2D(0, 0);
						// Manda el mensaje de jugador eliminado
							objContext.GameController.EventsManager.Enqueue(new Messages.InformationMessage(Messages.InformationMessage.InformationType.KillBall, 0, this));
					}
			// Controla los movimientos
				Dimensions.Translate(Velocity);
		}

		/// <summary>
		///		Trata las colisiones
		/// </summary>
		private bool UpdateCollisions(IGameContext objContext)
		{ bool blnTreated = false;

				// Recorre las colisiones
					foreach (CollisionTargets objCollision in CollisionEvaluator.Targets)
						if (!blnTreated)
							{ float fltAngle = CollisionEvaluator.ComputeAngle(objCollision);

									blnTreated = TreatCollision(objContext, CollisionEvaluator.ComputeDirection(fltAngle));
							}
				// Devuelve el valor que indica si se ha tratado la colisión
					return blnTreated;
		}

		/// <summary>
		///		Trata una colisión en una dirección
		/// </summary>
		private bool TreatCollision(IGameContext objContext, CollisionTargets.CollisionFrom intDirection)
		{ bool blnTreated = false;

				// Trata la colisión
					switch (intDirection)
						{ case CollisionTargets.CollisionFrom.Right:
								case CollisionTargets.CollisionFrom.Left:
										Velocity = new Vector2D(-Velocity.X, Velocity.Y + objContext.MathHelper.Random(3));	
										blnTreated = true;
									break;
								case CollisionTargets.CollisionFrom.Down:
								case CollisionTargets.CollisionFrom.Up:
										Velocity = new Vector2D(Velocity.X + objContext.MathHelper.Random(3), -Velocity.Y);
										blnTreated = true;
									break;
						}
				// Normaliza la velocidad
					Velocity = new Vector2D(objContext.MathHelper.Clamp(Velocity.X, -cnstMaxVelocity, cnstMaxVelocity),
																	objContext.MathHelper.Clamp(Velocity.Y, -cnstMaxVelocity, cnstMaxVelocity));
				// Devuelve el valor que indica si se ha tratado la colisión
					return blnTreated;
		}

		/// <summary>
		///		Velocidad
		/// </summary>
		private Vector2D Velocity { get; set; }

		/// <summary>
		///		Posición inicial
		/// </summary>
		private Vector2D StartPosition { get; set; }

		/// <summary>
		///		Velocidad inicial
		/// </summary>
		private Vector2D StartVelocity { get; set; }
	}
}
