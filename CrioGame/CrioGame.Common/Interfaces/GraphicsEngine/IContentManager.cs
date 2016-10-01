using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine
{
	/// <summary>
	///		Controlador de contenido
	/// </summary>
	public interface IContentManager
	{
		/// <summary>
		///		Inicializa el controlador de contenido
		/// </summary>
		void Initialize(string strContentRoot);

		/// <summary>
		///		Carga una imagen
		/// </summary>
		void Load(Models.Contents.AbstractContentBase objContent);

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		void Unload();
	}
}
