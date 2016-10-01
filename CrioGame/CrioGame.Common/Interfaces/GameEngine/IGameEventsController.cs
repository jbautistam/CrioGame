using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Interface para los controladores de eventos del juego
	/// </summary>
	public interface IGameEventsController
	{
		/// <summary>
		///		Encola un mensaje
		/// </summary>
		void Enqueue(Models.Messages.AbstractMessageModel objMessage);

		/// <summary>
		///		Obtiene los mensajes de determiando tipo
		/// </summary>
		System.Collections.Generic.List<TypeData> Dequeue<TypeData>() where TypeData : Models.Messages.AbstractMessageModel;
	}
}
