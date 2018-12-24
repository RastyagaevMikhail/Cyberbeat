using GameCore;

using UnityEngine;

public class Example : MonoBehaviour
{

	public Localizator localizator { get { return Localizator.instance; } }
	public void ToggleLanguage ()
	{
		if (localizator.GetLanguage () == SystemLanguage.Russian)
		{
			Localizator.LocalizatorSetEnglish ();
		}
		else
		{
			Localizator.LocalizatorSetRussian ();
		}
	}
}
