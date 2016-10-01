using System;

namespace Bau.Libraries.CrioGame.Common.Models.Messages
{
	/// <summary>
	///		Clase abstracta con el contenido de un mensaje
	/// </summary>
	public abstract class AbstractMessageModel
	{
		public AbstractMessageModel(int intMessageType, object objTag = null)
		{ MessageType = intMessageType;
			Tag = objTag;
		}

		/// <summary>
		///		Tipo de mensaje
		/// </summary>
		public int MessageType { get; }

		/// <summary>
		///		Información adicional asociada al mensaje
		/// </summary>
		public object Tag { get; }
	}
}
