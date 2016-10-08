using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Interface del controlador de contenido del motor de juegos
	/// </summary>
	public interface IGameContentDictionary
	{
		/// <summary>
		///		Añade contenido
		/// </summary>
		void AddContent(string strKey, Models.Contents.AbstractContentBase objContent);

		/// <summary>
		///		Añade una imagen
		/// </summary>
		void AddImage(string strKey, string strContentKey);

		/// <summary>
		///		Añade una fuente
		/// </summary>
		void AddFont(string strKey, string strContentKey);

		/// <summary>
		///		Añade una fuente
		/// </summary>
		void AddFont(string strKey, Models.Contents.Graphics.FontContent objFont);

		/// <summary>
		///		Añade un sonido
		/// </summary>
		void AddSound(string strKey, string strContentKey, Models.Contents.Sounds.SoundContent.SoundType intType);

		/// <summary>
		///		Obtiene un contenido
		/// </summary>
		Models.Contents.AbstractContentBase GetContent(string strKey);

		/// <summary>
		///		Carga los recursos a partir de una cadena de texto
		/// </summary>
		void LoadResources(string strText);

		/// <summary>
		///		Carga los recursos a partir de una colección de recursos
		/// </summary>
		void LoadResources(System.Collections.Generic.List<Models.Resources.ResourceModel> objColResources);

		/// <summary>
		///		Raíz del contenido
		/// </summary>
		string ContentRoot { get; }
	}
}
