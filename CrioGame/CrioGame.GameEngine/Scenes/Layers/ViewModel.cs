using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models;
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
		///		Añade una entidad
		/// </summary>
		public AbstractModelBase AddEntity(AbstractEngineSceneModel.Layer intLayer, AbstractModelBase objEntity)
		{ return AddEntity(intLayer.ToString(), objEntity);
		}

		/// <summary>
		///		Añade una entidad
		/// </summary>
		public AbstractModelBase AddEntity(string strLayerKey, AbstractModelBase objEntity)
		{ // Añade la entidad
				NewEntities.Add(new Tuple<string, AbstractModelBase>(strLayerKey, objEntity));
			// ... y la devuelve
				return objEntity;
		}

		/// <summary>
		///		Trata las nuevas entidades
		/// </summary>
		private void TreatNewEntities(IGameContext objContext)
		{	for (int intIndex = NewEntities.Count - 1; intIndex >= 0; intIndex--)
				{ Tuple<string, AbstractModelBase> objTuple = NewEntities[intIndex];
					LayerModel objLayer = SearchLayer(objTuple.Item1);

						// Inicializa la entidad que se está creando
							objTuple.Item2.Initialize(objContext);
						// Añade la entidad a las capas
							objLayer.Add(objTuple.Item2);
						// Elimina la entidad
							NewEntities.RemoveAt(intIndex);
				}
		}

		/// <summary>
		///		Obtiene la capa con una clave (si no existe la crea)
		/// </summary>
		private LayerModel SearchLayer(string strKey)
		{ if (AbstractEngineSceneModel.Layer.Background.ToString().Equals(strKey))
				return Background;
			else if (AbstractEngineSceneModel.Layer.UserInterface.ToString().Equals(strKey))
				return UserInterfaceLayer;
			else
				return GameLayers.Add(strKey, true);
		}

		/// <summary>
		///		Inicializa los datos de la vista
		/// </summary>
		public void Initialize(IGameContext objContext)
		{	// Obtiene las coordenadas de pantalla
				ViewPortScreen = ConvertCamera(objContext);
			// Crea las nuevas entidades (por si acaso)
				TreatNewEntities(objContext);
			// Inicializa la capa de fondo
				Background.Initialize(objContext);
			// Inicializa las capas de juego
				for (int intIndex = 0; intIndex < GameLayers.Count; intIndex++)
					GameLayers[intIndex].Initialize(objContext);
			// Inicializa la capa de interface de usuario
				UserInterfaceLayer.Initialize(objContext);
		}

		/// <summary>
		///		Actualiza los datos de las vistas
		/// </summary>
		public void Update(IGameContext objContext)
		{ // Obtiene las coordenadas de pantalla
				ViewPortScreen = ConvertCamera(objContext);
			// Crea las nuevas entidades (por si acaso)
				TreatNewEntities(objContext);
			// Trata las colisiones
				PhysicsEngine.Evaluate(this, GameLayers, objContext);
			// Actualiza la capa de fondo
				Background.Update(objContext);
			// Actualiza las capas de juego
				for (int intIndex = 0; intIndex < GameLayers.Count; intIndex++)
					GameLayers[intIndex].Update(objContext);
			// Actualiza la capa de interface de usuario
				UserInterfaceLayer.Update(objContext);
		}

		/// <summary>
		///		Dibuja el contenido de la vista
		/// </summary>
		public void Draw(IGameContext objContext)
		{	// Obtiene las coordenadas de pantalla
				ViewPortScreen = ConvertCamera(objContext);
			// Dibuja la capa de fondo
				Background.Draw(objContext, ViewPortScreen);
			// Dibuja las capas de juego
				for (int intIndex = 0; intIndex < GameLayers.Count; intIndex++)
					GameLayers[intIndex].Draw(objContext, ViewPortScreen);
			// Dibuja la capa de interface de usuario
				UserInterfaceLayer.Draw(objContext, ViewPortScreen);
		}

		/// <summary>
		///		Convierte la vista de cámara a un rectángulo que se ponga en la pantalla
		/// </summary>
		private Rectangle ConvertCamera(IGameContext objContext)
		{ int intWidth = (int) objContext.GameController.MainManager.GraphicsEngine.ScreenController.ViewPort.Width;
			int intHeight = (int) objContext.GameController.MainManager.GraphicsEngine.ScreenController.ViewPort.Height;

				// Obtiene el rectángulo convertido
				return new Rectangle(Map(Camera.ViewPortPercentScreen.X, intWidth),
														 Map(Camera.ViewPortPercentScreen.Y, intHeight),
														 Map(Camera.ViewPortPercentScreen.Width, intWidth),
														 Map(Camera.ViewPortPercentScreen.Height, intHeight));
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
		///		Capa de fondo
		/// </summary>
		internal LayerModel Background { get; } = new LayerModel(AbstractEngineSceneModel.Layer.Background.ToString(), false);

		/// <summary>
		///		Capa internas
		/// </summary>
		internal LayerModelCollection GameLayers { get; } = new LayerModelCollection();

		/// <summary>
		///		Capa de interface de usuario
		/// </summary>
		internal LayerModel UserInterfaceLayer { get; } = new LayerModel(AbstractEngineSceneModel.Layer.UserInterface.ToString(), false);

		/// <summary>
		///		Entidades nuevas (entidades que se han dado de alta en el proceso de Update de una entidad o en la inicialización)
		/// </summary>
		private List<Tuple<string, AbstractModelBase>> NewEntities { get; } = new List<Tuple<string, AbstractModelBase>>();

		/// <summary>
		///		Motor de físicas
		/// </summary>
		private Physics.PhysicsEngine PhysicsEngine { get; } = new Physics.PhysicsEngine();

		/// <summary>
		///		Rectángulo de la pantalla normalizado
		/// </summary>
		public Rectangle ViewPortScreen { get; private set; }
	}
}
