using System.Collections.Generic;

namespace LucasSerrano.Translation
{
	/// <summary> Static class to parse CSV formatted  text into a 2D table. </summary>
	public static class CsvParser
	{
		// ---------------------------------------------------------------------

		/// <summary>
		/// Returns the content of a CSV formatted  text as a 2D string <b>list</b> with the format [line][column]. </summary>
		public static List<List<string>> ParseList(string csvContent)
		{
			List<List<string>> csvTable = new List<List<string>>();
			csvTable.Add(new List<string>());
			int currentLine = 0;

			string cell;
			int i = 0;

			// Iterate on each character of the text.
			csvContent = csvContent.Trim().Replace("\r", "");
			while (i < csvContent.Length)
			{
				cell = "";

				// If we find quote marks, the cell goes to the next quote marks.
				if (csvContent[i] == '"')
					cell += GetQuotedCell(csvContent, ref i);
				// If not, the cell goes to the next comma or \n.
				else
					cell += GetSimpleCell(csvContent, ref i);

				csvTable[currentLine].Add(cell);

				if (i < csvContent.Length && csvContent[i] == '\n')
				{
					csvTable.Add(new List<string>());
					currentLine++;
				}
				i++;    /// Add 1 to go to the character following the coma or \n.
			}

			return csvTable;
		}

		/// <summary>
		/// Returns the content of a CSV formate text as a 2D string <b>array</b> with the format [line][column]. </summary>
		public static string[][] ParseArray(string csvContent)
		{
			List<string>[] listArray = ParseList(csvContent).ToArray();

			string[][] csvTable = new string[listArray.Length][];
			for (int i = 0; i < csvTable.Length; i++)
			{
				csvTable[i] = listArray[i].ToArray();
			}

			return csvTable;
		}


		// ---------------------------------------------------------------------

		/// <summary>
		/// Returns a string that goes from csvContent[i] to the nexr comma or new line. <para></para>
		/// Moving i to the index of that comma or new line. </summary>
		private static string GetSimpleCell(string csvContent, ref int i)
		{
			string cell = "";

			while (i < csvContent.Length && csvContent[i] != ',' && csvContent[i] != '\n')
			{
				cell += csvContent[i];
				i++;
			}

			return cell;
		}

		/// <summary>
		/// Returns a string that goes from csvContent[i + 1] to the next appearance of a single quotation marks character ( " ). <para></para>
		/// Moving i to the index of the next comma or new line. </summary>
		static string GetQuotedCell(string csvContent, ref int i)
		{
			string cell = "";

			i++;
			while (i < csvContent.Length - 1)
			{
				// In a csv formatted text, quotation marks within cells appear duplicated.
				// If there are quotation marks but other ones don't appear behind them, the cell has ended.
				if (csvContent[i] == '"' && csvContent[i + 1] != '"')
				{
					i++;
					break;
				}
				else if (csvContent[i] == '"')		// If there is only one quote character, move one to only save one of them.
					i++;
				cell += csvContent[i];
				i++;
			}

			while (i < csvContent.Length && csvContent[i] != ',' && csvContent[i] != '\n')
				i++;

			return cell;
		}
	}
}
