using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GameUIControntoller : MonoBehaviour
	{
		[SerializeField] GameObject ActivateGateButton;
		[SerializeField] IntVariable currentCombo;
		public Track track { get { return TracksCollection.instance.CurrentTrack; } }

		public void _OnZoneGateActive (bool isZone)
		{
			bool activateGate = !track.GetGateState (currentCombo.Value) && isZone;

			ActivateGateButton.SetActive (activateGate);
		}
	}
}
