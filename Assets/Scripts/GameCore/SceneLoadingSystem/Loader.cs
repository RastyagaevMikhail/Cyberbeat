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
			SceneManager.LoadScene (SceneName);
		}
		public void OnSceneLoaded ()
		{
			FullScreenHolder.SetActive (false);
		}
		
	}
}
