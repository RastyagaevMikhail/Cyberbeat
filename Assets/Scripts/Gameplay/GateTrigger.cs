using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
	public class GateTrigger : MonoBehaviour
	{

		[SerializeField] ParticleSystem pSystem;
		[SerializeField] GameObject Tunnel;
		[SerializeField] GameObject EndGate;

		private void Start ()
		{
			Tunnel.SetActive (false);
			EndGate.SetActive (false);
		}
		private void OnTriggerEnter (Collider other)
		{

			if (other.CompareTag ("Player"))
			{

				pSystem.Play ();
				Tools.DelayAction (this, pSystem.duration, () =>
				{
					Tunnel.SetActive (true);
					EndGate.SetActive (true);
				});
			}
		}
	}
}
