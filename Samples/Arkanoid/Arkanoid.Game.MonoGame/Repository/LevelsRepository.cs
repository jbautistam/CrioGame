using System;

namespace Bau.Libraries.ArkanoidGame.MonoGame.Repository
{
	/// <summary>
	///		Clase para lectura de datos de los niveles
	/// </summary>
	internal class LevelsRepository : CrioGame.Common.Repository.AbstractRepository, Logic.Repository.ILevelsRepository
	{
		/// <summary>
		///		Carga los ladrillos de la escena
		/// </summary>
		public string [] LoadLevel(int intLevel)
		{ string strFileName = System.IO.Path.Combine(Program.PathData, $"Level_{intLevel}.txt");
			
				// Carga el archivo
					if (System.IO.File.Exists(strFileName))
						try
							{ return GetLines(System.IO.File.ReadAllText(strFileName));
							}
						catch 
							{ 
							}
				// Si ha llegado hasta aquí es porque no ha podido cargar
					return new string[1];
		}
	}
}
