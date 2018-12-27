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
		private void Awake ()
		{
			OnAwake.Invoke ();
		}
		void Start ()
		{
			OnStart.Invoke ();
		}

	}
}
