using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Interface para los datos de una escena
	/// </summary>
	public interface IScene
	{
		/// <summary>
		///		Añade una vista
		/// </summary>
		IView CreateView(string strKey, Rectangle rctPercentScreen, Rectangle rctWorld, Rectangle rctCamera, int intZOrder);

		/// <summary>
		///		Carga el contenido
		/// </summary>
		void LoadContent(IGameContext objContext);

		/// <summary>
		///		Inicializa una escena
		/// </summary>
		void Initialize(IGameContext objContext);

		/// <summary>
		///		Actualiza una escena
		/// </summary>
		void Update(IGameContext objContext);

		/// <summary>
		///		Dibuja una escena
		/// </summary>
		void Draw(IGameContext objContext);

		/// <summary>
		///		Vista predeterminada
		/// </summary>
		IView ViewDefault { get; }
	}
}
