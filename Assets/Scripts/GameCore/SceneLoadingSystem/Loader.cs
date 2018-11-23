namespace GameCore
{
	using System;

	using UnityEngine.SceneManagement;
	using UnityEngine;

	public class Loader : MonoBehaviour
	{
		[SerializeField] GameObject FullScreenHolder;
		private void Start ()
		{
			OnSceneLoaded ();
		}

		public void LoadScene (string SceneName)
		{
			FullScreenHolder.SetActive (true);
			Debug.Log ("Loader.LoadScene");
			Tools.DelayAction (this, 0.5f, () => SceneManager.LoadScene (SceneName));
		}
		public void OnSceneLoaded ()
		{
			FullScreenHolder.SetActive (false);
		}
#if UNITY_EDITOR

		private void OnApplicationQuit ()
		{
			LoadingManager.instance.nextScene = "Menu";
		}
#endif
	}
}
