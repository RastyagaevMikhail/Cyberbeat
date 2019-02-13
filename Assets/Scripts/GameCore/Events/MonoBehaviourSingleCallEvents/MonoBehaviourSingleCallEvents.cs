using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
	public class MonoBehaviourSingleCallEvents : MonoBehaviour
	{
		[SerializeField] bool invokeOnEnable = true;
		[SerializeField] UnityEvent _OnEnable;
		private void OnEnable () { if (invokeOnEnable) _OnEnable.Invoke (); }

		[SerializeField] bool invokeOnAwake = true;
		[SerializeField] UnityEvent OnAwake;
		private void Awake () { if (invokeOnAwake) OnAwake.Invoke (); }

		[SerializeField] bool invokeOnStart = true;
		[SerializeField] UnityEvent OnStart;
		private void Start () { if (invokeOnStart) OnStart.Invoke (); }

		[SerializeField] bool invokeOnDisable = true;
		[SerializeField] UnityEvent _OnDisable;
		private void OnDisable () { if (invokeOnDisable) _OnDisable.Invoke (); }

		[SerializeField] bool invokeOnDestroy = true;
		[SerializeField] UnityEvent _OnDestroy;
		private void OnDestroy () { if (invokeOnDestroy) _OnDestroy.Invoke (); }
	}
}
