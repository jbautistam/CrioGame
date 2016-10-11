using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Particles
{
	/// <summary>
	///		Clase base para los emisores de partículas
	/// </summary>
	public abstract class AbstractParticleEmitter : Graphics.AbstractActorModel
	{
		public AbstractParticleEmitter(IScene objScene, TimeSpan tsLifeTime, GameObjectDimensions objDimensions) 
								: base(objScene, objDimensions)
		{ LifeTime = tsLifeTime;
		}

		/// <summary>
		///		Inicializa el actor
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ StartTime = objContext.ActualTime;
			InitializeEmitter(objContext);
		}

		/// <summary>
		///		Inicializa el emisor
		/// </summary>
		public abstract void InitializeEmitter(IGameContext objContext);

		/// <summary>
		///		Actualiza los datos del actor
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ // Desactiva el actor si ha pasado el tiempo
				if (objContext.ActualTime - StartTime > LifeTime)
					Active = false;
				else
					{ // Crea una nueva partícula si es necesario
							if (Particles.Count < Minimum)
								CreateParticle(objContext);
						// Mueve las partículas
							for (int intIndex = 0; intIndex < Particles.Count; intIndex++)
								if (Particles[intIndex].Active)
									{	// Actualiza la partícula
											Particles[intIndex].Update(objContext);
										// Desactiva la partícula si está fuera de la vista
											if (!Dimensions.HasPoint(Particles[intIndex].Dimensions.Position.X + Dimensions.Position.X,
																							 Particles[intIndex].Dimensions.Position.Y + Dimensions.Position.Y))
												Particles.RemoveEntity(intIndex);
									}
					}
		}

		/// <summary>
		///		Crea una partícula
		/// </summary>
		protected abstract void CreateParticle(IGameContext objContext);

		/// <summary>
		///		Dibuja las partículas
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ for (int intIndex = 0; intIndex < Particles.Count; intIndex++)
				if (Particles[intIndex].Active)
					{ // Cambia las posiciones del sprite
							(Sprites[0] as Graphics.SpriteModel).DeltaX = (int) Particles[intIndex].Dimensions.Position.X;
							(Sprites[0] as Graphics.SpriteModel).DeltaY = (int) Particles[intIndex].Dimensions.Position.Y;
						// Dibuja el sprite
							Sprites[0].Draw(objContext, rctCamera);
					}
		}

		/// <summary>
		///		Colección de partículas
		/// </summary>
		protected ObjectPool<ParticleModel> Particles { get; } = new ObjectPool<ParticleModel>();

		/// <summary>
		///		Número mínimo de partículas
		/// </summary>
		public int Minimum { get; set; } = 10;

		/// <summary>
		///		Máximo número de partículas
		/// </summary>
		public int Maximum { get; set; } = 20;

		/// <summary>
		///		Momento en que se arranca el emisor
		/// </summary>
		private TimeSpan StartTime { get; set; }

		/// <summary>
		///		Tiempo de vida del emisor
		/// </summary>
		private TimeSpan LifeTime { get; }

		/// <summary>
		///		Tiempo de vida mínimo
		/// </summary>
		public int MinimumTimeLife { get; set; } = 30;

		/// <summary>
		///		Tiempo de vida máximo
		/// </summary>
		public int MaximumTimeLife { get; set; } = 60;

		/// <summary>
		///		Indica si el emiosr está activo
		/// </summary>
		public bool Active { get; set; }
	}
}
