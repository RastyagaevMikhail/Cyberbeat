using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

	public class SetActiveInTime : MonoBehaviour
	{
		[SerializeField] GameObject Target;

		public void _InTime (TimeEvent timeEvent)
		{
			Target.SetActive(timeEvent.isTime);
		}
		public void _InTime (bool isTime)
		{
			Target.SetActive(isTime);
		}

	}
}
