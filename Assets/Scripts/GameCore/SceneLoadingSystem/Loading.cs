using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Text = TMPro.TextMeshProUGUI;
namespace GameCore
{
	public class Loading : SerializedMonoBehaviour
	{
		[SerializeField] ILoadingProgressor loadingProgressor;
		float progress { set { if (loadingProgressor != null) loadingProgressor.value = value; } }
		LoadingManager manager { get { return LoadingManager.instance; } }
		RandomStack<string> loadingTextsRandStack;
		IEnumerator Start ()
		{

			StartCoroutine (ShowLoadingText ());
#if UNITY_EDITOR
			yield return new WaitForSeconds (5f);
#endif
			var ao = SceneManager.LoadSceneAsync (manager.nextScene);
			while (!ao.isDone)
			{

				progress = ao.progress;
				yield return null;
			}
			progress = 1f;
			yield return new WaitForSeconds (5f);
			manager.loader.OnSceneLoaded ();
			ao.allowSceneActivation = true;
		}
		public IEnumerator ShowLoadingText ()
		{
			loadingTextsRandStack = new RandomStack<string> (localizationTagsOfLoadingText);
			loadingText.text = loadingTextsRandStack.Get ().localized ();
			WaitForSeconds waitForSeconds = new WaitForSeconds (0.5f);
			while (true)
			{

				loadingDots.text = ".";
				yield return waitForSeconds;
				loadingDots.text = "..";
				yield return waitForSeconds;
				loadingDots.text = "...";
				yield return waitForSeconds;
				loadingText.text = loadingTextsRandStack.Get ().localized ();
			}
		}

		[SerializeField] Text loadingText;
		[SerializeField] Text loadingDots;

		[SerializeField] List<string> localizationTagsOfLoadingText;

#if UNITY_EDITOR
		[Button] public void ValidateTags ()
		{
			localizationTagsOfLoadingText = Localizator.GetTranslations ().ToList ().FindAll (t => t.code.Contains ("loading_text_")).Select (t => t.code).ToList ();
		}
#endif

	}
}
