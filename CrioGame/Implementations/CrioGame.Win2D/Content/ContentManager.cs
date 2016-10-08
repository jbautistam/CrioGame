using System;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Bau.Libraries.CrioGame.Common.Models.Contents;
using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Contents.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Contents.Sounds;
using Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine;
using Microsoft.Graphics.Canvas.Text;

namespace Bau.Libraries.CrioGame.Win2D.Content
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
		{ RootDirectory = strContentRoot;
		}

		/// <summary>
		///		Carga todo el contendio
		/// </summary>
		public void Load(AbstractContentBase objContent)
		{ if (objContent is ImageContent2D)
				LoadImageAsync(objContent as ImageContent2D);
			else if (objContent is FontContent)
				LoadFont(objContent as FontContent);
			else if (objContent is SoundContent)
				LoadSound(objContent as SoundContent);
		}

		/// <summary>
		///		Descarga el contenido
		/// </summary>
		public void Unload()
		{ // MainGame.Content.Unload();
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		private async void LoadImageAsync(ImageContent2D objImage)
		{ CanvasBitmap objTexture = await CanvasBitmap.LoadAsync(MainGame.Manager.Canvas.Device, objImage.ContentKey);

				// Inicializa los datos de la imagen
					objImage.Initialize((int) objTexture.SizeInPixels.Width, (int) objTexture.SizeInPixels.Height);
				// Añade la textura al diccionario
					AddItem(objImage.Key, objImage.ContentKey, objTexture);
		}

		/// <summary>
		///		Obtiene una imagen
		/// </summary>
		internal CanvasBitmap GetImage(string strKey)
		{ return Items.Search(strKey)?.Content as CanvasBitmap;
		}

		/// <summary>
		///		Carga una fuente
		/// </summary>
		private void LoadFont(FontContent objFont)
		{ CanvasTextFormat objCanvasFont = new CanvasTextFormat();

				// Asigna las propiedades a la fuente (si no se ha definido ninguna, recoge las predeterminadas)
					if (string.IsNullOrEmpty(objFont.Family))
						objCanvasFont.FontFamily = "Verdana";
					else
						objCanvasFont.FontFamily = objFont.Family;
					if (objFont.Size <= 0)
						objCanvasFont.FontSize = 24;
					else
						objCanvasFont.FontSize = objFont.Size;
				// Añade la fuente
					AddItem(objFont.Key, objFont.ContentKey, objCanvasFont);
		}

		/// <summary>
		///		Obtiene una fuente
		/// </summary>
		internal CanvasTextFormat GetSpriteFont(string strKey)
		{ return Items.Search(strKey)?.Content as CanvasTextFormat;
		}

		/// <summary>
		///		Carga un sonido
		/// </summary>
		private void LoadSound(SoundContent objSound)
		{ 
			//switch (objSound.Type)
			//	{	case SoundContent.SoundType.Effect:
			//				AddItem(objSound.Key, objSound.ContentKey, MainGame.Content.Load<SoundEffect>(objSound.ContentKey));
			//			break;
			//		case SoundContent.SoundType.Song:
			//				AddItem(objSound.Key, objSound.ContentKey, MainGame.Content.Load<Song>(objSound.ContentKey));
			//			break;
			//		default:
			//			throw new NotImplementedException("No se reconoce el tipo de sonido");
			//	}
		}

		/// <summary>
		///		Obtiene un sonido del diccionario
		/// </summary>
		internal ContentItem GetSound(string strKey)
		{ return Items.Search(strKey);
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

		/// <summary>
		///		Directorio raíz
		/// </summary>
		public string RootDirectory { get; internal set; }
	}
}
