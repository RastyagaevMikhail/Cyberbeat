using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[RequireComponent (typeof (SpawnedObject))]
	public class RuntimeTimer : MonoBehaviour
	{

		bool started = false;
		float ElapsedTime = 0;
		float StartTime;
		public Action OnTimeElapsed = () => { };
		public Action<float> OnTimeProgress = (t) => { };
		public Action<float> OnTimeElapse = (t) => { };
		private SpawnedObject _spawnedObj = null;
		public SpawnedObject spawnedObj { get { return _spawnedObj ?? (_spawnedObj = GetComponent<SpawnedObject> ()); } }
		public void Init (float startTime)
		{
			ElapsedTime = startTime;
			StartTime = startTime;
			started = true;
		}

		// Update is called once per frame
		void Update ()
		{
			if (started)
			{
				ElapsedTime -= Time.deltaTime;

				OnTimeElapse (ElapsedTime);
				OnTimeProgress (ElapsedTime / StartTime);

				if (ElapsedTime <= 0)
				{
					Reset ();
				}
			}

		}

		public void Reset ()
		{
			if (OnTimeElapsed != null) OnTimeElapsed ();
			started = false;
			spawnedObj.PushToPool ();
		}
	}
}
