namespace GameCore
{

	using UnityEngine;

	public class LoadingManager : SingletonData<LoadingManager>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/LoadingManager")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate () { }
#endif

		public string nextScene = "Menu";
		Loader _loader = null;
		public Loader loader
		{
			get
			{
				if (_loader == null)
				{
					_loader = GameObject.FindObjectOfType<Loader> ();
					if (_loader == null)
					{
						_loader = Instantiate (Resources.Load<Loader> ("UI/Loader"));
					}
				}
				return _loader;
			}
		}

		public void LoadScene (string next_scene)
		{
			Debug.Log ("LoadingManager.LoadScene");
			nextScene = next_scene;

			loader.LoadScene ("Loading");
		}
	}

}
