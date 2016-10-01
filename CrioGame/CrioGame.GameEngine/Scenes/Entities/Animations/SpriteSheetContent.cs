using System;

using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations
{
	/// <summary>
	///		Imagen con una serie de sprites
	/// </summary>
	public class SpriteSheetContent : Common.Models.Contents.AbstractContentBase
	{ 
		public SpriteSheetContent(string strKey, string strImageKey) : base(strKey, null) 
		{ ImageKey = strImageKey;
		}

		/// <summary>
		///		Crea una hoja
		/// </summary>
		public SpriteSheetFrames CreateSheet(string strFramesKey, Rectangle[] arrRctRectangles)
		{ return SheetFrames.Add(strFramesKey, new SpriteSheetFrames(strFramesKey, ImageKey, arrRctRectangles));
		}

		/// <summary>
		///		Calcula los rectángulos de los frames iniciales
		/// </summary>
		public SpriteSheetFrames CreateSheet(string strFramesKey, int intRows, int intColumns, int intFrameWidth, int intFrameHeight)
		{ SpriteSheetFrames objSheet = SheetFrames.Add(strFramesKey, new SpriteSheetFrames(strFramesKey, ImageKey, intRows * intColumns));
			int intTop = 0;

				// Asigna los rectángulos
					for (int intRow = 0; intRow < intRows; intRow++)
						{ int intLeft = 0;

								// Genera los rectángulos por columnas
									for (int intColumn = 0; intColumn < intColumns; intColumn++)
										{ // Crea el rectángulo
												objSheet.Rectangles[intRow * intColumns + intColumn] = new Rectangle(intLeft, intTop, intFrameWidth, intFrameHeight);
											// Incrementa la posición izquierda
												intLeft += intFrameWidth;
										}
								// Incrementa la posición superior
									intTop += intFrameHeight + 1;
						}
				// Devuelve el sheet creado
					return objSheet;
		}

		/// <summary>
		///		Obtiene una serie de frames
		/// </summary>
		public SpriteSheetFrames SearchFrames(string strAnimationKey)
		{ return SheetFrames.Search(strAnimationKey);
		}

		/// <summary>
		///		Clave de la imagen de la que se obtiene la animación
		/// </summary>
		public string ImageKey { get; }

		/// <summary>
		///		Clave de la animación predeterminada
		/// </summary>
		public string DefaultAnimationKey 
		{ get { return "Default"; }
		}

		/// <summary>
		///		Diccionario con los frames del SpriteSheet
		/// </summary>
		private Common.Models.Collections.DictionaryContainer<SpriteSheetFrames> SheetFrames { get; } = new Common.Models.Collections.DictionaryContainer<SpriteSheetFrames>();
	}
}
