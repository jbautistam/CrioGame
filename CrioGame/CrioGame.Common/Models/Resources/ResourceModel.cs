using System;

namespace Bau.Libraries.CrioGame.Common.Models.Resources
{
	/// <summary>
	///		Datos de un recurso
	/// </summary>
	public class ResourceModel
	{
		/// <summary>
		///		Tipo de recurso
		/// </summary>
		public enum ResourceType
			{ 
				/// <summary>Tipo de recurso</summary>
				Image,
				/// <summary>Música de fondo</summary>
				Song,
				/// <summary>Efecto de sonido</summary>
				Effect,
				/// <summary>Fuente</summary>
				Font,
				/// <summary>Hoja de imágenes</summary>
				SpriteSheet
			}

		public ResourceModel(ResourceType intType, string strKey, string strPath)
		{ Type = intType;
			Key = strKey;
			Path = strPath;
		}

		/// <summary>
		///		Tipo del recurso
		/// </summary>
		public ResourceType Type { get; }

		/// <summary>
		///		Clave del recurso
		/// </summary>
		public string Key { get; }

		/// <summary>
		///		Ruta del recurso en el contenido
		/// </summary>
		public string Path { get; }

		/// <summary>
		///		Hojas del recurso
		/// </summary>
		public Collections.ListKey<ResourceSheetModel> Sheets { get; } = new Collections.ListKey<ResourceSheetModel>();
	}
}
