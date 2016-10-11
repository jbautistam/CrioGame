using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Layers
{
	/// <summary>
	///		Clase con los datos de una vista en pantalla
	/// </summary>
	public class ViewModel : ListKeyItem, IView
	{ 
		public ViewModel(string strKey, CameraView objCamera, int intZOrder) : base(strKey)
		{ Camera = objCamera;
			ZOrder = intZOrder;
		}

		/// <summary>
		///		Crea una capa
		/// </summary>
		public void CreateLayer(string strKey, bool blnEvaluateCollisions)
		{ GameLayers.Add(strKey, blnEvaluateCollisions);
		}

		/// <summary>
		///		Inicializa los datos de la vista
		/// </summary>
		internal void Initialize(IGameContext objContext)
		{	int intWidth = (int) objContext.GameController.MainManager.GraphicsEngine.ScreenController.ViewPort.Width;
			int intHeight = (int) objContext.GameController.MainManager.GraphicsEngine.ScreenController.ViewPort.Height;

				// Obtiene el rectángulo convertido
					ViewPortScreen = new Rectangle(Map(Camera.ViewPortPercentScreen.X, intWidth),
																				 Map(Camera.ViewPortPercentScreen.Y, intHeight),
																				 Map(Camera.ViewPortPercentScreen.Width, intWidth),
																				 Map(Camera.ViewPortPercentScreen.Height, intHeight));
		}

		/// <summary>
		///		Obtiene la capa con una clave (si no existe la crea)
		/// </summary>
		private LayerModel SearchLayer(string strKey)
		{ if (AbstractSceneModel.Layer.Background.ToString().Equals(strKey))
				return Background;
			else if (AbstractSceneModel.Layer.UserInterface.ToString().Equals(strKey))
				return UserInterfaceLayer;
			else
				return GameLayers.Add(strKey, true);
		}

		/// <summary>
		///		Mapea un valor
		/// </summary>
		private float Map(float fltPercent, float fltValue)
		{ return fltPercent * fltValue / 100;
		}

		/// <summary>
		///		Datos de la cámara
		/// </summary>
		public CameraView Camera { get; }

		/// <summary>
		///		ZOrder de la vista
		/// </summary>
		public int ZOrder { get; set; }

		/// <summary>
		///		Rectángulo de la pantalla normalizado
		/// </summary>
		public Rectangle ViewPortScreen { get; private set; }

		/// <summary>
		///		Capa de fondo
		/// </summary>
		internal LayerModel Background { get; } = new LayerModel(AbstractSceneModel.Layer.Background.ToString(), false);

		/// <summary>
		///		Capa internas
		/// </summary>
		internal LayerModelCollection GameLayers { get; } = new LayerModelCollection();

		/// <summary>
		///		Capa de interface de usuario
		/// </summary>
		internal LayerModel UserInterfaceLayer { get; } = new LayerModel(AbstractSceneModel.Layer.UserInterface.ToString(), false);
	}
}
