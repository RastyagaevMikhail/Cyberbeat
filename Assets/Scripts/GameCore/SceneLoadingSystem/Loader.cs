namespace GameCore
{
	using System;

	using UnityEngine.SceneManagement;
	using UnityEngine;

	public class Loader : MonoBehaviour
	{
#if UNITY_EDITOR
		private void OnApplicationQuit ()
		{
			LoadingManager.instance.nextScene = "Menu";
		}
#endif
		[SerializeField] GameObject FullScreenHolder;
		private void Start ()
		{
			OnSceneLoaded ();
		}

		public void LoadScene (string SceneName)
		{
			FullScreenHolder.SetActive (true);
			Debug.Log ($"Loader.LoadScene {SceneName}");
			// Tools.DelayAction (this, 0.5f, () => SceneManager.LoadScene (SceneName));
			SceneManager.LoadScene (SceneName);
		}
		public void OnSceneLoaded ()
		{
			FullScreenHolder.SetActive (false);
		}
		public void ReloadScene ()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
