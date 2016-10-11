﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.Asteroids.Game.Logic.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : AbstractActorModel
	{ 
		public ExplosionModel(IScene objScene, GameObjectDimensions objDimensions, Polar2D vctVelocity) 
							: base(objScene, objDimensions)
		{ Velocity = vctVelocity;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void InitializeActor(IGameContext objContext)
		{ AddAnimation("Rocks", "Explosion", "Default", 0, 0);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void UpdateActor(IGameContext objContext)
		{ // Modifica las coordenadas y el estado del objeto
				if (Dimensions.Position.X < 0 || ((Sprites[0] as SpriteAnimableModel)?.Loops ?? 0) > 0)
					Scene.Map.RemoveGameEntity(this);
				else
					{ Vector2D vctVelocity = Velocity.ToVector();

							// Controla los movimientos
								Dimensions.Translate(vctVelocity.X, vctVelocity.Y);
					}
		}

		/// <summary>
		///		Velocidad en horizontal
		/// </summary>
		public Polar2D Velocity { get; }
	}
}
