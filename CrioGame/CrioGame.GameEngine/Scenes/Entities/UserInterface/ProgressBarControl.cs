using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface
{
	/// <summary>
	///		Barra de progreso
	/// </summary>
	public class ProgressBarControl : AbstractControl
	{
		public ProgressBarControl(Rectangle rctPosition,  TimeSpan tsBetweenUpdate, int intZOrder = 0) 
							: base(rctPosition, tsBetweenUpdate, intZOrder)
		{
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{
		}

		/// <summary>
		///		Modifica el control
		/// </summary>
		public override void Update(IGameContext objContext)
		{
		}

		/// <summary>
		///		Dibuja el control
		/// </summary>
		public override void Draw(IGameContext objContext)
		{ Draw(objContext, Position);
		}

		/// <summary>
		///		Dibuja el control en una posición
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ float fltBarWidth;

				// Calcula el porcentaje de la barra
					Percent = (int) objContext.MathHelper.Clamp(Percent, 0, 100);
					fltBarWidth = Position.Width * Percent / 100;
				// Dibuja las barras
					DrawContent(objContext, Background, 0, 0, Position.Width, rctCamera);
					DrawContent(objContext, Bar, Bar.DeltaX, Bar.DeltaY, fltBarWidth, rctCamera);
		}

		/// <summary>
		///		Dibuja una imagen en una posición
		/// </summary>
		private void DrawContent(IGameContext objContext, Graphics.SpriteModel objSprite, 
														 int intDeltaX, int intDeltaY, float fltWidth, Rectangle rctCamera)
		{ if (objSprite != null)
				{ // Coloca el objeto
						objSprite.X = (int) Position.X + intDeltaX;
						objSprite.Y = (int) Position.Y + intDeltaY;
						objSprite.Width = (int) fltWidth - 2 * intDeltaX;
						objSprite.Height = (int) Position.Height - 2 * intDeltaY;
					// Dibuja el objeto
						objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.DrawFull(objSprite, rctCamera);
				}
		}

		/// <summary>
		///		Porcentaje de la barra
		/// </summary>
		public int Percent { get; set; } = 100;

		/// <summary>
		///		Barra de progreso
		/// </summary>
		public Graphics.SpriteModel Bar { get; set; }
	}
}
