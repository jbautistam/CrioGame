using System;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GameEngine
{
	/// <summary>
	///		Interface para el tratamiento de una vista en pantalla
	/// </summary>
	public interface IView
	{
		/// <summary>
		///		Crea una capa en la vista
		/// </summary>
		void CreateLayer(string strKey, bool blnEvaluateCollisions);

		/// <summary>
		///		Añade una entidad a una capa por su clave
		/// </summary>
		Models.AbstractModelBase AddEntity(string strLayerKey, Models.AbstractModelBase objEntity);

		/// <summary>
		///		Cámara utilizada en la vista
		/// </summary>
		Models.Structs.CameraView Camera { get; }

		/// <summary>
		///		Rectángulo de la pantalla normalizado, es decir, a partir de <see cref="Camera"/> y de
		///	las dimensiones de la ventana la vista actual calculará los datos de pantalla
		/// </summary>
		Models.Structs.Rectangle ViewPortScreen { get; }
	}
}
