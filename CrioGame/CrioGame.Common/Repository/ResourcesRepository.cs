using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Resources;

namespace Bau.Libraries.CrioGame.Common.Repository
{
	/// <summary>
	///		Clase de repositorio de recursos
	/// </summary>
	public class ResourcesRepository : AbstractRepository
	{
		/// <summary>
		///		Carga los recursos de un texto
		/// </summary>
		public List<ResourceModel> Load(string strText)
		{ List<ResourceModel> objColResources = new List<ResourceModel>();
			
				// Carga el archivo
					if (!string.IsNullOrEmpty(strText))
						{ string [] arrStrLines = GetLines(strText);
							int intLine = 0;

								while (intLine < arrStrLines.Length)
									{ string strTrimmed = arrStrLines[intLine];

											if (IsEmpty(arrStrLines[intLine]))
												intLine++;
											else
												{ string [] arrStrParts;
													ResourceModel.ResourceType? intType;
													string strKey;

														// Separa los datos
															arrStrParts = strTrimmed.Split(' ');
														// Obtiene el tipo y la clave
															intType = ConvertResourceType(GetPart(arrStrParts, 0));
															strKey = GetPart(arrStrParts, 1);
														// Obtiene el recurso
															if (intType == null || string.IsNullOrEmpty(strKey))
																intLine++;
															else
																switch (intType)
																	{ case ResourceModel.ResourceType.Image:
																		case ResourceModel.ResourceType.Song:
																		case ResourceModel.ResourceType.Effect:
																		case ResourceModel.ResourceType.Font:
																				// Añade el recurso
																					objColResources.Add(new ResourceModel(intType ?? ResourceModel.ResourceType.Image,
																																								strKey, JoinParts(arrStrParts, 2)));
																				// Incrementa la línea
																					intLine++;
																			break;
																		case ResourceModel.ResourceType.SpriteSheet:
																				ResourceModel objSpriteSheet = new ResourceModel(ResourceModel.ResourceType.SpriteSheet, strKey, JoinParts(arrStrParts, 2));

																					// Lee el contenido del spriteSheet
																						intLine++;
																						LoadSpriteSheet(objSpriteSheet, arrStrLines, ref intLine);
																					// Añade el recurso
																						objColResources.Add(objSpriteSheet);
																			break;
																		default: // ... nos saltamos las líneas desconocidas
																				intLine++;
																			break;
																	}
												}
									}
						}
				// Devuelve la colección de recursos
					return objColResources;
		}

		/// <summary>
		///		Carga el contenido del spriteSheet
		/// </summary>
		private void LoadSpriteSheet(ResourceModel objSpriteSheet, string [] arrStrLines, ref int intLine)
		{ bool blnReadSheet = true;

				// Lee los sheets
					while (blnReadSheet && intLine < arrStrLines.Length)
						{ string [] arrStrParts = arrStrLines[intLine].Split(' ');
			
								// Indica que (hasta ahora) no se ha leído ningún sheet
									blnReadSheet = false;
								// Lee el sheet
									if (GetPart(arrStrParts, 0).Equals("Sheet", StringComparison.CurrentCultureIgnoreCase))
										{ ResourceSheetModel objSheet = new ResourceSheetModel(GetPart(arrStrParts, 1));
											bool blnReadContent = true;

												// Indica que se ha leído una clave Sheet
													blnReadSheet = true;
													intLine++;
												// Lee los rectángulos
													while (blnReadContent && intLine < arrStrLines.Length)
														{ string strNextLine = arrStrLines[intLine];
															string [] arrStrNextLine = strNextLine.Split(' ');

																// Indica que hasta ahora no se ha leído contenido
																	blnReadContent = false;										
																// Lee un rectángulo
																	if (GetPart(arrStrNextLine, 0).Equals("Rectangle", StringComparison.CurrentCultureIgnoreCase))
																		{ int intLeft = ConvertInt(GetPart(arrStrNextLine, 1));
																			int intTop = ConvertInt(GetPart(arrStrNextLine, 2));
																			int intWidth = ConvertInt(GetPart(arrStrNextLine, 3));
																			int intHeight = ConvertInt(GetPart(arrStrNextLine, 4));

																				// Añade el rectángulo si se ha escrito correctamente
																					if (intTop >= 0 && intLeft >= 0 && intWidth >= 0 && intHeight >= 0)
																						objSheet.Rectangles.Add(new Models.Structs.Rectangle(intLeft, intTop, intWidth, intHeight));
																				// De cualquier forma, se debe indicar que se ha leído una línea
																					blnReadContent = true;
																		}
																	else if (GetPart(arrStrNextLine, 0).Equals("Animation", StringComparison.CurrentCultureIgnoreCase))
																		{ string strKey = GetPart(arrStrNextLine, 1);
																			int intFrameTime = ConvertInt(GetPart(arrStrNextLine, 2));

																				// Crea la animación
																					if (!string.IsNullOrEmpty(strKey))
																						{ ResourceAnimationModel objAnimation = new ResourceAnimationModel(strKey);

																								// Normaliza el ratio de frames
																									if (intFrameTime < 1)
																										intFrameTime = 30;
																								// Asigna el ratio de framse
																									objAnimation.FrameTime = intFrameTime;
																								// Añade el resto de partes
																									for (int intFrame = 3; intFrame < arrStrNextLine.Length; intFrame++)
																										{ int intValue = ConvertInt(GetPart(arrStrNextLine, intFrame));

																												if (intValue >= 0)
																													objAnimation.Frames.Add(intValue);
																										}
																								// y añade la animación a la colección
																									objSheet.Animations.Add(objAnimation);
																						}
																				// De cualquier forma, indica que se ha leído la línea
																					blnReadContent = true;
																		}
																// Incrementa el número de línea si se ha tratado
																	if (blnReadContent)
																		intLine++;	
														}
												// Añade la hoja al recurso
													objSpriteSheet.Sheets.Add(objSheet);
										}
					}
		}

		/// <summary>
		///		Obtiene el tipo de recurso
		/// </summary>
		private ResourceModel.ResourceType? ConvertResourceType(string strResource)
		{ // Busca el valor en el enumerado
				foreach (string strName in Enum.GetNames(typeof(ResourceModel.ResourceType)))
					if (strName.Equals(strResource, StringComparison.CurrentCultureIgnoreCase))
						return (ResourceModel.ResourceType) Enum.Parse(typeof(ResourceModel.ResourceType), strResource);
			// Devuelve el tipo predeterminado
				return null;
		}
	}
}
