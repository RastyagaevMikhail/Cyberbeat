using GameCore;

using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class TrackVerticalList : VerticalClampedScrolling<TrackScrollData>
    {
        [SerializeField] TracksCollection collection;
        List<Track> tracks => collection.Objects;
        public override int panCount => tracks.Count;
        public override RectTransform GetPrefabInstance (int i)
        {
            var instance = Instantiate (PrefabRect, contentRect, false);
            var trackinfo = new TrackScrollData (tracks[i]);
            trackinfo.InitViewGameObject (instance.gameObject);
            return instance;
        }

        private void Awake ()
        {
            Initialize ();
        }
    }
}
