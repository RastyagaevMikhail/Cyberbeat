using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class GameEventListernerInt : MonoBehaviour
	{

		[SerializeField] EventListenerInt listener;

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
