using System;

namespace MinesGame.Model.Entities
{
	/// <summary>
	///		Clase con los datos de puntuación
	/// </summary>
	public class ScoresModel
	{	
		public ScoresModel(int intScore, int intLifes)
		{ Score = intScore;
			Lifes = intLifes;
		}

		/// <summary>
		///		Puntuación
		/// </summary>
		internal int Score { get; set; } = 0;

		/// <summary>
		///		Número de vidas
		/// </summary>
		internal int Lifes { get; set; } = 2;
	}
}
