using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[ExecuteInEditMode]
	public class LoadingTiledGridFill : ALoadingProgressor
	{

		[SerializeField] List<GameObject> Tiles;

		public override float Value
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
				float countOfLoaded = Tiles.Sum (t => t.activeSelf ? 1f : 0f);
				return countOfLoaded / (float) Tiles.Count;
			}
		}

#if UNITY_EDITOR
		[Range (0f, 1f)]
		public float value;
		private void Update ()
		{
			if (!Application.isPlaying)
				Value = value;
		}
#endif

	}
}
