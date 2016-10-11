using System;

namespace Bau.Libraries.ArkanoidGame.Logic
{
	/// <summary>
	///		Clase de configuración
	/// </summary>
	internal static class Configuration
	{
		/// <summary>
		///		Grupos para la evaluación de colisiones
		/// </summary>
		[Flags]
		public enum GroupCollisionObjects
			{ Player = 1, 
				Enemy = 2,
				Brick = 4,
				Ball = 8,
				Pill = 16,
				Laser = 32,
				Other = 256	
			}

		/// <summary>
		///		Repositorio que se utiliza para cargar los niveles
		/// </summary>
		internal static Repository.ILevelsRepository LevelsRepository { get; set; }

		/// <summary>
		///		Sonido de una explosión
		/// </summary>
		internal static string ExplosionSound { get; } =  "ExplosionSound";

		/// <summary>
		///		Sonido de un láser
		/// </summary>
		internal static string LaserSound { get; } =  "LaserSound";

		/// <summary>
		///		Tiempo entre los disparos del jugador
		/// </summary>
		internal static TimeSpan TimeSpanPlayerLaserFire { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de los disparos del jugador
		/// </summary>
		internal static TimeSpan TimeSpanPlayerLaserUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los Updates de las explosiones de los enemigos
		/// </summary>
		internal static TimeSpan TimeEnemyExplosion { get; } = TimeSpan.FromMilliseconds(10);
  }
}
