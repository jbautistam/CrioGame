using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface
{
	/// <summary>
	///		Control para manejar un botón
	/// </summary>
	public class ButtonControl : AbstractControl
	{
		public ButtonControl(GameObjectDimensions objDimensions) : base(objDimensions)
		{ 
		}

		/// <summary>
		///		Crea un botón
		/// </summary>
		public static ButtonControl Create(IGameContext objContext, float fltX, float fltY, 
																			 string strContentKey, string strSpriteSheet,
																			 string strImageNormal, string strImageFocused, 
																			 string strFontKey, string strText, ColorEngine clrText, ColorEngine clrTextFocused)
		{ ButtonControl cmdButton = new ButtonControl(new GameObjectDimensions(fltX, fltY));
			SpriteSheetContent objSpriteSheet = objContext.GameController.ContentController.GetContent(strSpriteSheet) as SpriteSheetContent;

				// Asigna las imágenes
					cmdButton.Background = new SpriteModel(cmdButton, strContentKey, new GameObjectDimensions(0, 0),
																								 objSpriteSheet.SearchFrames(strImageNormal).Rectangles[0]);
					if (!string.IsNullOrEmpty(strImageFocused))
						cmdButton.FocusBackground = new SpriteModel(cmdButton, strContentKey, new GameObjectDimensions(0, 0),
																												objSpriteSheet.SearchFrames(strImageFocused).Rectangles[0]);
				// Asigna el texto
					if (!string.IsNullOrEmpty(strText))
						{ cmdButton.Text = new TextModel(cmdButton, strFontKey, strText, new GameObjectDimensions(20, 10, clrText));
							cmdButton.TextFocused = new TextModel(cmdButton, strFontKey, strText, new GameObjectDimensions(20, 10, clrTextFocused));
						}
				// Devuelve el control
					return cmdButton;
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ Dimensions.Resize(Background.RectangleSource);
		}

		/// <summary>
		///		Modifica el control
		/// </summary>
		public override void Update(IGameContext objContext)
		{ bool blnMouseOnButton = Dimensions.HasPoint(objContext.GameController.MainManager.GraphicsEngine.InputManager.CurrentMousePosition);

				// Indica que no se ha pulsado ni tiene el foco ...
					Clicked = false;
					Focused = false;
				// Comprueba si se ha pulsado sobre el botón
					if (objContext.GameController.MainManager.GraphicsEngine.InputManager.IsPressedMouseButton(Common.Enums.MouseButtons.Left) &&
							blnMouseOnButton)
						Clicked = true;
					else if (blnMouseOnButton)
						Focused = true;
		}

		/// <summary>
		///		Dibuja el botón en una posición
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ // Dibuja el fondo del botón
				if (!Enabled && DisabledBackground != null)
					DisabledBackground.Draw(objContext, rctCamera);
				else if (Focused && FocusBackground != null)
					FocusBackground.Draw(objContext, rctCamera);
				else if (Background != null)
					Background.Draw(objContext, rctCamera);
			// Dibuja el texto del botón
				if (Focused && TextFocused != null)
					TextFocused.Draw(objContext, rctCamera);
				else if (Text != null)
					Text.Draw(objContext, rctCamera);
		}

		/// <summary>
		///		Texto
		/// </summary>
		public Graphics.TextModel Text { get; set; }

		/// <summary>
		///		Etiqueta del texto enfocado
		/// </summary>
		public Graphics.TextModel TextFocused { get; set; }

		/// <summary>
		///		Fondo cuando el botón tiene el foco
		/// </summary>
		public Graphics.SpriteModel FocusBackground { get; set; }

		/// <summary>
		///		Fondo cuando el botón está inactivo
		/// </summary>
		public Graphics.SpriteModel DisabledBackground { get; set; }

		/// <summary>
		///		Indica si el botón tiene el foco
		/// </summary>
		public bool Focused { get; set; }

		/// <summary>
		///		Indica si el botón está activo
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		///		Indica si se ha pulsado sobre el botón
		/// </summary>
		public bool Clicked { get; private set; }
	}
}
