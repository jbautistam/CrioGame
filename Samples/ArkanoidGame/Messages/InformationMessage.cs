using System;

namespace ArkanoidGame.Messages
{
	/// <summary>
	///		Mensaje de "enemigo eliminado"
	/// </summary>
	public class InformationMessage : Bau.Libraries.CrioGame.Common.Models.Messages.AbstractMessageModel
	{	// Enumerados públicos
			/// <summary>
			///		Tipo de mensaje informativo
			/// </summary>
			public enum InformationType
				{ 
					/// <summary>Se ha matado un enemigo</summary>
					KillBrick,
					/// <summary>Añadir puntuación</summary>
					AddScore,
					/// <summary>Se ha eliminado una pelota</summary>
					KillBall,
					/// <summary>Se ha eliminado el jugador</summary>
					KillPlayer,
					/// <summary>El jugador ha obtenido una nueva vida</summary>
					NewLife,
					/// <summary>Crea una serie de pelotas</summary>
					CreateBalls
				}
		
		public InformationMessage(InformationType intMessageType, int intScore, object objTag = null) : base((int) intMessageType, objTag)
		{	Type = intMessageType;
			Score = intScore;
		}

		/// <summary>
		///		Tipo de mensaje
		/// </summary>
		public InformationType Type { get; }

		/// <summary>
		///		Puntuación
		/// </summary>
		public int Score { get; }
	}
}
