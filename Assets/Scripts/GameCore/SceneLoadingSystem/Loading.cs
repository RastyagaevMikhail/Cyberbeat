using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Text = TMPro.TextMeshProUGUI;
namespace GameCore
{
	public class Loading : MonoBehaviour
	{
		[SerializeField] LoadingManager manager;
		[SerializeField] float maxTimeFromLoading = 5f;
		[SerializeField] UnityEventFloat onProgress;
		[SerializeField] UnityEventFloat onFakeLoading;
		[SerializeField] UnityEvent onLoadComplete;
		private void OnValidate ()
		{
			if (manager == null)
				manager = Resources.Load<LoadingManager> ("Data/LoadingManager");
		}
		IEnumerator Start ()
		{
			var ao = SceneManager.LoadSceneAsync (manager.nextScene);
			float startTime = Time.time;
			while (!ao.isDone)
			{
				onProgress.Invoke (ao.progress);
				yield return null;
			}
			onProgress.Invoke (1f);

			//?Fake waithing if needed
			float elapsedTime = Time.time - startTime;
			while (elapsedTime < maxTimeFromLoading)
			{
				onFakeLoading.Invoke (elapsedTime);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
			manager.loader.OnSceneLoaded ();
			onLoadComplete.Invoke ();
			ao.allowSceneActivation = true;
		}

	}
}
