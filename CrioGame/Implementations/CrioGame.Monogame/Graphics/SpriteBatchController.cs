using System;

using Xna = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl.Graphics
{
	/// <summary>
	///		Controlador de dibujo por lotes
	/// </summary>
	public class SpriteBatchController : ISpriteBatch
	{	// Variables privadas
			private SpriteBatch objSpriteBatch;
			private Rectangle rctScreen;

		internal SpriteBatchController(GameInternal.MainGame objMainGame, GraphicsDevice grpsDevice)
		{ MainGame = objMainGame;
			objSpriteBatch = new SpriteBatch(grpsDevice);
		}

		/// <summary>
		///		Comienza el dibujo por lotes
		/// </summary>
		public void Begin()
		{ // Inicializa el rectángulo que define la pantalla
				rctScreen = new Rectangle(0, 0, MainGame.GraphicsDevice.Viewport.Width,
																	objSpriteBatch.GraphicsDevice.Viewport.Height);
			// Inicia el proceso de imágenes por lotes
				objSpriteBatch.Begin();
		}

		/// <summary>
		///		Obtiene las dimensiones de una imagen
		/// </summary>
		public Size2D GetDimensions(AbstractImageModelBase objImage)
		{ Texture2D objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				if (objTexture != null)
					return new Size2D(objTexture.Width, objTexture.Height);
				else
					return new Size2D(0, 0);
		}

		/// <summary>
		///		Dibuja una imagen escalando en un rectángulo (el de la cámara)
		/// </summary>
		public void Draw(AbstractImageModelBase objImage, Rectangle rctCamera)
		{ Texture2D objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				// Dibuja la imagen:
				//   -- Si no se ha definido ni un rectángulo origen ni un destino, dibuja directamente en una posición
				//   -- Si se ha definido un rectángulo origen, se dibuja parte de él en una posición
				//   -- Si se ha definido un rectángulo destino, se dibuja escalado
				//   -- Si se han definido los dos, se dibuja parte de él escalado
				if (objImage.RectangleDraws != null && objImage.RectangleDraws.Length > 0)
					DrawParts(objTexture, objImage, rctCamera);
				else if (objImage.RectangleSource.IsEmpty)
					DrawAtPosition(objTexture, objImage, rctCamera);
				else if (!objImage.RectangleSource.IsEmpty) // ... tenemos una posición y parte de una imagen
					DrawCrop(objTexture, objImage, rctCamera);
				else if (objImage.Dimensions.Position.Width != 0 && objImage.Dimensions.Position.Height != 0) // ... sólo está definido rectangle target
					DrawScaled(objTexture, objImage, rctCamera);
				else // ... tenemos dimensiones de la imagen y una parte de la imagen
					DrawFull(objTexture, objImage, rctCamera);
		}

		/// <summary>
		///		Dibuja una imagen escalada a partir de parte de una imagen
		/// </summary>
		public void DrawFull(AbstractImageModelBase objImage, Rectangle rctCamera)
		{ Texture2D objTexture = MainGame.ContentManager.GetImage(objImage.ContentKey);

				// Dibuja la imagen
					objSpriteBatch.Draw(objTexture, Convert(objImage.Dimensions.Position.X + rctCamera.X, 
																									objImage.Dimensions.Position.Y + rctCamera.Y, 
																									objImage.Dimensions.Position.Width, objImage.Dimensions.Position.Height), 
															Convert(objImage.RectangleSource), Convert(objImage.Dimensions.Color));
		}

		/// <summary>
		///		Dibuja una serie de partes de una textura
		/// </summary>
		private void DrawParts(Texture2D objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{	for (int intIndex = 0; intIndex < objImage.RectangleDraws.Length; intIndex++)
				{ Rectangle rctDraw = new	Rectangle(objImage.RectangleDraws[intIndex].X + rctCamera.X,
																						objImage.RectangleDraws[intIndex].Y + rctCamera.Y,
																						objImage.RectangleDraws[intIndex].Width,
																						objImage.RectangleDraws[intIndex].Height);
				 
						// Dibuja
							objSpriteBatch.Draw(objTexture, Convert(rctDraw) ?? new Xna.Rectangle(), Convert(ColorEngine.White));
				}
		}

		/// <summary>
		///		Dibuja una imagen en un posición
		/// </summary>
		private void DrawAtPosition(Texture2D objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, ConvertVector(objImage.Dimensions.Position.X + rctCamera.X, 
																										objImage.Dimensions.Position.Y + rctCamera.Y), 
													null, Convert(objImage.Dimensions.Color));
		}

		/// <summary>
		///		Dibuja una imagen escalada en un rectángulo
		/// </summary>
		private void DrawScaled(Texture2D objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, Convert(objImage.Dimensions.Position.X + rctCamera.X, 
																							objImage.Dimensions.Position.Y + rctCamera.Y, 
																							objImage.Dimensions.Position.Width, 
																							objImage.Dimensions.Position.Height), 
													Convert(objImage.Dimensions.Color));
		}

		/// <summary>
		///		Dibuja parte de una imagen en una posición
		/// </summary>
		private void DrawCrop(Texture2D objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ DrawTexture(objTexture, 
									new Vector2D(objImage.Dimensions.Position.X + rctCamera.X, objImage.Dimensions.Position.Y + rctCamera.Y), 
									null,
									objImage.RectangleSource, new Vector2D(objImage.Dimensions.ScaledDimensions.Width/ 2, objImage.Dimensions.ScaledDimensions.Height / 2),
									objImage.Dimensions.Angle, new Vector2D(objImage.Dimensions.Scale, objImage.Dimensions.Scale), 
									objImage.Dimensions.Color);
		}

		/// <summary>
		///		Dibuja una textura
		/// </summary>
		private void DrawTexture(Texture2D objTexture, Vector2D? objPosition, Rectangle? rctDestination, Rectangle? rctSource,
														 Vector2D? pntOrigin, float fltRotation, Vector2D? vctScale, ColorEngine clrColor,
														 SpriteEffects intEffects = SpriteEffects.None)
		{ if (fltRotation != 0)
				objSpriteBatch.Draw(objTexture, Convert(objPosition), Convert(rctDestination), 
														Convert(rctSource), Convert(pntOrigin), fltRotation, Convert(vctScale), Convert(clrColor), 
														intEffects, 0);
			else
				objSpriteBatch.Draw(objTexture, Convert(objPosition), Convert(rctDestination), 
														Convert(rctSource), null, fltRotation, Convert(vctScale), Convert(clrColor), 
														intEffects, 0);
		}

		/// <summary>
		///		Dibuja una imagen escalada a partir de parte de una imagen
		/// </summary>
		private void DrawFull(Texture2D objTexture, AbstractImageModelBase objImage, Rectangle rctCamera)
		{ objSpriteBatch.Draw(objTexture, Convert(objImage.Dimensions.Position.X + rctCamera.X, 
																							objImage.Dimensions.Position.Y + rctCamera.Y, 
																							objImage.Dimensions.Position.Width, 
																							objImage.Dimensions.Position.Height), 
													Convert(objImage.RectangleSource), Convert(objImage.Dimensions.Color));
		}

		/// <summary>
		///		Dibuja un texto utilizando una vista
		/// </summary>
		public void DrawText(AbstractTextModel objText, Rectangle rctCamera)
		{ SpriteFont objFont = MainGame.ContentManager.GetSpriteFont(objText.ContentKey);

				objSpriteBatch.DrawString(objFont, objText.Text, 
																	ConvertVector(objText.Dimensions.Position.X + rctCamera.X, 
																								objText.Dimensions.Position.Y + rctCamera.Y), 
																	Convert(objText.Dimensions.Color));
		}

		/// <summary>
		///		Convierte un vector
		/// </summary>
		private Xna.Vector2 ConvertVector(float intX, float intY)
		{ return new Xna.Vector2(intX, intY);
		}

		/// <summary>
		///		Convierte un vector
		/// </summary>
		private Xna.Vector2? Convert(Vector2D? objVector)
		{ if (objVector == null)
				return null;
			else
				return new Xna.Vector2(objVector?.X ?? 0, objVector?.Y ?? 0);
		}

		/// <summary>
		///		Convierte un rectángulo
		/// </summary>
		private Xna.Rectangle Convert(float fltX, float fltY, float fltWidth, float fltHeight)
		{ return new Xna.Rectangle((int) fltX, (int) fltY, (int) fltWidth, (int) fltHeight);
		}

		/// <summary>
		///		Convierte un rectángulo
		/// </summary>
		private Xna.Rectangle? Convert(Rectangle? rctRectangle)
		{ if (rctRectangle == null)
				return null;
			else
				return new Xna.Rectangle((int) (rctRectangle?.X ?? 0), (int) (rctRectangle?.Y ?? 0),
																 (int) (rctRectangle?.Width ?? 0), (int) (rctRectangle?.Height ?? 0));
		}

		/// <summary>
		///		Convierte un color
		/// </summary>
		private Xna.Color Convert(ColorEngine clrColor)
		{ return new Xna.Color(clrColor.R, clrColor.G, clrColor.B, clrColor.A);
		}

		/// <summary>
		///		Finaliza el dibujo por lotes
		/// </summary>
		public void End()
		{ objSpriteBatch.End();
		}

		/// <summary>
		///		Manager principal
		/// </summary>
		private GameInternal.MainGame MainGame { get; }
	}
}
