using UnityEngine;
using GameCore;


public class Example : MonoBehaviour {

	public void ToggleLanguage()
	{
		if(Localizator.GetLanguage() == SystemLanguage.Russian)
		{
			Localizator.LocalizatorSetEnglish();
		} else {
			Localizator.LocalizatorSetRussian();
		}
	}
}
