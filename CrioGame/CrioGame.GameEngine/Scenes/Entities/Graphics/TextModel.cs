using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Modelo para los textos
	/// </summary>
	public class TextModel : AbstractTextModel
	{
		public TextModel(AbstractModelBase objParent, string strContentKey, string strText, int intX, int intY, 
										 ColorEngine? clrColor = null, int intZOrder = 0) 
								: base(objParent, strContentKey, strText, intX, intY, clrColor, intZOrder)
		{ DeltaX = intX;
			DeltaY = intY; 
		}

		/// <summary>
		///		Inicializa el objeto
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // ... inicializa
		}

		/// <summary>
		///		Modifica el objeto
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // ... modifica el objeto
		}

		/// <summary>
		///		Dibuja el objeto
		/// </summary>
		public override void Draw(IGameContext objContext)
		{ // Cambia la posición
				if (Parent != null)
					{ if (Parent is UserInterface.AbstractControl)
							{ X = DeltaX + (int) (Parent as UserInterface.AbstractControl).Position.X;
								Y = DeltaY + (int) (Parent as UserInterface.AbstractControl).Position.Y;
							}
						else if (Parent is AbstractActorModel)
							{ X = DeltaX + (int) (Parent as AbstractActorModel).Dimensions.Position.X;
								Y = DeltaY + (int) (Parent as AbstractActorModel).Dimensions.Position.Y;
							}
					}
			// Dibuja la imagen
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.DrawText(this);
		}

		/// <summary>
		///		Dibuja el objeto
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ // Cambia la posición
				if (Parent != null)
					{ if (Parent is UserInterface.AbstractControl)
							{ X = DeltaX + (int) (Parent as UserInterface.AbstractControl).Position.X;
								Y = DeltaY + (int) (Parent as UserInterface.AbstractControl).Position.Y;
							}
						else if (Parent is AbstractActorModel)
							{ X = DeltaX + (int) (Parent as AbstractActorModel).Dimensions.Position.X;
								Y = DeltaY + (int) (Parent as AbstractActorModel).Dimensions.Position.Y;
							}
					}
			// Dibuja la imagen
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.DrawText(this, rctCamera);
		}

		/// <summary>
		///		Desplazamiento X
		/// </summary>
		public int DeltaX { get; set; }

		/// <summary>
		///		Desplazamiento Y
		/// </summary>
		public int DeltaY { get; set; }
	}
}
