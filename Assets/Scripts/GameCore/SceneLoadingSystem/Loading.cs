using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Text = TMPro.TextMeshProUGUI;
namespace GameCore
{
	public class Loading : MonoBehaviour
	{
		[SerializeField] ILoadingProgressor loadingProgressor;
		float progress { set { if (loadingProgressor != null) loadingProgressor.Value = value; } get { return loadingProgressor.Value; } }
		LoadingManager manager { get { return LoadingManager.instance; } }
		RandomStack<string> loadingTextsRandStack;
		[SerializeField] float fakePercent;
		[SerializeField] float fakePercentStep = 0.1f;
		IEnumerator Start ()
		{

			StartCoroutine (ShowLoadingText ());
#if UNITY_EDITOR
			yield return new WaitForSeconds (5f);
#endif
			var ao = SceneManager.LoadSceneAsync (manager.nextScene);
			while (!ao.isDone)
			{
				progress = ao.progress.GetAsClamped (fakePercent, 1);
				yield return null;
			}
			progress = 1f;
			// yield return new WaitForSeconds (5f);
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
				AddFakePercent ();
				loadingDots.text = "..";
				yield return waitForSeconds;
				AddFakePercent ();
				loadingDots.text = "...";
				yield return waitForSeconds;
				AddFakePercent ();
				loadingText.text = loadingTextsRandStack.Get ().localized ();
			}
		}

		private void AddFakePercent ()
		{
			fakePercent += fakePercentStep;
			progress = fakePercent;
		}

		[SerializeField] Text loadingText;
		[SerializeField] Text loadingDots;

		[SerializeField] List<string> localizationTagsOfLoadingText;

#if UNITY_EDITOR
		[ContextMenu ("Validate Tags")] public void ValidateTags ()
		{
			localizationTagsOfLoadingText = LocalizationManager.instance.Keys.Keys.ToList ().FindAll (key => key.Contains ("loading_text_"));
		}
#endif

	}
}
