using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[ExecuteInEditMode]
	public class LoadingTiledGridFill : SerializedMonoBehaviour, ILoadingProgressor
	{

		[SerializeField] List<GameObject> Tiles;
		[ShowInInspector, PropertyRange (0f, 1f)]
		public float value
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

	}
}
