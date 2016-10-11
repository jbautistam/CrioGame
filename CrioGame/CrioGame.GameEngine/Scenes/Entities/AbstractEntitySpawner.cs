﻿using System;

using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities
{
	/// <summary>
	///		Entidad abstracta para la creación de entidades
	/// </summary>
	public abstract class AbstractEntitySpawner : AbstractModelBase
	{ // Variables privadas
			private TimeSpan tsLastCreate = TimeSpan.Zero;

		public AbstractEntitySpawner(IScene objScene, int intProbability = 100)
		{ Scene = objScene;
			Probability = intProbability;
		}

		/// <summary>
		///		Modifica la entidad
		/// </summary>
		public override void Update(IGameContext objContext)
		{ if (objContext.MathHelper.Random(100) < Probability)
				{ // Crea una nueva entidad
						Create(objContext);
					// Asigna la hora de última creación
						tsLastCreate = objContext.ActualTime;
				}
		}

		/// <summary>
		///		Crea una entidad
		/// </summary>
		protected abstract void Create(IGameContext objContext);

		/// <summary>
		///		Escena a la que pertenece
		/// </summary>
		protected IScene Scene { get; }

		/// <summary>
		///		Probabilidad de creación
		/// </summary>
		protected int Probability { get; }
	}
}
