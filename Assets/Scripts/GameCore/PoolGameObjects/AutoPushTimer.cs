using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class AutoPushTimer : MonoBehaviour
	{
		[SerializeField] float TimeDelay = 0.5f;

		private void OnEnable()
		{
			Tools.DelayAction(this, TimeDelay, () =>
			{
				Pool.instance.Push(gameObject);
			});
		}
	}
}