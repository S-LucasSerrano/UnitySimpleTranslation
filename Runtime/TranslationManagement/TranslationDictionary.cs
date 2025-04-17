using System;
using System.Collections.Generic;
using UnityEngine;

namespace LucasSerrano.Translation
{
	/* When adding entries to the dictionary have in count:
	 *		- Languages and ids are case-insensitive. [Español], [ESPAÑOL] and [español] are all the same.
			- It is sensitive to accents and other special character. [Ingles] and [Inglés] will be considered different languages.
			- Ids can only appear once across all translation files.
				But languages can appear once on each file. The language columns can be in a different order for each file.
	 */

	/// <summary>
	/// Static class containing all texts in all languages. </summary>
	public static class TranslationDictionary
	{
		/// Access each translation by doing -> translations[language][id]
		private static Dictionary<string, Dictionary<string, string>> translations = new();

		private static string _currentLanguage = "";
		private static Action<string> onLanguageChange;


		// ---------------------------------------------------------------------

		/// <summary> Add a new translation to the dictionary </summary>
		public static void AddEntry(string language, string id, string translation)
		{
			if (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(id))
				return;

			language = language.ToLower();
			id = id.ToLower();

			// Add language if it doesn't exist already.
			if (translations.ContainsKey(language) == false)
			{
				translations.Add(language, new Dictionary<string, string>());

				// The first language to be added will be the default language.
				if (string.IsNullOrEmpty(CurrentLanguage))
					CurrentLanguage = language;
			}
			// Check for duplicated ids.
			if (translations[language].ContainsKey(id) == true)
			{
				Debug.LogWarning("Translation with id [" + id + "] already exists for the language [" + language + "] in the dictionary.");
				return;
			}

			translations[language].Add(id, translation);
		}

		/// <summary> Remove all translations from the dictionary. </summary>
		public static void ClearDictionary()
		{
			translations.Clear();
			_currentLanguage = null;
		}

		// ---------

		/// <summary>
		/// Get the translation with the given id for the given language. <para></para>
		/// If target language is null, the globally selected language is used. </summary>
		public static string GetTranslation(string id, string language = null)
		{
			if (language == null)
				language = CurrentLanguage;

			id = id.ToLower();
			language = language.ToLower();

			if (translations.ContainsKey(language) == false)
				return "missing language [" + language + "]";
			if (translations[language].ContainsKey(id) == false)
				return "missing translation [" + id + "]";

			return translations[language][id];
		}

		// ---------

		/// <summary> Globally selected language. </summary>
		public static string CurrentLanguage
		{
			get
			{
				return _currentLanguage;
			}

			set
			{
				value = value.ToLower();

				if (value == _currentLanguage)
					return;

				if (translations.ContainsKey(value))
				{
					_currentLanguage = value;
					onLanguageChange?.Invoke(_currentLanguage);
				}
				else
				{
					Debug.LogError("Translation dictionary does not contain the language [" + value + "]");
				}
			}
		}

		/// <summary> Add a listener to know when the globally selected language is changed. </summary>
		public static void AddListenerToLanguageChange(Action<string> listner) => onLanguageChange += listner;

		/// <summary> Remove a listener from the events invoked when changing the globally selected language. </summary>
		public static void RemoveListenerFromLanguageChange(Action<string> listner) => onLanguageChange -= listner;
	}
}
