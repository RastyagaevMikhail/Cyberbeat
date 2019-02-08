using GameCore;

using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class AuthorsScrolList : VerticalClampedScrolling<AuthorsData>
	{
		[SerializeField] TracksCollection collection;
		List<Track> tracks => collection.Objects;
		public override int panCount => tracks.Count;
		void Awake ()
		{
			Initialize ();
		}

		public override RectTransform GetPrefabInstance (int i)
		{
			RectTransform instnace = Instantiate (PrefabRect, ContentRect, false);
			new AuthorsData (tracks[i]).InitViewGameObject (instnace.gameObject);
			return instnace;
		}
	}
}
