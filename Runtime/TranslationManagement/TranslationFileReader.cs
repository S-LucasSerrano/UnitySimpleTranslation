using UnityEngine;

namespace LucasSerrano.Translation
{
	/// <summary>
	/// Base class for components that add translations to the dictionary from text assets. </summary>
	public abstract class TranslationFileReader : MonoBehaviour
	{
		[Space]
		[SerializeField] TextAsset[] files = { };


		protected virtual void Awake()
		{
			foreach(var file in files) 
			{
				ReadFile(file);
			}
		}

		protected abstract void ReadFile(TextAsset file);
	}
}