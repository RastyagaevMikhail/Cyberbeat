using System.Collections;
using System.Collections.Generic;

using Timers;

using UnityEngine;
namespace GameCore
{
	public class AutoPushTimer : MonoBehaviour
	{
		[SerializeField] float TimeDelay = 0.5f;
		[SerializeField] PoolVariable pool;
		[SerializeField, HideInInspector] SpawnedObject spawnedObject = null;
		private void OnValidate ()
		{
			if (spawnedObject == null) spawnedObject = GetComponent<SpawnedObject> ();
		}
		
		private void Start ()
		{
			TimersManager.SetTimer (this, TimeDelay, () => pool.Push (spawnedObject));
		}

	}
}
