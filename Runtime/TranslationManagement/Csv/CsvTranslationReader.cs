using System.Collections.Generic;
using UnityEngine;

// >> CSV file strcture:
/*		|     | language1 | language2 |
 *		| id1 | (...)     | (...)     |
 *		| id2 | (...)     | (...)     | */

namespace LucasSerrano.Translation
{
	/// <summary>
	/// Add translation to the dictionary from CSV files. </summary>

	[AddComponentMenu("Lucas Serrano/Translation/CSV Translation Reader")]
	public class CsvTranslationReader : TranslationFileReader
	{
		// ---------------------------------------------------------------------

		protected override void ReadFile(TextAsset file)
		{
			// Check each cell of the table except the first line and column.
			List<List<string>> csvTable = CsvParser.ParseList(file.text);
			for (int line = 1; line < csvTable.Count; line++)
			{
				for (int column = 1; column < csvTable[line].Count; column++)
				{
					var id = csvTable[line][0];					// The first column contains the id.
					var language = csvTable[0][column];			// The first line contains the language.
					var translation = csvTable[line][column];	// Whatever is in the cell is the translation for that id in that language.

					TranslationDictionary.AddEntry(language, id, translation);
				}
			}
		}

		// ---------------------------------------------------------------------
	}
}
