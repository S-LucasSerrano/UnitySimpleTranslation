using UnityEngine;

namespace LucasSerrano.Translation
{
	/// <summary>
	/// Component that shows a translated text extracted from the <see cref="TranslationDictionary"/></summary>
	public abstract class TranslatedText : MonoBehaviour
	{
		[Space][SerializeField] protected string translationId = "translation-id";

		public string TranslationId
		{
			get => translationId;
			set
			{
				translationId = value;
				Translate();
			}
		}


		// ---------------------------------------------------------------------

		protected virtual void Start()
		{
			Translate();
			TranslationDictionary.AddListenerToLanguageChange(OnLanguageChanged);
		}

		protected virtual void OnDestroy()
		{
			TranslationDictionary.RemoveListenerFromLanguageChange(OnLanguageChanged);
		}

		protected virtual void OnLanguageChanged(string newLanguage)
		{
			Translate();
		}


		// ---------------------------------------------------------------------

		protected void Translate()
		{
			if (string.IsNullOrEmpty(translationId))
				return;

			var translation = TranslationDictionary.GetTranslation(translationId);
			SetText(translation);
		}

		/// <summary> Override this to use and show the translated text. </summary>
		protected abstract void SetText(string text);
	}
}