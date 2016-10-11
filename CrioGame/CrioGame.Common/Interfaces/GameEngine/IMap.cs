using System;

using Bau.Libraries.CrioGame.Common.Models;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Interface para los mapas de una escena
	/// </summary>
	public interface IMap 
	{
		/// <summary>
		///		Añade una entidad de control
		/// </summary>
		void AddControlEntity(AbstractModelBase objEntity, TimeSpan? tsBetweenUpdate = null);

		/// <summary>
		///		Elimina una entidad de control
		/// </summary>
		void RemoveControlEntity(AbstractModelBase objEntity);

		/// <summary>
		///		Añade una entidad al mapa sobre la vista predeterminada
		/// </summary>
		void AddGameEntity(string strLayerKey, AbstractModelBase objEntity, TimeSpan? tsBetweenUpdate = null);

		/// <summary>
		///		Añade una entidad al mapa
		/// </summary>
		void AddGameEntity(IView objView, string strLayerKey, AbstractModelBase objEntity, TimeSpan? tsBetweenUpdate = null);

		/// <summary>
		///		Añade una entidad de juego
		/// </summary>
		void AddGameEntity(System.Collections.Generic.List<Models.Structs.ViewLayerModel> objColViewLayer, 
											 AbstractModelBase objEntity, TimeSpan? tsBetweenUpdate = null);

		/// <summary>
		///		Elimina una entidad de control
		/// </summary>
		void RemoveGameEntity(AbstractModelBase objEntity);
	}
}
