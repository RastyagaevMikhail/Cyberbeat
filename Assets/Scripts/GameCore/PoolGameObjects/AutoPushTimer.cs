using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class AutoPushTimer : MonoBehaviour
	{
		[SerializeField] float TimeDelay = 0.5f;
		[SerializeField] PoolVariable pool;

		private void OnEnable ()
		{
			Tools.DelayAction (this, TimeDelay, () =>
			{
				pool.Push (gameObject);
			});
		}
	}
}
