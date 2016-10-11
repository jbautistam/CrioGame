using System;

namespace Bau.Libraries.SpaceWar.Game.Logic.Model.Entities
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

		/// <summary>
		///		Número de asteroides
		/// </summary>
		public int Asteroids { get; set; }

		/// <summary>
		///		Naves enemigas
		/// </summary>
		public int Ships { get; set; }
	}
}
