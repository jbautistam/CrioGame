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
		public SpriteModel(AbstractModelBase objParent, string strContentKey, TimeSpan tsBetweenUpdate,
											 int intX, int intY, Rectangle? rctSource = null, ColorEngine? clrTile = null,
											 int intZOrder = 0)
									: base(objParent, strContentKey, tsBetweenUpdate, intX, intY, clrTile, intZOrder)
		{ if (rctSource != null)
				{ RectangleSource = rctSource ?? new Rectangle();
					Width = (int) (rctSource?.Width ?? 0);
					Height = (int) (rctSource?.Height ?? 0);
				}
			DeltaX = intX;
			DeltaY = intY;
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
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this);
		}

		/// <summary>
		///		Dibuja el elemento
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctView)
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
				objContext.GameController.MainManager.GraphicsEngine.SpriteBatch.Draw(this, rctView);
		}

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
