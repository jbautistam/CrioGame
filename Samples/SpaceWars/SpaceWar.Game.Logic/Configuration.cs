using System;

namespace Bau.Libraries.SpaceWar.Game.Logic
{
	/// <summary>
	///		Clase de configuración
	/// </summary>
	public static class Configuration
	{
		/// <summary>
		///		Grupos de los objetos del juego para la evaluación de colisiones
		/// </summary>
		[Flags]
		public enum GroupGameObjects
			{ Player = 1,
				Enemy = 2,
				Other = 4
			}

		/// <summary>
		///		Capa del juego
		/// </summary>
		internal static string LayerGame { get; } = "Game";

		/// <summary>
		///		Sonido de una explosión
		/// </summary>
		internal static string ExplosionSound { get; } =  "ExplosionSound";

		/// <summary>
		///		Sonido de un láser
		/// </summary>
		internal static string LaserSound { get; } =  "LaserSound";

		/// <summary>
		///		Tiempo entre los Updates de los fondos parallax
		/// </summary>
		internal static TimeSpan TimeSpanParallax { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre los Updates de la puntuación
		/// </summary>
		internal static TimeSpan TimeSpanScore { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre las actualizaciones del interface de usuario
		/// </summary>
		internal static TimeSpan TimeSpanUserInterface { get; } = TimeSpan.FromMilliseconds(30);

		/// <summary>
		///		Tiempo entre los Spawns de las naves enemigas
		/// </summary>
		internal static TimeSpan TimeSpawnShip { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de las naves enemigas
		/// </summary>
		internal static TimeSpan TimeSpanShipUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos de las naves enemigas
		/// </summary>
		internal static TimeSpan TimeSpanShipFire { get; } = TimeSpan.FromMilliseconds(1000);

		/// <summary>
		///		Tiempo entre los Updates de los disparos de las naves enemigas
		/// </summary>
		internal static TimeSpan TimeSpanMineLaserUpdate { get; } = TimeSpan.FromMilliseconds(10);

		/// <summary>
		///		Tiempo entre los disparos del jugador
		/// </summary>
		internal static TimeSpan TimeSpanPlayerLaserFire { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		///		Tiempo entre los Updates de los disparos del jugador
		/// </summary>
		internal static TimeSpan TimeSpanPlayerLaserUpdate { get; } = TimeSpan.FromMilliseconds(3);

		/// <summary>
		///		Tiempo entre los Updates de las explosiones de los enemigos
		/// </summary>
		internal static TimeSpan TimeEnemyExplosion { get; } = TimeSpan.FromMilliseconds(10);
  }
}
