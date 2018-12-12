using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{

	public class OnStartOnAwakeActions : MonoBehaviour
	{
		[SerializeField, DrawWithUnity] UnityEvent OnAwake;
		[SerializeField, DrawWithUnity] UnityEvent OnStart;
		private void Awake ()
		{
			OnAwake.Invoke();
		}
		void Start ()
		{
			OnStart.Invoke();
		}

	}
}
