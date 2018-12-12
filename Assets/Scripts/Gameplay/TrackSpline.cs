using FluffyUnderware.Curvy;
using FluffyUnderware.DevTools;

using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class TrackSpline : MonoBehaviour
	{
		public GameData gameData { get { return GameData.instance; } }
		float StartSpeed { get { return gameData.CurrentStartSpeed; } }
		private TrackController _traclCtrl = null;
		public TrackController traclCtrl { get { return _traclCtrl ?? (_traclCtrl = GetComponent<TrackController> ()); } }
		private CurvySpline _spline = null;
		public CurvySpline spline { get { if (_spline == null) _spline = GetComponent<CurvySpline> (); return _spline; } }
		float speed;
		[Group ("Segments")]
		[SerializeField]
		List<TrackSplineSegment> segments = null;
		public List<TrackSplineSegment> Segments
		{
			get
			{
#if UNITY_EDITOR
				ValidateSegments ();
#endif
				return segments;

			}
		}

		[ShowInInspector]
		public float LengthTimeTrackSpline { get { return segments.Select (s => s.TimeOnSegment).Sum (); } }

#if UNITY_EDITOR
		[ContextMenu ("Validate Segments")]
		[Button]
		private void ValidateSegments ()
		{
			float sum_time = 0;
			float speed = gameData.currentTrack.StartSpeed;
			segments = spline.Segments.Select (s =>
			{
				TrackSplineSegment trackSplineSegment = new TrackSplineSegment ()
					.Init_TrackSplineSegment (s, sum_time, speed);

				sum_time += trackSplineSegment.TimeOnSegment;
				speed = trackSplineSegment.EndSpeed;
				return trackSplineSegment;
			}).ToList ();
			// }
		}

#endif
		public Vector3 GetPosByTrackTime (float time)
		{
			// time = convertToTrackTime (time);
			Vector3 result = default (Vector3);
			// Tools.LogCollection (Segments);
			foreach (var segment in Segments)
			{
				if (segment.IsTrackTime (time))
				{
					// Точка положения бита
					result = segment.InterpolateByTrackTime (time);
					break;
				}
			}
			return result;
		}
		public float TFByTrackTime (float time)
		{
			// time = convertToTrackTime (time);
			foreach (var segment in Segments)
				if (segment.IsTrackTime (time))
				{
					return segment.TFByTrackTime (time);
				}
			return 0;
		}

		private float convertToTrackTime (float time)
		{
			float length = gameData.currentTrack.music.clip.length;
			var timePercent = time / LengthTimeTrackSpline;

			return timePercent * length;
		}

		public List<Vector3> GetSegemntPositionsByRangeTime (float startTime, float endTime)
		{
			return GetSegemntsByRangeTime (startTime, endTime).Select (s => s.position).ToList ();
		}
		public List<CurvySplineSegment> GetSegemntsByRangeTime (float startTime, float endTime)
		{
			List<CurvySplineSegment> result = new List<CurvySplineSegment> ();
			float sum_time = 0;
			speed = StartSpeed;
			// Tools.LogCollection (Segments);
			foreach (var segment in Segments)
			{
				bool segmentInRange = sum_time.InRange (startTime, endTime);
				Debug.LogFormat ("{0}<={1}<={2}  is {3}", startTime, sum_time, endTime, segmentInRange);
				if (segmentInRange)
				{
					result.Add (segment.segment);
				}
				sum_time += segment.TimeOnSegment;
				speed = segment.EndSpeed;
			}
			return result;
		}

	}
}
