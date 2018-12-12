using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	using Sirenix.OdinInspector.Editor;
	using Sirenix.OdinInspector;

	using System;

	using UnityEditor;

	using UnityEngine;

	public class TimeEventsDataCreationWindow : OdinEditorWindow
	{

		[MenuItem ("Game/Windows/Generations/TimeEventsDataCreationWindow")]
		private static void ShowWindow ()
		{
			TimeEventsDataCreationWindow window = GetWindow<TimeEventsDataCreationWindow> ();
			window.Show ();
			window.Init (LayerType.Line, TracksCollection.instance.CurrentTrack);
		}

		private void Init (LayerType line, Track currentTrack)
		{
			layer = line;
			track = currentTrack;
		}

		[SerializeField] Track track;
		[SerializeField] LayerType layer;

		[Button]
		void Create ()
		{

			var timePointsData = ScriptableObject.CreateInstance<TimePointsData> ();

			string path = "Assets/Data/TimePoints/{0}/{1}.asset".AsFormat (track.name, layer);

			timePointsData.CreateAsset (path);

			var events = track.GetLongEventsByType (layer);

			var timeEvent = ScriptableObject.CreateInstance<TimeOfEventsData> ();

			timeEvent.Init (track.koreography.SampleRate, events, timePointsData);

			path = "Assets/Data/TimeEvents/{0}/{1}.asset".AsFormat (track.name, layer);

			timeEvent.CreateAsset (path);

		}
	}
}
