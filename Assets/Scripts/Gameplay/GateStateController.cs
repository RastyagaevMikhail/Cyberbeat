using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
	public class GateStateController : MonoBehaviour
	{
		private Renderer _rend = null;
		public Renderer rend { get { if (_rend == null) _rend = GetComponent<Renderer> (); return _rend; } }

		[SerializeField] Material activeMaterial;
		[SerializeField] Material notActiveMaterial;
		[SerializeField] GameObject Trigger;
		public Track track;
		public int Index;
		private void Start ()
		{
			Activate (track.GetGateState (Index));
			DisableTunelAndGate ();
		}
		public void Activate (bool active)
		{
			rend.sharedMaterial = active ? activeMaterial : notActiveMaterial;
			DisableTunelAndGate ();
			Trigger.SetActive (active);
		}

		public void _DeActivateIfMyIndex (int index)
		{
			if (index == Index)
			{
				Activate (false);

			}
		}

		public void _ActivateIfMyIndex (int index)
		{
			if (index == Index)
			{
				Activate (true);
			}
		}

		[SerializeField] ParticleSystem pSystem;
		[SerializeField] GameObject Tunnel;
		[SerializeField] GameObject EndGate;

		public void DisableTunelAndGate ()
		{
			Tunnel.SetActive (false);
			EndGate.SetActive (false);
		}

		public void _OpenGate ()
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
