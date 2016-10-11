﻿using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes
{
	/// <summary>
	///		Clase abstracta para el tratamiento de una escena
	/// </summary>
	public abstract class AbstractSceneModel : IScene
	{	// Constantes privadas
			private const string cnstStrKeyViewDefault = "Default";
		// Enumerados públicos
			/// <summary>
			///		Capas predefinidas
			/// </summary>
			public enum Layer
				{
					/// <summary>Capa con los fondos</summary>
					Background,
					/// <summary>Capa con el interface de usuario</summary>
					UserInterface
				}

		public AbstractSceneModel()
		{ MainMap = new Layers.MapModel(this);
		}

		/// <summary>
		///		Carga el contenido
		/// </summary>
		public void LoadContent(IGameContext objContext)
		{ // Crea la vista predeterminadao
				ViewDefault = CreateView(cnstStrKeyViewDefault, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, 100, 100),
																 new Rectangle(0, 0, 100, 100), 0);
			// Carga el contenido de la escena
				LoadContentScene(objContext);
		}

		/// <summary>
		///		Carga el contenido de la escena
		/// </summary>
		public abstract void LoadContentScene(IGameContext objContext);

		/// <summary>
		///		Añade una vista
		/// </summary>
		public IView CreateView(string strKey, Rectangle rctPercentScreen, Rectangle rctWorld, Rectangle rctCamera, int intZOrder)
		{ return Views.Add(strKey, rctPercentScreen, rctWorld, rctCamera, intZOrder);
		}

		/// <summary>
		///		Inicializa las capas
		/// </summary>
		public void Initialize(IGameContext objContext)
		{ // Inicializa las vistas
				Views.Initialize(objContext);
			// Llama a la inicialización adicional de la escena
				InitializeScene(objContext);
			// Inicializa el mapa
				MainMap.Initialize(objContext);
			// Guarda el momento en que se ha inicializado la escena
				TimeStart = objContext.ActualTime;
		}

		/// <summary>
		///		Inicializa la escena
		/// </summary>
		public abstract void InitializeScene(IGameContext objContext);

		/// <summary>
		///		Actualiza las capas
		/// </summary>
		public void Update(IGameContext objContext)
		{ // Actualiza los datos de la vista
				Views.Initialize(objContext);
			// Actualiza los datos del mapa
				MainMap.Update(objContext);
			// Llama a la actualización adicional de la escena (si ha pasado un tiempo, sobre todo
			// para evitar que se cambie de escena demasiado rápido)
				if ((objContext.ActualTime - TimeStart).TotalSeconds > 2)
					UpdateScene(objContext);
		}

		/// <summary>
		///		Actualiza los datos de la escena
		/// </summary>
		public abstract void UpdateScene(IGameContext objContext);

		/// <summary>
		///		Dibuja el mapa
		/// </summary>
		public void Draw(IGameContext objContext)
		{ MainMap.Draw(objContext);
		}

		/// <summary>
		///		Vista predeterminada
		/// </summary>
		public IView ViewDefault { get; private set; }

		/// <summary>
		///		Vistas asociadas a la escena
		/// </summary>
		public Layers.ViewModelCollection Views { get; } = new Layers.ViewModelCollection();

		/// <summary>
		///		Mapa de la escena (para cumplir con la interface)
		/// </summary>
		public IMap Map 
		{ get { return MainMap; }
		}

		/// <summary>
		///		Mapa de la escena
		/// </summary>
		internal Layers.MapModel MainMap { get; }

		/// <summary>
		///		Momento en que se inicia la escena
		/// </summary>
		private TimeSpan TimeStart { get; set; }
	}
}
