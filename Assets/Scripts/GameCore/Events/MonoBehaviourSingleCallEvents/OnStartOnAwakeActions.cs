using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{

	public class OnStartOnAwakeActions : MonoBehaviour
	{
		[SerializeField] UnityEvent OnAwake;
		[SerializeField] UnityEvent OnStart;
		[SerializeField] bool debug;
		private void Awake ()
		{
			OnAwake.Invoke ();
			if (debug) Debug.Log ($"{("OnAwake").a()} {name.mb()}\n{OnAwake.Log()}", this);
		}
		void Start ()
		{
			OnStart.Invoke ();
			if (debug) Debug.Log ($"{("OnStart").a()} {name.mb()}\n{OnStart.Log()}", this);
		}

	}
}
