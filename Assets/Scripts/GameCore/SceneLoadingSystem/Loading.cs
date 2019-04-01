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
		[SerializeField] float maxTimeFromLoading = 5f;
		[SerializeField] GameObject back;
		[SerializeField] UnityEventFloat onProgress;
		[SerializeField] UnityEventFloat onFakeLoading;
		[SerializeField] UnityEvent onLoadComplete;
		[SerializeField] bool debug;

		public void LoadScene (string sceneName)
		{
			back.SetActive (true);
			StartCoroutine (cr_LoadScene (sceneName));
		}
		IEnumerator cr_LoadScene (string sceneName)
		{
			if (debug) Debug.Log ($"Loading.LoadScene(\"{sceneName}\")");
			var ao = SceneManager.LoadSceneAsync (sceneName);
			float startTime = Time.time;
			if (debug) Debug.LogFormat ("startTime = {0}", startTime);
			while (!ao.isDone)
			{
				if (debug) Debug.LogFormat ("ao.progress = {0}", ao.progress);
				onProgress.Invoke (ao.progress);
				yield return null;
			}
			onProgress.Invoke (1f);

			//?---Fake waithing if needed
			float elapsedTime = Time.time - startTime;
			if (debug)
			{
				Debug.LogFormat ("elapsedTime = {0}", elapsedTime);
				Debug.LogFormat ("maxTimeFromLoading = {0}", maxTimeFromLoading);
			}
			while (elapsedTime < maxTimeFromLoading)
			{
				onFakeLoading.Invoke (elapsedTime);
				elapsedTime += Time.deltaTime;
				if (debug) Debug.LogFormat ("elapsedTime = {0}", elapsedTime);

				yield return new WaitForEndOfFrame ();
			}
			onLoadComplete.Invoke ();
			ao.allowSceneActivation = true;
			if (debug) Debug.LogFormat ("ao.allowSceneActivation = {0}", ao.allowSceneActivation);
		}

	}
}
