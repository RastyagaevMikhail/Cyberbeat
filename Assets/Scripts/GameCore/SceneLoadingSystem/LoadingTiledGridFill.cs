using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    [ExecuteInEditMode]
	public class LoadingTiledGridFill : MonoBehaviour, ILoadingProgressor
	{

		[SerializeField] List<GameObject> Tiles;

		public float Value
		{
			set
			{
				int count = Tiles.Count;
				int countOfLoaded = (value * count).RoundToInt ();
				for (int i = 0; i < count; i++)
					Tiles[i].SetActive (i < countOfLoaded);
			}
			get
			{
				int countOfLoaded = Tiles.Sum (t => t.activeSelf ? 1 : 0);
				return countOfLoaded / Tiles.Count;
			}
		}

#if UNITY_EDITOR
		[Range (0f, 1f)]
		public float value;
		private void Update ()
		{
			Value = value;
		}
#endif

	}
}
