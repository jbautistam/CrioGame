using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Clase base para los sprites dibujables
	/// </summary>
	public class SpriteModel : AbstractImageModelBase
	{
		public SpriteModel(AbstractModelBase objParent, string strContentKey,
											 GameObjectDimensions objDimensions, Rectangle? rctSource = null)
									: base(strContentKey, objDimensions, rctSource ?? new Rectangle())
		{ Parent = objParent;
			if (rctSource != null)
				{ RectangleSource = rctSource ?? new Rectangle();
					Dimensions.Resize(rctSource?.Width ?? 0, rctSource?.Height ?? 0);
				}
			DeltaX = (int) objDimensions.Position.X;
			DeltaY = (int) objDimensions.Position.Y;
		}

		/// <summary>
		///		Inicializa el elemento
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ 
		}

		/// <summary>
		///		Actualiza el elemento
		/// </summary>
		public override void Update(IGameContext objContext)
		{	
		}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctView)
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
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this, rctView);
		}

		/// <summary>
		///		Elemento padre
		/// </summary>
		protected AbstractModelBase Parent { get; }

		/// <summary>
		///		Desplazamiento X respecto al padre
		/// </summary>
		public int DeltaX { get; set; }

		/// <summary>
		///		Desplazamiento Y respecto al padre
		/// </summary>
		public int DeltaY { get; set; }
	}
}
