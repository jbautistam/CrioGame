using System;

using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations
{
	/// <summary>
	///		Frames de un spriteSheet
	/// </summary>
	public class SpriteSheetFrames : Common.Models.Contents.AbstractContentBase
	{
		public SpriteSheetFrames(string strKey, string strContentKey, Rectangle [] arrRctRectangles) : base(strKey, strContentKey)
		{ Rectangles = arrRctRectangles;
		}

		public SpriteSheetFrames(string strKey, string strContentKey, int intFrames) : base(strKey, strContentKey)
		{ Rectangles = new Rectangle[intFrames];
		}

		/// <summary>
		///		Crea una animación con un array de frames
		/// </summary>
		public void CreateAnimation(string strAnimationKey, int [] arrIntFramesIndex, int intFrameTime)
		{ SpriteSheetAnimation objAnimation = SheetAnimations.Add(strAnimationKey, new SpriteSheetAnimation(arrIntFramesIndex.Length, intFrameTime));

				// Asigna los frames
					for (int intIndex = 0; intIndex < arrIntFramesIndex.Length; intIndex++)
						objAnimation.Frames[intIndex] = arrIntFramesIndex[intIndex];
		}

		/// <summary>
		///		Crea una animación predeterminada
		/// </summary>
		public void CreateDefaultAnimation(string strAnimationKey, int intFrameTime)
		{	SpriteSheetAnimation objAnimation = SheetAnimations.Add(strAnimationKey, new SpriteSheetAnimation(Rectangles.Length, intFrameTime));

				// Crea los frames
					objAnimation.InitDefault();
		}

		/// <summary>
		///		Obtiene una animación
		/// </summary>
		public SpriteSheetAnimation SearchAnimation(string strAnimationKey)
		{ return SheetAnimations.Search(strAnimationKey);
		}

		/// <summary>
		///		Rectángulos
		/// </summary>
		public Rectangle[] Rectangles { get; }

		/// <summary>
		///		Diccionario con las animaciones del SpriteSheet
		/// </summary>
		private DictionaryContainer<SpriteSheetAnimation> SheetAnimations { get; } = new DictionaryContainer<SpriteSheetAnimation>();
	}
}
