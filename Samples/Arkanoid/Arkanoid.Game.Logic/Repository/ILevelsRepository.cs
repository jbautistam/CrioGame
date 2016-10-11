using System;

namespace Bau.Libraries.ArkanoidGame.Logic.Repository
{
	/// <summary>
	///		Interface para el repositorio de los niveles
	/// </summary>
  public interface ILevelsRepository
  {
		/// <summary>
		///		Obtiene la cadena de datos de un nivel
		/// </summary>
		string [] LoadLevel(int intLevel);
  }
}
