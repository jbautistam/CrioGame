using System;

namespace Bau.Libraries.CrioGame.Common.Models.Contents.Sounds
{
	/// <summary>
	///		Clase para los datos de sonido
	/// </summary>
	public class SoundContent : AbstractContentBase
	{
		/// <summary>
		///		Tipo de sonido
		/// </summary>
		public enum SoundType
		{ 
			/// <summary>Efecto</summary>
			Effect,
			/// <summary>Canción</summary>
			Song
		}

		public SoundContent(string strKey, string strContentKey, SoundType intType) : base(strKey, strContentKey)
		{	Type = intType;
		}

		/// <summary>
		///		Tipo del sonido almacenado
		/// </summary>
		public SoundType Type { get; }
	}
}
