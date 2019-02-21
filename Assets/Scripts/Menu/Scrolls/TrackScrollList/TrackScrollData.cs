using System;
using GameCore;
using UnityEngine;

namespace CyberBeat
{
	public class TrackScrollData : IDataItem
	{
		public Track track;
		public TrackScrollData (Track track)
		{
			this.track = track;
		}

        public void InitViewGameObject(GameObject go)
        {
            var cell = go.GetComponent<TrackScrollViewCell>();
			cell.UpdateContent(track);
        }
    }
}
