using System;

namespace AsteroidsGame.Messages
{
	/// <summary>
	///		Mensaje informativo
	/// </summary>
	public class InformationMessage : Bau.Libraries.CrioGame.Common.Models.Messages.AbstractMessageModel
	{
			/// <summary>
			///		Tipo de mensaje
			/// </summary>
			public enum MessageMode
				{ 
					/// <summary>Se ha eliminado un enemigo</summary>
					EnemyKill,
					/// <summary>Se ha eliminado al jugador</summary>
					PlayerKill
				}

		public InformationMessage(MessageMode intMessageType, int intScore) : base((int) intMessageType)
		{ Type = intMessageType;
			Score = intScore;
		}

		/// <summary>
		///		Tipo de mensaje
		/// </summary>
		public MessageMode Type { get; }

		/// <summary>
		///		Puntuación
		/// </summary>
		public int Score { get; }
	}
}
