using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface
{
	/// <summary>
	///		Control para manejar un checkbox
	/// </summary>
	public class CheckBoxControl : AbstractControl
	{
		public CheckBoxControl(Rectangle rctPosition, bool blnIsChecked, TimeSpan tsBetweenUpdate, int intZOrder = 0) 
								: base(rctPosition, tsBetweenUpdate, intZOrder)
		{ IsChecked = blnIsChecked;
		}

		/// <summary>
		///		Crea un checkbox
		/// </summary>
		public static CheckBoxControl Create(IGameContext objContext, bool blnIsChecked,
																				 float fltX, float fltY, 
																				 string strContentKey, string strSpriteSheet, 
																				 string strImageBackground, string strImageChecked, string strImageUnchecked, 
																				 string strFont, string strTextChecked, string strTextUnchecked, 
																				 ColorEngine clrTextChecked, ColorEngine clrTextUnchecked)
		{ CheckBoxControl cmdButton = new CheckBoxControl(new Rectangle(fltX, fltY), blnIsChecked,
																											TimeSpan.FromMilliseconds(20));
			SpriteSheetContent objSpriteSheet = objContext.GameController.ContentController.GetContent(strSpriteSheet) as SpriteSheetContent;

				// Asigna las imágenes
					if (!string.IsNullOrEmpty(strImageBackground))
						cmdButton.Background = new SpriteModel(cmdButton, strContentKey, 
																									 TimeSpan.FromMilliseconds(20), 
																									 0, 0,
																									 objSpriteSheet.SearchFrames(strImageBackground).Rectangles[0],
																									 null, 0);
					if (!string.IsNullOrEmpty(strImageChecked))
						cmdButton.CheckedImage = new SpriteModel(cmdButton, strContentKey, 
																										 TimeSpan.FromMilliseconds(20), 0, 0,
																										 objSpriteSheet.SearchFrames(strImageChecked).Rectangles[0],
																										 null, 0);
					if (!string.IsNullOrEmpty(strImageUnchecked))
						cmdButton.UncheckedImage = new SpriteModel(cmdButton, strContentKey, 
																											 TimeSpan.FromMilliseconds(20), 0, 0,
																											 objSpriteSheet.SearchFrames(strImageUnchecked).Rectangles[0],
																											 null, 0);
				// Asigna el texto
					if (!string.IsNullOrEmpty(strTextChecked))
						cmdButton.TextChecked = new TextModel(cmdButton, strFont, strTextChecked, 20, 10, clrTextChecked);
					if (!string.IsNullOrEmpty(strTextUnchecked))
						cmdButton.TextUnchecked = new TextModel(cmdButton, strFont, strTextUnchecked, 20, 10, clrTextUnchecked);
				// Devuelve el control
					return cmdButton;
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ SpriteModel objSprite = CheckedImage;

				// Busca una imagen válida para calcular los datos
					if (objSprite == null)
						objSprite = UncheckedImage;
					if (objSprite == null)
						objSprite = Background;
				// Si hay una imagen válida
					if (objSprite != null)
						{ // Cambia el ancho del checkbox (para calcular si el ratón ha pulsado sobre el checkbox)
								Position = new Rectangle(Position.X, Position.Y, objSprite.RectangleSource.Width, objSprite.RectangleSource.Height);
							// Cambia la posición de los textos
								UpdateTextPosition(objSprite, TextChecked);
								UpdateTextPosition(objSprite, TextUnchecked);
						}
				// Guarda el momento actual
					TimeLastUpdate = objContext.ActualTime;
		}

		/// <summary>
		///		Modifica la posición de la etiqueta
		/// </summary>
		private void UpdateTextPosition(SpriteModel objSprite, TextModel objText)
		{	if (objSprite != null && objText != null)
				{ objText.DeltaX = (int) objSprite.RectangleSource.Width + 10;
					objText.DeltaY = (int) (objSprite.RectangleSource.Height / 2) - 10;
				}
		}

		/// <summary>
		///		Modifica el control
		/// </summary>
		public override void Update(IGameContext objContext)
		{ if ((objContext.ActualTime - TimeLastUpdate).TotalMilliseconds > 500)
				{ if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedMouseButton(Common.Enums.MouseButtons.Left) &&
								Position.HasPoint(objContext.GameController.MainManager.GraphicsEngine.InputManager.CurrentMousePosition))
							{ // Cambia el valor
									IsChecked = !IsChecked;
								// Guarda el momento de la última modificación
									TimeLastUpdate = objContext.ActualTime;
							}
				}
		}

		/// <summary>
		///		Dibuja el control
		/// </summary>
		public override void Draw(IGameContext objContext)
		{ // ... el dibujo pasa por la siguiente función
		}

		/// <summary>
		///		Dibuja el botón en una posición
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ // Dibuja el fondo del botón
				if (Background != null)
					Background.Draw(objContext, rctCamera);
			// Dibuja la imagen
				if (IsChecked && CheckedImage != null)
					CheckedImage.Draw(objContext, rctCamera);
				else if (!IsChecked && UncheckedImage != null)
					UncheckedImage.Draw(objContext, rctCamera);
				else if (UncheckedImage != null)
					UncheckedImage.Draw(objContext, rctCamera);
				else if (CheckedImage != null)
					CheckedImage.Draw(objContext, rctCamera);
			// Dibuja el texto del botón
				if (IsChecked && TextChecked != null)
					TextChecked.Draw(objContext, rctCamera);
				else if (TextUnchecked != null)
					TextUnchecked.Draw(objContext, rctCamera);
				else if (TextChecked != null)
					TextChecked.Draw(objContext, rctCamera);
		}

		/// <summary>
		///		Momento de la última modificación
		/// </summary>
		private TimeSpan TimeLastUpdate { get; set; }

		/// <summary>
		///		Texto cuando se selecciona el checkbox
		/// </summary>
		public TextModel TextChecked { get; set; }

		/// <summary>
		///		Texto cuando el checkbox no esta seleccionado
		/// </summary>
		public TextModel TextUnchecked { get; set; }

		/// <summary>
		///		Fondo cuando el botón tiene el foco
		/// </summary>
		public SpriteModel UncheckedImage { get; set; }

		/// <summary>
		///		Fondo cuando el botón está inactivo
		/// </summary>
		public SpriteModel CheckedImage { get; set; }

		/// <summary>
		///		Indica si el botón se ha seleccionado
		/// </summary>
		public bool IsChecked { get; set; }
	}
}
