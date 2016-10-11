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
		public AbstractActorModel(IScene objScene, GameObjectDimensions objDimensions)
		{ Scene = objScene;
			Dimensions = objDimensions;
		}

		/// <summary>
		///		Añade un texto
		/// </summary>
		public TextModel AddText(string strContentKey, string strText, float fltX, float fltY, ColorEngine? clrColor = null)
		{ return AddText(strContentKey, strText, new GameObjectDimensions(fltX, fltY, clrColor));
		}

		/// <summary>
		///		Añade un texto
		/// </summary>
		public TextModel AddText(string strContentKey, string strText, GameObjectDimensions objDimensions)
		{ TextModel objText = new TextModel(this, strContentKey, strText, objDimensions);

				// Añade el texto a la colección de sprites
					Sprites.Add(objText);
				// Devuelve el texto creado
					return objText;
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(string strContentKey, float fltX, float fltY)
		{ return AddSprite(strContentKey, new GameObjectDimensions(fltX, fltY));
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(string strContentKey, GameObjectDimensions objDimensions)
		{ SpriteModel objSprite = new SpriteModel(this, strContentKey, objDimensions);

				// Añade el sprite
					Sprites.Add(objSprite);
				// Devuelve el sprite añadido
					return objSprite;
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(string strContentKey, GameObjectDimensions objDimensions, Rectangle rctSource)
		{ SpriteModel objSprite = new SpriteModel(this, strContentKey, objDimensions, rctSource);

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
		public SpriteModel AddSprite(SpriteSheetContent objSpriteSheet, string strFramesKey, 
																 float fltX, float fltY, int intFrameIndex)
		{ return AddSprite(objSpriteSheet, strFramesKey, intFrameIndex, new GameObjectDimensions(fltX, fltY));
		}

		/// <summary>
		///		Añade un sprite
		/// </summary>
		public SpriteModel AddSprite(SpriteSheetContent objSpriteSheet, string strFramesKey, int intFrameIndex, 
																 GameObjectDimensions objDimensions)
		{ SpriteModel objSprite = new SpriteModel(this, objSpriteSheet.ContentKey, objDimensions, 
																							objSpriteSheet.SearchFrames(strFramesKey).Rectangles[intFrameIndex]);

				// Añade el sprite
					Sprites.Add(objSprite);
				// Devuelve el sprite añadido
					return objSprite;
		}

		/// <summary>
		///		Añade una animación
		/// </summary>
		public SpriteModel AddAnimation(string strSheetContentKey, string strFramesKey, string strAnimationKey, 
																		float fltX, float fltY, bool blnActive = true)
		{ return AddAnimation(strSheetContentKey, strFramesKey, strAnimationKey, 
													new GameObjectDimensions(fltX, fltY), blnActive);
		}

		/// <summary>
		///		Añade una animación
		/// </summary>
		public SpriteModel AddAnimation(string strSheetContentKey, string strFramesKey, string strAnimationKey, 
																		GameObjectDimensions objDimensions, bool blnActive = true)
		{ SpriteAnimableModel objAnimation = new SpriteAnimableModel(this, strSheetContentKey, strFramesKey, strAnimationKey,  
																																 objDimensions);

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
		{ int intActive = -1;

				// Actualiza los sprites
					for (int intIndex = 0; intIndex < Sprites.Count; intIndex++)
						if (Sprites[intIndex].Active)
							{ // Actualiza el sprite
									Sprites[intIndex].Update(objContext);
								// Inicializa el sprite activo
									if (intActive < 0)
										intActive = intIndex;
							}
				// Asigna el ancho y alto del primer sprite activo
					if (intActive >= 0)
						{ SpriteAnimableModel objAnimation = Sprites[intActive] as SpriteAnimableModel;

								if (objAnimation != null)
									Dimensions.Resize(objAnimation.ActualFrameDimensions);
								else
									{ SpriteModel objSprite = Sprites[intActive] as SpriteModel;
										
											if (objSprite != null)
												Dimensions.Position = new Rectangle(Dimensions.Position.X, Dimensions.Position.Y,
																														objSprite.Dimensions.Position.Width, objSprite.Dimensions.Position.Height);
									}
						}
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
		public virtual void Draw(IGameContext objContext, Rectangle rctRectangle)
		{ Sprites.Draw(objContext, rctRectangle);
		}

		/// <summary>
		///		Escena a la que se asocia el actor
		/// </summary>
		public IScene Scene { get; }

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
