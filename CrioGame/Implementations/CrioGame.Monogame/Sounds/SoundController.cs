using System;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl.Sounds
{
	/// <summary>
	///		Controlador de sonido
	/// </summary>
	public class SoundController : ISoundController
	{
		internal SoundController(GameInternal.MainGame objMainGame)
		{ MainGame = objMainGame;
		}

// private SoundEffect explosionSound;

// private SoundEffectInstance explosionSoundInstance;
// // Load the laserSound Effect and create the effect Instance
// laserSound = Content.Load<SoundEffect>("Sounds\\laserFire");
// laserSoundInstance = laserSound.CreateInstance();
// // Load the laserSound Effect and create the effect Instance
//explosionSound = Content.Load<SoundEffect>("Sounds\\explosion");

//explosionSoundInstance = explosionSound.CreateInstance();

//explosionSoundInstance.Dispose();

//explosionSound.Play();

//  // Game Music.
//private Song gameMusic;
//// Load the game music
//gameMusic = Content.Load<Song>("Sounds\\gameMusic");

// Start playing the music.
 //MediaPlayer.Play(gameMusic);

 // MediaPlayer.Stop();

		/// <summary>
		///		Toca un sonido
		/// </summary>
		public void Play(string strKey)
		{	Content.ContentItem objContent = MainGame.ContentManager.GetSound(strKey);

				// Si ha localizado el sonido, lo envía
					if (objContent != null && objContent.Content != null)
						{ if (objContent.Content is SoundEffect && MainGame.Manager.GameEngine.MainManager.GameParameters.Configuration.PlayEffects)
								{ (objContent.Content as SoundEffect).Play();
								}
							else if (objContent.Content is Song && MainGame.Manager.GameEngine.MainManager.GameParameters.Configuration.PlayMusic)
								MediaPlayer.Play(objContent.Content as Song);
						}
		}

		/// <summary>
		///		Detiene el sonido
		/// </summary>
		public void Stop()
		{ MediaPlayer.Stop();
		}

		/// <summary>
		///		Manager principal
		/// </summary>
		private GameInternal.MainGame MainGame { get; }
	}
}
