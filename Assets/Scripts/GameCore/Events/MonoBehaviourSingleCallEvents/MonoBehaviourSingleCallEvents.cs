
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
	public class MonoBehaviourSingleCallEvents : MonoBehaviour
	{
		[SerializeField] UnityEvent _OnEnable;
		private void OnEnable () { _OnEnable.Invoke (); }

		[SerializeField] UnityEvent OnAwake;
		private void Awake () { OnAwake.Invoke (); }

		[SerializeField] UnityEvent OnStart;
		private void Start () { OnStart.Invoke (); }

		[SerializeField] UnityEvent _OnDisable;
		private void OnDisable () { _OnDisable.Invoke (); }

		[SerializeField] UnityEvent _OnDestroy;
		private void OnDestroy () { _OnDestroy.Invoke (); }
	}
}
