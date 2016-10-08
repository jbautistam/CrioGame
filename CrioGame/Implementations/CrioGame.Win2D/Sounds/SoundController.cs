using System;

using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.Win2D.Sounds
{
	/// <summary>
	///		Controlador de sonido
	/// </summary>
	public class SoundController : ISoundController
	{
		internal SoundController(GameInternal.MainGame objMainGame)
		{ MainGame = objMainGame;
		}

		/// <summary>
		///		Toca un sonido
		/// </summary>
		public void Play(string strKey)
		{	
			//Content.ContentItem objContent = MainGame.ContentManager.GetSound(strKey);

			//	// Si ha localizado el sonido, lo envía
			//		if (objContent != null && objContent.Content != null)
			//			{ if (objContent.Content is SoundEffect && MainGame.Manager.GameEngine.MainManager.GameParameters.Configuration.PlayEffects)
			//					{ (objContent.Content as SoundEffect).Play();
			//					}
			//				else if (objContent.Content is Song && MainGame.Manager.GameEngine.MainManager.GameParameters.Configuration.PlayMusic)
			//					MediaPlayer.Play(objContent.Content as Song);
			//			}
		}

		/// <summary>
		///		Detiene el sonido
		/// </summary>
		public void Stop()
		{ // MediaPlayer.Stop();
		}

		/// <summary>
		///		Manager principal
		/// </summary>
		private GameInternal.MainGame MainGame { get; }
	}
}
