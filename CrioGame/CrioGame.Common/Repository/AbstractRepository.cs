using System;
using System.Collections.Generic;

using Bau.Libraries.CrioGame.Common.Models.Resources;

namespace Bau.Libraries.CrioGame.Common.Repository
{
	/// <summary>
	///		Clase base para las clases de lectura de archivos
	/// </summary>
	public abstract class AbstractRepository
	{
		/// <summary>
		///		Comprueba si una línea es un comentario o está vacía
		/// </summary>
		protected bool IsEmpty(string strLine)
		{ return string.IsNullOrEmpty(strLine) || strLine.StartsWith("#");
		}

		/// <summary>
		///		Obtiene las líneas de un texto
		/// </summary>
		protected string [] GetLines(string strText)
		{ string [] arrStrLines;

				// Reemplaza los saltos de línea
					strText = strText.Replace('\r', ' ');
				// Obtiene las líneas
					arrStrLines = (strText?.Trim() ?? "").Split('\n');
				// Normaliza las líneas
					for (int intIndex = 0; intIndex < arrStrLines.Length; intIndex++)
						arrStrLines[intIndex] = Normalize(arrStrLines[intIndex]);
				// Devuelve las líneas
					return arrStrLines;
		}

		/// <summary>
		///		Normaliza una cadena leída
		/// </summary>
		private string Normalize(string strLine)
		{ string strTrimmed = strLine?.Trim();

				// Quita los tabuladores y los sustituye por espacios
					while (strTrimmed.IndexOf('\t') >= 0)
						strTrimmed = strTrimmed.Replace('\t', ' ');
				// Quita los espacios dobles
					while (strTrimmed.IndexOf("  ") >= 0)
						strTrimmed = strTrimmed.Replace("  ", " ");
				// Devuelve la cadena
					return strTrimmed?.Trim();
		}

		/// <summary>
		///		Convierte una cadena en un entero
		/// </summary>
		protected int ConvertInt(string strValue)
		{ int intValue;

				if (!string.IsNullOrEmpty(strValue) && int.TryParse(strValue, out intValue))
					return intValue;
				else
					return -1;
		}

		/// <summary>
		///		Obtiene una parte de la lista
		/// </summary>
		protected string GetPart(string [] arrStrParts, int intIndex)
		{ if (arrStrParts.Length > intIndex)
				return arrStrParts[intIndex]?.Trim();
			else
				return "";
		}

		/// <summary>
		///		Concatena las partes de una cadena
		/// </summary>
		protected string JoinParts(string [] arrStrParts, int intFrom)
		{ string strTotal = "";

				// Calcula la cadena concatenada
					for (int intIndex = intFrom; intIndex < arrStrParts.Length; intIndex++)
						strTotal += GetPart(arrStrParts, intIndex);
				// Devuelve la cadena total
					return strTotal;
		}
	}
}
