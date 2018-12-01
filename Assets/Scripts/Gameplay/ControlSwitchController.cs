using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (Player))]
	public class ControlSwitchController : MonoBehaviour
	{

		private Player _player = null;
		public Player player { get { if (_player == null) _player = GetComponent<Player> (); return _player; } }
		public bool startCountTime { get; set; }

		public static Action<InputControlType> OnSwitchControl;

		[SerializeField] public float time = 0;
		[SerializeField] TimeOfEvent currentTime;
		[SerializeField] int currentTimeIndex = 0;
		[SerializeField] bool isTime;
		[SerializeField] bool lastTime;
		List<TimeOfEvent> Times { get { return GameData.instance.currentLines; } }
		private void Start ()
		{
			currentTime = Times.First ();
		}

		void Update ()
		{
			if (!startCountTime) return;

			time += Time.deltaTime;
			if (lastTime != isTime)
			{
				lastTime = isTime;
				InputControlType controlTypeToSwitch = isTime ? InputControlType.Center : InputControlType.Side;
				player.SetControl (controlTypeToSwitch);
				if (OnSwitchControl != null) OnSwitchControl (controlTypeToSwitch);
			}
			if (currentTime.Start <= time)
			{
				// Debug.LogFormat ("currentTime = [{0}]", currentTime);
				// Debug.LogFormat ("StartTime {0} time = {1}", currentTime.StartTime, time);
				isTime = true;
				if (currentTime.End <= time)
				{
					isTime = false;

					currentTimeIndex++;
					if (currentTimeIndex >= Times.Count) return;
					currentTime = Times[currentTimeIndex];
				}
			}

		}
	}
}
