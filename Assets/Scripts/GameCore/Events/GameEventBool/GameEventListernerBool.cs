using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class GameEventListernerBool : MonoBehaviour
	{

		[SerializeField] EventListenerBool listener;

		private void OnEnable ()
		{
			if (!listener.OnEnable ())
			{
				Debug.LogError ("Event not set On listener", this);
			}
		}

		private void OnDisable ()
		{
			if (!listener.OnDisable ())
			{
				Debug.LogError ("Event not set On listener", this);
			}
		}
	}
}
