using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;

namespace Bau.Libraries.CrioGame.Common.Models.Graphics
{
	/// <summary>
	///		Modelo para los textos
	/// </summary>
	public abstract class AbstractTextModel : AbstractDrawableModelBase
	{
		public AbstractTextModel(string strContentKey, string strText, Structs.GameObjectDimensions objDimensions) 
								: base(strContentKey, objDimensions)
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
