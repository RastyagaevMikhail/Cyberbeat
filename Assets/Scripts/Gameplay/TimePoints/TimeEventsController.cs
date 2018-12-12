using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

	public class TimeEventsController : MonoBehaviour
	{
		[SerializeField] TimeOfEventsData dataTime;
		List<TimeOfEvent> Times { get { return dataTime.Times; } }
		TimeOfEvent currentTime;
		[SerializeField] GameEvent StartEvent;
		EventListener listener;
		private void OnEnable ()
		{
			listener = new EventListener (StartEvent, () => startCountTime = true);
			listener.OnEnable ();
		}
		private void OnDisable ()
		{
			listener.OnDisable ();
		}
		private void Start ()
		{
			currentTime = Times.First ();
		}

		bool isTime;
		bool lastTime;
		private float time;
		private bool startCountTime;
		private int indexOfTime;

		public Action<bool, TimeOfEvent> OnChanged;

		void Update ()
		{
			if (!startCountTime) return;

			time += Time.deltaTime;
			if (lastTime != isTime)
			{
				lastTime = isTime;
				if (OnChanged != null) OnChanged (isTime, currentTime);
			}
			if (currentTime.Start <= time)
			{
				isTime = true;
				if (currentTime.End <= time)
				{
					isTime = false;

					indexOfTime++;
					if (indexOfTime >= Times.Count) return;
					currentTime = Times[indexOfTime];
				}
			}
		}

		public void SetStartEvent (GameEvent gameEvent)
		{
			StartEvent = gameEvent;
		}
	}

}
