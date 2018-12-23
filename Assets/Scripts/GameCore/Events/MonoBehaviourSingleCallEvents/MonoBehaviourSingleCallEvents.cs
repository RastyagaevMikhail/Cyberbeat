using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
	public class MonoBehaviourSingleCallEvents : MonoBehaviour
	{
		[SerializeField, DrawWithUnity] UnityEvent _OnEnable;
		private void OnEnable () { _OnEnable.Invoke (); }

		[SerializeField, DrawWithUnity] UnityEvent OnAwake;
		private void Awake () { OnAwake.Invoke (); }

		[SerializeField, DrawWithUnity] UnityEvent OnStart;
		private void Start () { OnStart.Invoke (); }

		[SerializeField, DrawWithUnity] UnityEvent _OnDisable;
		private void OnDisable () { _OnDisable.Invoke (); }

		[SerializeField, DrawWithUnity] UnityEvent _OnDestroy;
		private void OnDestroy () { _OnDestroy.Invoke (); }
	}
}
