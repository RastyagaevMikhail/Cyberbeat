using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace CyberBeat
{

	public class MainMenuController : SerializedMonoBehaviour //DevSkim: ignore DS184626 
	{
		private static MainMenuController _instance = null;
		public static MainMenuController instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<MainMenuController> (); _instance.Init (); return _instance; } }
		public HeaderController header { get { return HeaderController.instance; } }

		[SerializeField] Transform cameraTransform;
		[SerializeField] Sprite BlueFrame, PinkFrame;
		[SerializeField] Color Blue, Pink;
		[Button]
		public void Init ()
		{

			if (inited) return;
			inited = true;
			//TODO
			// Init TrackList
			// var trackCells = tracks
			// 	.Select (t =>
			// 	{
			// 		int index = tracks.IndexOf (t);
			// 		Sprite Frame = index % 2 == 0 ? BlueFrame : PinkFrame;
			// 		Color color = index % 2 == 0 ? Blue : Pink;
			// 		return new TrackScrollData (t, Frame, color);
			// 	})
			// 	.ToList ();

			// trackScrollList.UpdateData (trackCells);
		}
		bool inited = false;

		TracksCollection tracksCollection { get { return TracksCollection.instance; } }
		GameData gameData { get { return GameData.instance; } }
		Track track { get { return tracksCollection.CurrentTrack; } }
		List<Track> tracks { get { return tracksCollection.Objects; } }

		[SerializeField] TrackScrollList trackScrollList;
		private void Start ()
		{
			Init ();
		}

		public void ToStart ()
		{
			gameObject.SetActive (false);
		}

	}

}
