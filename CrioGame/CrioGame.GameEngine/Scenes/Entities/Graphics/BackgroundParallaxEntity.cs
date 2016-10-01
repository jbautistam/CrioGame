using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Fondo parallax
	/// </summary>
	public class BackgroundParallaxEntity : SpriteModel
	{ 
		public BackgroundParallaxEntity(IView objView, string strKey, 
																	  TimeSpan tsBetweenUpdate, int intSpeed, 
																	  int intX, int intY, int intZOrder = 0) 
					: base(null, strKey, tsBetweenUpdate, intX, intY, null, null, intZOrder)
		{ Speed = intSpeed;
			View = objView;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Asigna el ancho y alto
				Width = (int) View.ViewPortScreen.Width;
				Height = (int) View.ViewPortScreen.Height;
			// If we divide the screen with the texture width then we can determine the number of tiles need.
			// We add 1 to it so that we won’t have a gap in the tiling
				RectangleDraws = new Rectangle[((int) View.ViewPortScreen.Width) / Width + 1];
			// Asigna las posiciones iniciales del fondo
			//! Necesitamos que los títulos se ajusten lado a lado para crear el efecto
				for (int intIndex = 0; intIndex < RectangleDraws.Length; intIndex++)
					RectangleDraws[intIndex] = new Rectangle(intIndex * Width, Y, Width, Height);
		}

		/// <summary>
		///		Actualiza el fondo
		/// </summary>
		public override void Update(IGameContext objContext)
		{	for (int intIndex = 0; intIndex < RectangleDraws.Length; intIndex++)
				{	int intX = (int) RectangleDraws[intIndex].X + Speed;

						// Dependiendo de si se está moviendo hacia la izquierda o la derecha
							if (Speed <= 0) // hacia la izquierda
								{	// Check the texture is out of view then put that texture at the end of the screen
										if (intX <= -Width)
											intX = Width * (RectangleDraws.Length - 1);
									//WrapTextureToLeft(i);
								}
							else // hacia la derecha
								{	// Check if the texture is out of view then position it to the start of the screen
										if (intX >= Width * (RectangleDraws.Length - 1))
											intX = -Width;
									//WrapTextureToRight(i);
								}
						// Asigna el nuevo rectángulo
							RectangleDraws[intIndex] = new Rectangle(intX, RectangleDraws[intIndex].Y,
																											 RectangleDraws[intIndex].Width, RectangleDraws[intIndex].Height);
				}
		}

//private void WrapTextureToLeft(int index)
//{
//// If the textures are scrolling to the left, when the tile wraps, it should be put at the
//// one pixel to the right of the tile before it.
//int prevTexture = index - 1;
//if (prevTexture < 0)
//prevTexture = positions.Length - 1;
//positions[index].X = positions[prevTexture].X + texture.Width;
//}
//private void WrapTextureToRight(int index)
//{
//// If the textures are scrolling to the right, when the tile wraps, it should be
////placed to the left of the tile that comes after it.
//int nextTexture = index + 1;
//if (nextTexture == positions.Length)
//nextTexture = 0;
//positions[index].X = positions[nextTexture].X - texture.Width;
//}

		///// <summary>
		/////		Dibuja el objeto
		///// </summary>
		//internal override void Draw(TimeSpan tsDrawTime, Engine.ICoconousController objGraphicsManager)
		//{	for (int intIndex = 0; intIndex < arrVctPositions.Length; intIndex++)
		//		{ Rectangle rctBackground = new Rectangle((int) arrVctPositions[intIndex].X, (int) arrVctPositions[intIndex].Y, Parent.ScreenWidth, Parent.ScreenHeight);

		//				objGraphicsManager.SpriteBatch.Draw(Image.Texture, rctBackground, ColorEngine.White);
		//		}
		//}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public override void Draw(IGameContext objContext)
		{ objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this);
		}

		/// <summary>
		///		Velocidad
		/// </summary>
		public int Speed { get; }

		/// <summary>
		///		Vista
		/// </summary>
		public IView View { get; }
	}
}
