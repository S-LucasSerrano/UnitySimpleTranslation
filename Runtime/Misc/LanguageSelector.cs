using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LucasSerrano.Translation
{
	/// <summary>
	/// Component that acts as intermediary betweeen the scene and the <see cref="TranslationDictionary"/> to change the selected language. </summary>
	/// As a static class, the translation manager can not be referenced from the Unity inspector.
	/// This component allows to change the language from UI buttons and other Unity events, just set CurrentLanguage.

	[AddComponentMenu("Lucas Serrano/Translation/Language Selector")]
	public class LanguageSelector : MonoBehaviour
    {
        /// <summary> Lists of UI buttons asociated to the language they will set. </summary>
        [Space][SerializeField] List<LanguageButtom> buttons = new List<LanguageButtom>();


		// ---------------------------------------------------------------------

		private void Start()
		{
			foreach(var languageButton in buttons)
            {
                languageButton.button.onClick.AddListener(() =>
                {
                    CurrentLanguage = languageButton.language;
                });

				// TO DO: I should probably remove the listeners if this component gets destroyed,
				//		but this requires a big-ish rework of this script.
            }
		}

		/// <summary> Selected language on the <see cref="TranslationDictionary"/> </summary>
		public string CurrentLanguage
        {
            get { return TranslationDictionary.CurrentLanguage; }
            set { TranslationDictionary.CurrentLanguage = value; }
        }


		// ---------------------------------------------------------------------

		[System.Serializable]
        private class LanguageButtom
        {
            public string language = "";
            public Button button = null;
        }
    }
}
