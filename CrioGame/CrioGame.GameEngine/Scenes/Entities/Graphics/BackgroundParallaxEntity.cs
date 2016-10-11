using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Fondo parallax
	/// </summary>
	public class BackgroundParallaxEntity : SpriteModel
	{ 
		public BackgroundParallaxEntity(IView objView, string strKey, 
																	  int intSpeed,
																	  int intX, int intY, int intZOrder = 0) 
					: base(null, strKey, new GameObjectDimensions(intX, intY, 0, 0, 1, 0, null, intZOrder))
		{ Speed = intSpeed;
			View = objView;
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Asigna el ancho y alto
				Dimensions = new GameObjectDimensions(Dimensions.Position.X, Dimensions.Position.Y,
																							View.ViewPortScreen.Width, View.ViewPortScreen.Height);
			// Si dividimos la pantalla por el ancho de la textura, sabemos el número de rectángulos que necesitamos
			// Añadimos 1 de forma que no exista ningún gap entre los diferentes rectángulos
				RectangleDraws = new Rectangle[((int) View.ViewPortScreen.Width) / ((int) Dimensions.Position.Width) + 1];
			// Asigna las posiciones iniciales del fondo
			//! Necesitamos que los rectángulos se ajusten lado a lado para crear el efecto
				for (int intIndex = 0; intIndex < RectangleDraws.Length; intIndex++)
					RectangleDraws[intIndex] = new Rectangle(intIndex * Dimensions.Position.Width, Dimensions.Position.X, 
																									 Dimensions.Position.Width, Dimensions.Position.Height);
		}

		/// <summary>
		///		Actualiza el fondo
		/// </summary>
		public override void Update(IGameContext objContext)
		{	for (int intIndex = 0; intIndex < RectangleDraws.Length; intIndex++)
				{	float fltX = (int) RectangleDraws[intIndex].X + Speed;

						// Dependiendo de si se está moviendo hacia la izquierda o la derecha
							if (Speed <= 0) // hacia la izquierda
								{	// Check the texture is out of view then put that texture at the end of the screen
										if (fltX <= - Dimensions.Position.Width)
											fltX = Dimensions.Position.Width * (RectangleDraws.Length - 1);
									//WrapTextureToLeft(i);
								}
							else // hacia la derecha
								{	// Check if the texture is out of view then position it to the start of the screen
										if (fltX >= Dimensions.Position.Width * (RectangleDraws.Length - 1))
											fltX = -Dimensions.Position.Width;
									//WrapTextureToRight(i);
								}
						// Asigna el nuevo rectángulo
							RectangleDraws[intIndex] = new Rectangle(fltX, RectangleDraws[intIndex].Y,
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

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctView)
		{ objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this, rctView);
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
