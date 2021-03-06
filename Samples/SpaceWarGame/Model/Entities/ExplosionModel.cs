﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace SpaceWarGame.Model.Entities
{
	/// <summary>
	///		Modelo con los datos de una explosión
	/// </summary>
	public class ExplosionModel : SpriteAnimableModel
	{ // Tipo de explosion
			public enum ExplosionType
				{ Ship,
					BossGround
				}
		// Variables privadas
			private int intUpdates = 0;

		private ExplosionModel(IView objView, ExplosionType intType, float fltX, float fltY, Vector2D vctVelocity, 
													 string strSheetContentKey, string strFramesKey, string strAnimationKey, string strContentKey, 
													 TimeSpan tsBetweenUpdate) 
							: base(null, strSheetContentKey, strFramesKey, strAnimationKey, strContentKey, tsBetweenUpdate, (int) fltX, (int) fltY)
		{ View = objView;
			Velocity = vctVelocity;
			Type = intType;
		}

		/// <summary>
		///		Crea una explosión
		/// </summary>
		public static ExplosionModel Create(IView objView, ExplosionType intType, float fltX, float fltY, Vector2D vctVelocity, 
																				TimeSpan tsBetweenUpdate)
		{ string strFramesKey = "ExplosionShip";
		
				// Obtiene la clave de animación
					switch (intType)
						{	case ExplosionType.BossGround:
									strFramesKey = "ExplosionGround";
								break;
						}
				// Crea la explosión
					return new	ExplosionModel(objView, intType, fltX, fltY, vctVelocity, "SpaceWar", strFramesKey, "Default", "SpaceWarImage", tsBetweenUpdate);
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Inicializa el objeto
				base.Initialize(objContext);
		}

		/// <summary>
		///		Modifica el estado del objeto
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // Modifica las coordenadas y el estado del objeto
				if (X < 0 || intUpdates++ >= Frames)
					Active = false;
				else
					{ X += (int) Velocity.X;
						Y += (int) Velocity.Y;
						X = (int) objContext.MathHelper.ClampScreenWidth(X, ScaledWidth, View);
						Y = (int) objContext.MathHelper.ClampScreenHeight(Y, ScaledHeight, View);
					}
			// Llama al método base
				base.Update(objContext);
		}

		/// <summary>
		///		Vista
		/// </summary>
		public IView View { get; }

		/// <summary>
		///		Velocidad en horizontal
		/// </summary>
		public Vector2D Velocity { get; }

		/// <summary>
		///		Tipo de explosión
		/// </summary>
		private ExplosionType Type { get; }
	}
}
