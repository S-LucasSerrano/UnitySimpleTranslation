using UnityEngine;
using UnityEngine.UI;

namespace LucasSerrano.Translation
{
	/// <summary> Shows a translated text on a legacy <see cref="Text"/> component of the Unity UI. </summary>

	[AddComponentMenu("Lucas Serrano/Translation/Translated Text (Legacy)")]
	public class TranslatedTextLegacy : TranslatedText
	{
        [SerializeField] Text uiText = null;


		// ---------------------------------------------------------------------

		private void Reset()
		{
			uiText = GetComponent<Text>();
			translationId = uiText != null ? uiText.gameObject.name : translationId;
		}

		protected override void SetText(string text)
		{
			uiText.text = text;
		}
	}
}
