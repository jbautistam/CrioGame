using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Controlador de escenas
	/// </summary>
	public interface ISceneController
	{
		/// <summary>
		///		Cambia la escena actual
		/// </summary>
		void SetScene(IScene objNewScene);

		/// <summary>
		///		Escena actual
		/// </summary>
		IScene ActualScene { get; }
	}
}
