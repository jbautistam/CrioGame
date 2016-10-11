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
		public TextModel(AbstractModelBase objParent, string strContentKey, string strText, 
										 GameObjectDimensions objDimensions) 
								: base(strContentKey, strText, objDimensions)
		{ Parent = objParent;
			DeltaX = (int) objDimensions.Position.X;
			DeltaY = (int) objDimensions.Position.Y; 
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
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ // Cambia la posición
				if (Parent != null)
					{ if (Parent is UserInterface.AbstractControl)
							Dimensions.MoveTo(DeltaX + (int) (Parent as UserInterface.AbstractControl).Dimensions.Position.X,
																DeltaY + (int) (Parent as UserInterface.AbstractControl).Dimensions.Position.Y);
						else if (Parent is AbstractActorModel)
							Dimensions.MoveTo(DeltaX + (int) (Parent as AbstractActorModel).Dimensions.Position.X,
																DeltaY + (int) (Parent as AbstractActorModel).Dimensions.Position.Y);
					}
			// Dibuja la imagen
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.DrawText(this, rctCamera);
		}

		/// <summary>
		///		Objeto padre
		/// </summary>
		protected AbstractModelBase Parent { get; }

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
