using System;

namespace Bau.Libraries.Asteroids.Game.MonoGame
{
#if WINDOWS || LINUX
	/// <summary>
	///		Clase principal de la aplicación
	/// </summary>
	public static class Program
	{	
		/// <summary>
		///		Punto de entrada a la aplicación
		/// </summary>
		[STAThread]
		static void Main()
		{	new Logic.GameController(PathData).Start(new CrioGame.MonogameImpl.MonogameController());
		}

		/// <summary>
		///		Directorio 
		/// </summary>
		public static string PathData
		{ get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"); }
		}
	}
#endif
}
