using System.Xml;
using UnityEngine;

// >> XML structure:
/*		<?xml ...?>
 *		<root>
 *			<id1>
 *				<language1> (...) </language1>
 *				<language2> (...) </language2>
 *			</id1> 
 *			<id2>
 *				<language1> (...) </language1>
 *				<language2> (...) </language2>
 *			</id2>
 *		</root>		*/

namespace LucasSerrano.Translation
{
	/// <summary>
	/// Add translations to the dictionary from XML files. </summary>

	[AddComponentMenu("Lucas Serrano/Translation/XML Translation Reader")]
	public class XmlTranslationReader : TranslationFileReader
	{
		// ---------------------------------------------------------------------

		protected override void ReadFile(TextAsset file)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(file.text);

			foreach (XmlNode parentNode in xmlDoc.DocumentElement)
			{
				foreach (XmlNode childNode in parentNode.ChildNodes)
				{
					var id = parentNode.Name;
					var language = childNode.Name;
					var translation = childNode.InnerText;

					TranslationDictionary.AddEntry(language, id, translation);
				}
			}
		}

		// ---------------------------------------------------------------------
	}
}
