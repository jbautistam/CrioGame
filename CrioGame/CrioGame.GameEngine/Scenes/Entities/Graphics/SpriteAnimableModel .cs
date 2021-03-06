﻿using System;

using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Animación
	/// </summary>
	public class SpriteAnimableModel : SpriteModel
	{	
		public SpriteAnimableModel(AbstractModelBase objParent, 
															 string strSheetContentKey, string strFramesKey, string strAnimationKey, string strContentKey, TimeSpan tsBetweenUpdate,
															 int intX, int intY, 
															 ColorEngine? clrTile = null, int intZOrder = 0) 
								: base(objParent, strContentKey, tsBetweenUpdate, intX, intY, null, clrTile, intZOrder)
		{ SheetContentKey = strSheetContentKey;
			FramesKey = strFramesKey;
			AnimationKey = strAnimationKey;
		}

		/// <summary>
		///		Inicializa la animación
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ SetAnimation(objContext, SheetContentKey, FramesKey, AnimationKey, ContentKey);
		}

		/// <summary>
		///		Cambia la animación
		/// </summary>
		public void SetAnimation(IGameContext objContext, string strSheetContentKey, string strFramesKey, string strAnimationKey, string strContentKey)
		{ // Cambia la animación
				SpriteSheet = objContext.GameController.ContentController.GetContent(strSheetContentKey) as SpriteSheetContent;
			// Asigna la animación
				SetAnimation(strFramesKey, strAnimationKey);
		}

		/// <summary>
		///		Cambia la animación actual
		/// </summary>
		public void SetAnimation(string strFramesKey, string strAnimationKey)
		{	// Inicializa los datos de la animación
				ActualFrames = SpriteSheet.SearchFrames(strFramesKey);
				ActualAnimation = ActualFrames.SearchAnimation(strAnimationKey);
				ActualFrameIndex = 0;
				LastUpdate = TimeSpan.Zero;
			// Cambia las dimensiones a dibujar
				UpdateFrame();
		}

		/// <summary>
		///		Modifica la animación
		/// </summary>
		public override void Update(IGameContext objContext)
		{ if (LastUpdate.TotalMilliseconds == 0 || (objContext.ActualTime - LastUpdate).TotalMilliseconds > ActualAnimation.FrameTime)
				{ // Modifica el frame actual
						ActualFrameIndex = (ActualFrameIndex + 1) % ActualAnimation.Frames.Length;
					// Asigna el rectángulo del frame al rectángulo a dibujar
						UpdateFrame();
					// Cambia la fecha de última modificación
						LastUpdate = objContext.ActualTime;
				}
		}

		/// <summary>
		///		Modifica los datos del frame actual
		/// </summary>
		private void UpdateFrame()
		{ // Cambia el rectángulo a dibujar
				RectangleSource = ActualFrames.Rectangles[ActualAnimation.Frames[ActualFrameIndex]];
			// Asigna las dimensiones al sprite (por supesto, después de asignar la animación)
				Width = (int) RectangleSource.Width;
				Height = (int) RectangleSource.Height;
		}

		/// <summary>
		///		Clave del <see cref="SpriteSheetContent"/>
		/// </summary>
		protected string SheetContentKey { get; }

		/// <summary>
		///		Clave del <see cref="SpriteSheetFrames"/>
		/// </summary>
		protected string FramesKey { get; }

		/// <summary>
		///		Clave de <see cref="SpriteSheetFrames"/>
		/// </summary>
		public string AnimationKey { get; }

		/// <summary>
		///		Animación
		/// </summary>
		private SpriteSheetContent SpriteSheet { get; set; }

		/// <summary>
		///		Rectángulos del spriteSheet
		/// </summary>
		private SpriteSheetFrames ActualFrames { get; set; }

		/// <summary>
		///		Frames de animación actual
		/// </summary>
		private SpriteSheetAnimation ActualAnimation { get; set; }

		/// <summary>
		///		Frame actual
		/// </summary>
		internal int ActualFrameIndex { get; set; }

		/// <summary>
		///		Número de frames
		/// </summary>
		public int Frames 
		{ get { return ActualAnimation.Frames.Length; }
		}

		/// <summary>
		///		Ultima modificación de la animación
		/// </summary>
		private TimeSpan LastUpdate { get; set; }
	}
}