using System;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Win2D.Graphics
{
	/// <summary>
	///		Controlador de dibujo por lotes
	/// </summary>
	public class SpriteBatchController : ISpriteBatch
	{	// Variables privadas
			private CanvasSpriteBatch objSpriteBatch = null;
			private CanvasDrawingSession objDrawingSession = null;
			private Rectangle rctScreen;

		internal SpriteBatchController(GameInternal.MainGame objMainGame)
		{ MainGame = objMainGame;
		}

		/// <summary>
		///		Almacena la sesión de dibujo
		/// </summary>
		internal void InitDrawingSession(CanvasDrawingSession objDrawingSession)
		{ this.objDrawingSession = objDrawingSession;
		}

		/// <summary>
		///		Comienza el dibujo por lotes
		/// </summary>
		public void Begin()
		{ // Crea el spriteBath
				objSpriteBatch = objDrawingSession.CreateSpriteBatch();
			// Inicializa el rectángulo que define la pantalla
				rctScreen = new Rectangle(0, 0, MainGame.ScreenController.ViewPort.Width,
																	MainGame.ScreenController.ViewPort.Height);
		}

		/// <summary>
		///		Obtiene las dimensiones de una imagen
		/// </summary>
		public Size2D GetDimensions(AbstractImageModelBase objImage)
		{ CanvasBitmap objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				if (objTexture != null)
					return new Size2D(objTexture.SizeInPixels.Width, objTexture.SizeInPixels.Height);
				else
					return new Size2D(0, 0);
		}

		/// <summary>
		///		Dibuja una imagen
		/// </summary>
		public void Draw(AbstractImageModelBase objImage)
		{ Draw(objImage, rctScreen);
		}

		/// <summary>
		///		Dibuja una imagen escalando en un rectángulo (el de la cámara)
		/// </summary>
		public void Draw(AbstractImageModelBase objImage, Rectangle rctCamera)
		{ CanvasBitmap objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				// Dibuja la imagen:
				//   -- Si no se ha definido ni un rectángulo origen ni un destino, dibuja directamente en una posición
				//   -- Si se ha definido un rectángulo origen, se dibuja parte de él en una posición
				//   -- Si se ha definido un rectángulo destino, se dibuja escalado
				//   -- Si se han definido los dos, se dibuja parte de él escalado
					if (objImage.RectangleDraws != null && objImage.RectangleDraws.Length > 0)
						DrawParts(objTexture, objImage, rctCamera);
					else if (objImage.RectangleSource.IsEmpty && !objImage.FullScreen)
						DrawAtPosition(objTexture, objImage, rctCamera);
					else if (!objImage.RectangleSource.IsEmpty) // ... tenemos una posición y parte de una imagen
						DrawCrop(objTexture, objImage, rctCamera);
					else if (objImage.FullScreen) // ... dibuja a toda pantalla
						DrawFullScreen(objTexture, objImage, rctCamera);
					else if (objImage.Width != 0 && objImage.Height != 0) // ... sólo está definido rectangle target
						DrawScaled(objTexture, objImage, rctCamera);
					else // ... tenemos dimensiones de la imagen y una parte de la imagen
						DrawFull(objTexture, objImage, rctCamera);
		}

		/// <summary>
		///		Dibuja una imagen escalada a partir de parte de una imagen
		/// </summary>
		public void DrawFull(AbstractImageModelBase objImage, Rectangle rctCamera)
		{ CanvasBitmap objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				// Dibuja la imagen
					objSpriteBatch.DrawFromSpriteSheet(objTexture, Convert(objImage.X + rctCamera.X, 
																																 objImage.Y + rctCamera.Y, 
																																 objImage.Width, objImage.Height),
																						 Convert(objImage.RectangleSource), Convert(objImage.Color));
		}

		/// <summary>
		///		Dibuja una serie de partes de una textura
		/// </summary>
		private void DrawParts(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{	for (int intIndex = 0; intIndex < objImage.RectangleDraws.Length; intIndex++)
				{ Rectangle rctDraw = new	Rectangle(objImage.RectangleDraws[intIndex].X + rctCamera.X,
																						objImage.RectangleDraws[intIndex].Y + rctCamera.Y,
																						objImage.RectangleDraws[intIndex].Width,
																						objImage.RectangleDraws[intIndex].Height);
				 
						// Dibuja
							objSpriteBatch.Draw(objTexture, Convert(rctDraw), Convert(ColorEngine.White));
				}
		}

		/// <summary>
		///		Dibuja una imagen en una posición
		/// </summary>
		private void DrawAtPosition(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, ConvertVector(objImage.X + rctCamera.X, objImage.Y + rctCamera.Y), Convert(objImage.Color));
		}

		/// <summary>
		///		Dibuja una imagen escalada en un rectángulo
		/// </summary>
		private void DrawScaled(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, Convert(objImage.X + rctCamera.X, 
																							objImage.Y + rctCamera.Y, 
																							objImage.Width, 
																							objImage.Height), 
													Convert(objImage.Color));
		}

		/// <summary>
		///		Dibuja un objeto en toda la pantalla (los fondos, por ejemplo)
		/// </summary>
		private void DrawFullScreen(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, Convert(objImage.X + rctCamera.X, 
																							objImage.Y + rctCamera.Y, 
																							rctCamera.Width, rctCamera.Height), 
													Convert(objImage.Color));
		}

		/// <summary>
		///		Dibuja parte de una imagen en una posición
		/// </summary>
		private void DrawCrop(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ DrawTexture(objTexture, 
									new Vector2D(objImage.X + rctCamera.X, objImage.Y + rctCamera.Y), 
									null,
									objImage.RectangleSource, new Vector2D(objImage.ScaledWidth / 2, objImage.ScaledHeight / 2),
									objImage.Angle, new Vector2D(objImage.Scale, objImage.Scale), objImage.Color);
		}

		/// <summary>
		///		Dibuja una textura
		/// </summary>
		private void DrawTexture(CanvasBitmap objTexture, Vector2D? objPosition, Rectangle? rctDestination, Rectangle? rctSource,
														 Vector2D? pntOrigin, float fltRotation, Vector2D? vctScale, ColorEngine clrColor)
		{ if (fltRotation != 0)
				objSpriteBatch.DrawFromSpriteSheet(objTexture, Convert(objPosition ?? new Vector2D(0, 0)), Convert(rctSource),
																					 Convert(clrColor), Convert(pntOrigin ?? new Vector2D(0, 0)), fltRotation,
																					 Convert(vctScale ?? new Vector2D(1, 1)), CanvasSpriteFlip.None);
			else
				objSpriteBatch.DrawFromSpriteSheet(objTexture, Convert(objPosition ?? new Vector2D(0, 0)), 
																					 Convert(rctSource), Convert(clrColor),
																					 ConvertVector(0, 0), 0, Convert(vctScale ?? new Vector2D(1, 1)), 
																					 CanvasSpriteFlip.None);
		}

		/// <summary>
		///		Dibuja una imagen escalada a partir de parte de una imagen
		/// </summary>
		private void DrawFull(CanvasBitmap objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.DrawFromSpriteSheet(objTexture, Convert(objImage.X + rctCamera.X, 
																							objImage.Y + rctCamera.Y, 
																							objImage.Width, objImage.Height), 
																				 Convert(objImage.RectangleSource), 
																				 Convert(objImage.Color));
		}

		/// <summary>
		///		Dibuja un texto
		/// </summary>
		public void DrawText(AbstractTextModel objText)
		{ DrawText(objText, rctScreen);
		}

		/// <summary>
		///		Dibuja un texto utilizando una vista
		/// </summary>
		public void DrawText(AbstractTextModel objText, Rectangle rctCamera)
		{ CanvasTextFormat objFont = MainGame.ContentManager.GetSpriteFont(objText.ContentKey);

				objDrawingSession.DrawText(objText.Text, 
																	ConvertVector(objText.X + rctCamera.X, 
																								objText.Y + rctCamera.Y),
																	ConvertTocolor(objText.Color));

				objDrawingSession.DrawRectangle(new Windows.Foundation.Rect(50, 50, 50, 50),
																				ConvertTocolor(ColorEngine.Black));
		}

		/// <summary>
		///		Convierte un vector
		/// </summary>
		private System.Numerics.Vector2 ConvertVector(float intX, float intY)
		{ return new System.Numerics.Vector2(intX, intY);
		}

		/// <summary>
		///		Convierte un vector
		/// </summary>
		private System.Numerics.Vector2 Convert(Vector2D objVector)
		{ return ConvertVector(objVector.X, objVector.Y);
		}

		/// <summary>
		///		Convierte un rectángulo
		/// </summary>
		private Windows.Foundation.Rect Convert(float fltX, float fltY, float fltWidth, float fltHeight)
		{ return new Windows.Foundation.Rect((int) fltX, (int) fltY, (int) fltWidth, (int) fltHeight);
		}

		/// <summary>
		///		Convierte un rectángulo
		/// </summary>
		private Windows.Foundation.Rect Convert(Rectangle? rctRectangle)
		{ return new Windows.Foundation.Rect((int) (rctRectangle?.X ?? 0), (int) (rctRectangle?.Y ?? 0),
																				 (int) (rctRectangle?.Width ?? 0), (int) (rctRectangle?.Height ?? 0));
		}

		/// <summary>
		///		Convierte un color
		/// </summary>
		private System.Numerics.Vector4 Convert(ColorEngine clrColor)
		{ return new System.Numerics.Vector4(clrColor.R, clrColor.G, clrColor.B, clrColor.A) / 255.0f;
		}

		/// <summary>
		///		Convierte un color
		/// </summary>
		private Windows.UI.Color ConvertTocolor(ColorEngine clrColor)
		{ return Windows.UI.ColorHelper.FromArgb(clrColor.A, clrColor.R, clrColor.G, clrColor.B);
		}

		/// <summary>
		///		Finaliza el dibujo por lotes
		/// </summary>
		public void End()
		{ objSpriteBatch.Dispose();
			objSpriteBatch = null;
		}

		/// <summary>
		///		Manager principal
		/// </summary>
		private GameInternal.MainGame MainGame { get; }
	}
}
