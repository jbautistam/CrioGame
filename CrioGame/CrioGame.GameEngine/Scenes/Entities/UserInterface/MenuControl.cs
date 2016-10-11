using System;
using System.Collections.Generic;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface
{
	/// <summary>
	///		Control para mostrar un menú
	/// </summary>
	public class MenuControl : AbstractControl
	{
		public MenuControl(GameObjectDimensions objDimensions) : base(objDimensions)
		{
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{
		}

		/// <summary>
		///		Modifica el control
		/// </summary>
		public override void Update(IGameContext objContext)
		{ if (Items.Count > 0)
				{ int intLastIndex = ActualIndex;

						// Cambia el índice actual
							if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Common.Enums.Keys.Up))
								{ ActualIndex--;
									if (ActualIndex < 0)
										ActualIndex = Items.Count - 1;
								}
							else if (objContext.GameController.MainManager.GraphicsEngine.InputManager.ChangedPressedKey(Common.Enums.Keys.Down))
								ActualIndex = (ActualIndex + 1) % Items.Count;
						// Cambia el foco
							if (intLastIndex != ActualIndex)
								for (int intIndex = 0; intIndex < Items.Count; intIndex++)
									Items[intIndex].Focused = ActualIndex == intIndex;
				}
		}

		/// <summary>
		///		Dibuja los elementos
		/// </summary>
		public override void Draw(IGameContext objContext, Rectangle rctCamera)
		{ for (int intIndex = 0; intIndex < Items.Count; intIndex++)
				Items[intIndex].Draw(objContext, rctCamera);
		}

		/// <summary>
		///		Elementos del menú
		/// </summary>
		public List<ButtonControl> Items { get; } = new List<ButtonControl>();

		/// <summary>
		///		Indice actual
		/// </summary>
		public int ActualIndex { get; set; }
	}
}
