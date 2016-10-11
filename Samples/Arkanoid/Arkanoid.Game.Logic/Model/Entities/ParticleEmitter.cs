using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Components.Physics.Movements;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Particles;

namespace Bau.Libraries.ArkanoidGame.Logic.Model.Entities
{
	/// <summary>
	///		Emisor de partículas
	/// </summary>
	internal class ParticleEmitter : AbstractParticleEmitter
	{
		public ParticleEmitter(IScene objScene, TimeSpan tsLife, GameObjectDimensions objDimensions) 
								: base(objScene, tsLife, objDimensions)
		{
		}

		/// <summary>
		///		Inicializa el emisor
		/// </summary>
		public override void InitializeEmitter(IGameContext objContext)
		{ // Inicializa el tiempo de vida, el número máximo de elementos...
				Minimum = 10;
				Maximum = 10;
				MinimumTimeLife = 20;
				MaximumTimeLife = 3000;
			// Inicializa los sprites
				AddSprite(objContext.GameController.ContentController.GetContent("Paddle") as SpriteSheetContent,
									"Particles", 0, 0, 0);
		}

		/// <summary>
		///		Crea una partícula
		/// </summary>
		protected override void CreateParticle(IGameContext objContext)
		{	Particles.Add(new ParticleModel(objContext.MathHelper.Random(MinimumTimeLife, MaximumTimeLife),
																			new MovementVelocityComponent(new Vector2D(objContext.MathHelper.Random(-5, 5), 
																																								 objContext.MathHelper.Random(-5, 5)))));
		}
	}
}
