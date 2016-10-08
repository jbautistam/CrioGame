using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Collections;
using Bau.Libraries.CrioGame.Common.Models.Contents;
using Bau.Libraries.CrioGame.Common.Models.Contents.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Contents.Sounds;
using Bau.Libraries.CrioGame.Common.Models.Resources;
using Bau.Libraries.CrioGame.GameEngine.Scenes.Entities.Animations;

namespace Bau.Libraries.CrioGame.GameEngine.Engine
{
	/// <summary>
	///		Controlador de contenido
	/// </summary>
	public class GameRepository : Common.Interfaces.GameEngine.IGameContentDictionary
	{
		public GameRepository(string strContentRoot)
		{ ContentRoot = strContentRoot;
		}
		
		/// <summary>
		///		Añade contenido
		/// </summary>
		public void AddContent(string strKey, AbstractContentBase objContent)
		{ Items.Add(strKey, objContent);
		}

		/// <summary>
		///		Carga una imagen
		/// </summary>
		public void AddImage(string strKey, string strContentKey)
		{ Items.Add(strKey, new ImageContent2D(strKey, strContentKey));
		}

		/// <summary>
		///		Añade una fuente
		/// </summary>
		public void AddFont(string strKey, string strContentKey)
		{ AddFont(strKey, new FontContent(strKey, strContentKey));
		}

		/// <summary>
		///		Añade una fuente
		/// </summary>
		public void AddFont(string strKey, FontContent objFont)
		{	Items.Add(strKey, objFont);
		}

		/// <summary>
		///		Añade un sonido
		/// </summary>
		public void AddSound(string strKey, string strContentKey, SoundContent.SoundType intType)
		{ Items.Add(strKey, new SoundContent(strKey, strContentKey, intType));
		}

		/// <summary>
		///		Obtiene los elementos del diccionario como una lista
		/// </summary>
		internal List<KeyValuePair<string, AbstractContentBase>> ToList()
		{ return Items.ToList();
		}

		/// <summary>
		///		Obtiene un contenido
		/// </summary>
		public AbstractContentBase GetContent(string strKey)
		{ return Items.Search(strKey);
		}

		/// <summary>
		///		Carga los recursos a partir de una cadena de texto
		/// </summary>
		public void LoadResources(string strText)
		{ LoadResources(new Common.Repository.ResourcesRepository().Load(strText));
		}

		/// <summary>
		///		Carga los recursos a partir de una colección de recursos
		/// </summary>
		public void LoadResources(List<ResourceModel> objColResources)
		{ foreach (ResourceModel objResource in objColResources)
				switch (objResource.Type)
					{	case ResourceModel.ResourceType.Image:
								AddImage(objResource.Key, objResource.Path);
							break;
						case ResourceModel.ResourceType.Font:
								AddFont(objResource.Key, objResource.Path);
							break;
						case ResourceModel.ResourceType.Song:
								AddSound(objResource.Key, objResource.Path, SoundContent.SoundType.Song);
							break;
						case ResourceModel.ResourceType.Effect:
								AddSound(objResource.Key, objResource.Path, SoundContent.SoundType.Effect);
							break;
						case ResourceModel.ResourceType.SpriteSheet:
								AddContent(objResource.Key, GetSpriteSheet(objResource));
							break;
					}
		}

		/// <summary>
		///		Obtiene un <see cref="SpriteSheetContent"/> a partir de un <see cref="ResourceModel"/>
		/// </summary>
		private SpriteSheetContent GetSpriteSheet(ResourceModel objResource)
		{ SpriteSheetContent objSpriteSheet = new SpriteSheetContent(objResource.Key, objResource.Path);

				// Añade los rectángulos
					foreach (ResourceSheetModel objSheet in objResource.Sheets)
						{ SpriteSheetFrames objFrames = objSpriteSheet.CreateSheet(objSheet.Key, objSheet.Rectangles.ToArray());

								// Añade las animaciones
									foreach (ResourceAnimationModel objAnimation in objSheet.Animations)
										if (objAnimation.Frames.Count > 0)
											objFrames.CreateAnimation(objAnimation.Key, objAnimation.Frames.ToArray(), objAnimation.FrameTime);
										else
											objFrames.CreateDefaultAnimation(objAnimation.Key, objAnimation.FrameTime);
						}
				// Devuelve el spriteSheet
					return objSpriteSheet;
		}

		/// <summary>
		///		Raíz del contenido
		/// </summary>
		public string ContentRoot { get; private set; }

		/// <summary>
		///		Diccionario de contenido
		/// </summary>
		private DictionaryContainer<AbstractContentBase> Items { get; } = new DictionaryContainer<AbstractContentBase>();
	}
}
