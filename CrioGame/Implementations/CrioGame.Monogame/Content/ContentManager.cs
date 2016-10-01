using System;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Bau.Libraries.CrioGame.Common.Models.Contents;
using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Contents.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Contents.Sounds;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;

namespace Bau.Libraries.CrioGame.MonogameImpl.Content
{
	/// <summary>
	///		Controlador de contenido
	/// </summary>
	internal class ContentManager : IContentManager
	{
		internal ContentManager(GameInternal.MainGame objGame)
		{ MainGame = objGame;
		}

		/// <summary>
		///		Inicializa el controlador de contenido
		/// </summary>
		public void Initialize(string strContentRoot)
		{ MainGame.Content.RootDirectory = strContentRoot;
		}

		/// <summary>
		///		Carga todo el contendio
		/// </summary>
		public void Load(AbstractContentBase objContent)
		{ if (objContent is ImageContent2D)
				LoadImage(objContent as ImageContent2D);
			else if (objContent is FontContent)
				LoadFont(objContent as FontContent);
			else if (objContent is SoundContent)
				LoadSound(objContent as SoundContent);
		}

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		public void Unload()
		{ MainGame.Content.Unload();
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		private void LoadImage(ImageContent2D objImage)
		{ Texture2D objTexture = MainGame.Content.Load<Texture2D>(objImage.ContentKey);

				// Inicializa los datos de la imagen
					objImage.Initialize(objTexture.Width, objTexture.Height);
				// Añade la textura al diccionario
					AddItem(objImage.Key, objImage.ContentKey, objTexture);
		}

		/// <summary>
		///		Obtiene una imagen
		/// </summary>
		internal Texture2D GetImage(string strKey)
		{ return Items.Search(strKey)?.Content as Texture2D;
		}

		/// <summary>
		///		Obtiene una fuente
		/// </summary>
		internal SpriteFont GetSpriteFont(string strKey)
		{ return Items.Search(strKey)?.Content as SpriteFont;
		}

		/// <summary>
		///		Obtiene un sonido del diccionario
		/// </summary>
		internal ContentItem GetSound(string strKey)
		{ return Items.Search(strKey);
		}

		/// <summary>
		///		Carga una fuente
		/// </summary>
		private void LoadFont(FontContent objFont)
		{ AddItem(objFont.Key, objFont.ContentKey, MainGame.Content.Load<SpriteFont>(objFont.ContentKey));
		}

		/// <summary>
		///		Carga un sonido
		/// </summary>
		private void LoadSound(SoundContent objSound)
		{ switch (objSound.Type)
				{	case SoundContent.SoundType.Effect:
							AddItem(objSound.Key, objSound.ContentKey, MainGame.Content.Load<SoundEffect>(objSound.ContentKey));
						break;
					case SoundContent.SoundType.Song:
							AddItem(objSound.Key, objSound.ContentKey, MainGame.Content.Load<Song>(objSound.ContentKey));
						break;
					default:
						throw new NotImplementedException("No se reconoce el tipo de sonido");
				}
		}

		/// <summary>
		///		Añade el elemento al diccionario
		/// </summary>
		private void AddItem(string strKey, string strContentKey, object objContent)
		{ Items.Add(strKey, new ContentItem(strContentKey, objContent));
		}

		/// <summary>
		///		Controlador principal del juego
		/// </summary>
		private GameInternal.MainGame MainGame { get; }

		/// <summary>
		///		Elementos añadidos en la colección
		/// </summary>
		private DictionaryContainer<ContentItem> Items { get; } = new DictionaryContainer<ContentItem>();
	}
}
