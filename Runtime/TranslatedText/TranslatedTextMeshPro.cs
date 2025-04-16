using UnityEngine;
using TMPro;

namespace LucasSerrano.Translation
{
	/// <summary> Shows a translated text on a Unity TextMeshPro. </summary>

	[AddComponentMenu("Lucas Serrano/Translation/Translated Text (TMP)")]
	public class TranslatedTextMeshPro : TranslatedText
	{
		[SerializeField] TextMeshProUGUI uiText = null;


		// ---------------------------------------------------------------------

		private void Reset()
		{
			uiText = GetComponent<TextMeshProUGUI>();
			translationId = uiText != null ? uiText.gameObject.name : translationId;
		}

		protected override void SetText(string text)
		{
			uiText.text = text;
		}
	}
}