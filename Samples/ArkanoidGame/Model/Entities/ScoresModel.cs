using System;

namespace ArkanoidGame.Model.Entities
{
	/// <summary>
	///		Clase para mantener la información de puntuación, vidas ...
	/// </summary>
	public class ScoresModel
	{
		public ScoresModel(int intLevel, int intScore, int intLives)
		{ Level = intLevel;
			Score = intScore;
			Lives = intLives;
		}

		/// <summary>
		///		Nivel
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		///		Puntuación
		/// </summary>
		public int Score { get; set; }

		/// <summary>
		///		Número de vidas
		/// </summary>
		public int Lives { get; set; }
	}
}
