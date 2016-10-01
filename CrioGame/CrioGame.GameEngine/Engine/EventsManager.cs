using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Messages;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Manager de eventos
	/// </summary>
	public class EventsManager : Common.Interfaces.GameEngine.IGameEventsController
	{
		/// <summary>
		///		Encola un mensaje
		/// </summary>
		public void Enqueue(AbstractMessageModel objMessage)
		{ Messages.Add(objMessage);
		}

		/// <summary>
		///		Obtiene los mensajes de determiando tipo
		/// </summary>
		public List<TypeData> Dequeue<TypeData>() where TypeData : AbstractMessageModel
		{ List<TypeData> objColTarget = new List<TypeData>();

				// Obtiene los mensajes
					for (int intIndex = Messages.Count - 1; intIndex >= 0; intIndex--)
						if (Messages[intIndex].GetType() == typeof(TypeData))
							{ // Añade el mensaje a la colección de salida 
									objColTarget.Add(Messages[intIndex] as TypeData);
								// Quita el mensaje de la lista
									Messages.RemoveAt(intIndex);
							}
				// Devuelve la colección de mensajes
					return objColTarget;
		}

		/// <summary>
		///		Mensaje
		/// </summary>
		private List<AbstractMessageModel> Messages { get; } = new List<AbstractMessageModel>();
	}
}
