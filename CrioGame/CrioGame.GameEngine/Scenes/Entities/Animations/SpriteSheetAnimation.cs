using System;

namespace Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations
{
	/// <summary>
	///		Frames de una animación
	/// </summary>
	public class SpriteSheetAnimation
	{
		public SpriteSheetAnimation(int intFrames, int intFrameTime)
		{ FrameTime = intFrameTime;
			Frames = new int[intFrames];
		}

		/// <summary>
		///		Inicializa los frames por defecto
		/// </summary>
		public void InitDefault()
		{ for (int intIndex = 0; intIndex < Frames.Length; intIndex++)
				Frames[intIndex] = intIndex;
		}

		/// <summary>
		///		Tiempo entre frames
		/// </summary>
		public int FrameTime { get; }

		/// <summary>
		///		Frames
		/// </summary>
		public int[] Frames { get; }
	}
}
