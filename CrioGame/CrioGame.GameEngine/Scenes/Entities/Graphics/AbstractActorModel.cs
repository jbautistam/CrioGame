using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.UserInterface;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Graphics
{
	/// <summary>
	///		Modelo para tratamiento de un actor
	/// </summary>
	public abstract class AbstractActorModel : Common.Models.AbstractModelBase
	{
		public AbstractActorModel(IView objView, TimeSpan tsBetweenUpdate, GameObjectDimensions objDimensions)
						: base(tsBetweenUpdate)
		{ View = objView;
			Dimensions = objDimensions;
		}

		/// <summary>
		///		Añade un texto
		/// </summary>
		public TextModel AddText(string strContentKey, string strText, 
														 int intDeltaX, int intDeltaY, ColorEngine? clrColor = null)
		{ TextModel objText = new TextModel(this, strContentKey, strText, intDeltaX, intDeltaY, clrColor);

				// Añade el texto a la colección de sprites
					Sprites.Add(objText);
				// Devuelve el texto creado
					return objText;
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(string strContentKey, int intDeltaX, int intDeltaY, 
																 ColorEngine? clrColor = null, int intZOrder = 0)
		{ SpriteModel objSprite = new SpriteModel(this, strContentKey, TimeSpan.Zero, intDeltaX, intDeltaY, null, clrColor, intZOrder);

				// Añade el sprite
					Sprites.Add(objSprite);
				// Devuelve el sprite añadido
					return objSprite;
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(string strContentKey, int intDeltaX, int intDeltaY, Rectangle rctSource, 
																 ColorEngine? clrColor = null, int intZOrder = 0)
		{ SpriteModel objSprite = new SpriteModel(this, strContentKey, TimeSpan.Zero, intDeltaX, intDeltaY, rctSource,
																							clrColor, intZOrder);

				// Añade el sprite a la colección
					Sprites.Add(objSprite);
				// Devuelve el sprite añadido
					return objSprite;
		}

		/// <summary>
		///		Añade un control al contenido
		/// </summary>
		public void AddControl(AbstractControl objProgress)
		{ Sprites.Add(objProgress);
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(SpriteSheetContent objSpriteSheet, string strFramesKey, int intFrameIndex, 
																 int intDeltaX, int intDeltaY, 
																 ColorEngine? clrColor = null, int intZOrder = 0)
		{ SpriteModel objSprite = new SpriteModel(this, objSpriteSheet.ImageKey, TimeSpan.Zero, intDeltaX, intDeltaY, 
																							objSpriteSheet.SearchFrames(strFramesKey).Rectangles[intFrameIndex],
																							clrColor, intZOrder);

				// Añade el sprite
					Sprites.Add(objSprite);
				// Devuelve el sprite añadido
					return objSprite;
		}

		/// <summary>
		///		Añade una animación
		/// </summary>
		public SpriteModel AddAnimation(string strSheetContentKey, string strFramesKey, string strAnimationKey, string strContentKey, 
																		int intX, int intY, bool blnActive = true,
																		ColorEngine? clrTile = null, int intZOrder = 0)
		{ SpriteAnimableModel objAnimation = new SpriteAnimableModel(this, strSheetContentKey, strFramesKey, strAnimationKey, strContentKey, TimeSpan.Zero,
																																 intX, intY, clrTile, intZOrder);

				// Indica si está activo
					objAnimation.Active = blnActive;
				// Añade la animación a la colección de sprites
					Sprites.Add(objAnimation);
				// Devuelve la animación añadida
					return objAnimation;
		}

		/// <summary>
		///		Inicializa los datos
		/// </summary>
		public override void Initialize(IGameContext objContext)
		{ // Inicializa el actor
				InitializeActor(objContext);
			// Inicializa los sprites (después de inicializar el actor que es quien los define)
				for (int intIndex = 0; intIndex < Sprites.Count; intIndex++)
					Sprites[intIndex].Initialize(objContext);
		}

		/// <summary>
		///		Inicializa los datos del actor
		/// </summary>
		public abstract void InitializeActor(IGameContext objContext);

		/// <summary>
		///		Actualiza los datos
		/// </summary>
		public override void Update(IGameContext objContext)
		{ // Actualiza los sprites
				for (int intIndex = 0; intIndex < Sprites.Count; intIndex++)
					if (Sprites[intIndex].Active)
						Sprites[intIndex].Update(objContext);
			// Asigna el ancho y alto del primer sprite
				if (Sprites.Count > 0 && Dimensions.Position.IsEmpty)
					Dimensions.Position = new Rectangle(Dimensions.Position.X, Dimensions.Position.Y, Sprites[0].Width, Sprites[0].Height);
			// Actualiza los datos del actor
				UpdateActor(objContext);
		}

		/// <summary>
		///		Modifica los datos del actor
		/// </summary>
		public abstract void UpdateActor(IGameContext objContext);

		/// <summary>
		///		Dibuja el actor
		/// </summary>
		public virtual void Draw(IGameContext objContext)
		{ Sprites.Draw(objContext, View.ViewPortScreen);
		}

		/// <summary>
		///		Vista a la que se asocia el actor
		/// </summary>
		protected IView View { get; }

		/// <summary>
		///		Dimensiones del objeto en el juego
		/// </summary>
		public GameObjectDimensions Dimensions { get; }

		/// <summary>
		///		Evaluación de colisiones
		/// </summary>
		public Components.Physics.CollisionTargets CollisionEvaluator { get; protected set; }

		/// <summary>
		///		Sprites
		/// </summary>
		protected SpriteModelCollection Sprites { get; } = new SpriteModelCollection();
	}
}
