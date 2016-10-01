using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models.Graphics
{
	/// <summary>
	///		Modelo para los textos
	/// </summary>
	public abstract class AbstractTextModel : AbstractDrawableModelBase
	{
		public AbstractTextModel(AbstractModelBase objParent, string strContentKey, string strText, int intX, int intY, 
														 Structs.ColorEngine? clrColor = null, int intZOrder = 0) 
								: base(objParent, strContentKey, TimeSpan.FromMilliseconds(8000), intX, intY, clrColor, intZOrder)
		{ Text = strText;
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
		///		Texto del dibujo
		/// </summary>
		public string Text { get; set; }
	}
}
