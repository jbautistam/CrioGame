using System;
using System.Collections.Generic;

using ArkanoidGame.Model.Entities;
using Bau.Libraries.CrioGame.Common.Interfaces.GameEngine;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace ArkanoidGame.Repository
{
	/// <summary>
	///		Clase para lectura de datos de los niveles
	/// </summary>
	internal class LevelsRepository : Bau.Libraries.CrioGame.Common.Repository.AbstractRepository
	{
		/// <summary>
		///		Carga los ladrillos de la escena
		/// </summary>
		internal List<BrickModel> LoadBricks(IView objView, IGameContext objContext, int intLevel, TimeSpan tsBall)
		{ List<BrickModel> objColBricks = new List<BrickModel>();
			string strFileName = System.IO.Path.Combine(Program.PathData, $"Level_{intLevel}.txt");
			bool blnLoaded = false;
			
				// Carga el archivo
					if (System.IO.File.Exists(strFileName))
						try
							{ string [] arrStrLines = GetLines(System.IO.File.ReadAllText(strFileName));

									// Interpreta las líneas
										foreach (string strLine in arrStrLines)
											if (!IsEmpty(strLine))
												{ string [] arrStrParts = strLine.Trim().Split(';');
													int intTop, intLeft;
													BrickModel.BrickType intType;
													PillModel.PillType intPill;

														// Obtiene las partes
															intTop = ConvertInt(GetPart(arrStrParts, 0));
															intLeft = ConvertInt(GetPart(arrStrParts, 1));
															intType = ConvertToBrickType(GetPart(arrStrParts, 2));
															intPill = ConvertToPillMode(GetPart(arrStrParts, 3));
														// Añade el ladrillo a la escena
															if (intTop >= 0 && intLeft >= 0)
																{ // Obtiene un valor aleatorio para la píldora asociada
																		if (intPill == PillModel.PillType.None)
																			intPill = (PillModel.PillType) objContext.MathHelper.Random(((int) PillModel.PillType.None) + 1);
																	// Añade el ladrillo
																		objColBricks.Add(new BrickModel(objView, new GameObjectDimensions(intLeft, intTop), 
																																			intType, intPill, tsBall));
																	// Indica que se ha cargado algo correctamente
																		blnLoaded = true;
																}
												}
							}
						catch {}
				// Si no se ha cargado nada, se carga el contenido predeterminado
					if (!blnLoaded)
						objColBricks = LoadBricksDefault(objView, objContext, tsBall);
				// Devuelve la colección de ladrillos
					return objColBricks;
		}

		/// <summary>
		///		Carga los ladrillos predeterminados
		/// </summary>
		private List<BrickModel> LoadBricksDefault(IView objView, IGameContext objContext, TimeSpan tsBall)
		{	List<BrickModel> objColBricks = new List<BrickModel>();
			int intTop = 10;

				// Crea ladrillos por filas
					for (int intRow = 0; intRow < 3; intRow ++)
						{ int intLeft = 10;

								// Crea los ladrillos por columnas
									for (int intColumn = 0; intColumn < 13; intColumn++)
										{	// Crea el ladrillo
												objColBricks.Add(new BrickModel(objView, new GameObjectDimensions(intLeft, intTop), 
																												GetBrickType(intRow, intColumn), 
																												(PillModel.PillType) objContext.MathHelper.Random(((int) PillModel.PillType.None) + 1),
																												tsBall));
											// Pasa a la siguiente coordenada
												intLeft += 48;
										}
								// Pasa a la siguiente fila
									intTop += 26;
						}
				// Devuelve la colección de ladrillos
					return objColBricks;
		}

		/// <summary>
		///		Obtiene el tipo de píldora asociado en la línea
		/// </summary>
		private PillModel.PillType ConvertToPillMode(string strValue)
		{ // Busca el valor en el enumerado
				foreach (string strName in Enum.GetNames(typeof(BrickModel.BrickType)))
					if (strName.Equals(strValue, StringComparison.CurrentCultureIgnoreCase))
						return (PillModel.PillType) Enum.Parse(typeof(BrickModel.BrickType), strValue);
			// Devuelve el tipo predeterminado
				return PillModel.PillType.None;
		}

		/// <summary>
		///		Obtiene el tipo de ladrillo asociado en la línea
		/// </summary>
		private BrickModel.BrickType ConvertToBrickType(string strValue)
		{ // Busca el valor en el enumerado
				foreach (string strName in Enum.GetNames(typeof(BrickModel.BrickType)))
					if (strName.Equals(strValue, StringComparison.CurrentCultureIgnoreCase))
						return (BrickModel.BrickType) Enum.Parse(typeof(BrickModel.BrickType), strValue);
			// Devuelve el tipo predeterminado
				return BrickModel.BrickType.White;
		}

		/// <summary>
		///		Obtiene el tipo de ladrillo
		/// </summary>
		private BrickModel.BrickType GetBrickType(int intRow, int intColumn)
		{ return (BrickModel.BrickType) ((intRow * intColumn + intColumn) % (int) BrickModel.BrickType.Yellow);
		}
	}
}
