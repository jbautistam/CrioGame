using System;

namespace Bau.Libraries.Mines.Logic.Messages
{
	/// <summary>
	///		Mensaje de "enemigo eliminado"
	/// </summary>
	public class EnemyKillMessage : Bau.Libraries.CrioGame.Common.Models.Messages.AbstractMessageModel
	{
		public EnemyKillMessage(int intMessageType, int intScore) : base(intMessageType)
		{ Score = intScore;
		}

		/// <summary>
		///		Puntuación
		/// </summary>
		public int Score { get; }
	}
}
