using System;

using UnityEngine;

namespace CyberBeat
{
	public class TrackScrollData
	{

		public Track track;
		public Sprite Frame;
		public Color color;
		public TrackScrollData (Track track, Sprite frame = null, Color color = default (Color))
		{
			this.track = track;
			Frame = frame;
			this.color = color;

		}

	}
}
