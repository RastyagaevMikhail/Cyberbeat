using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class TunnelGateSetter : MonoBehaviour
	{

		[SerializeField] TimePointsData pointsData;
		[SerializeField] Transform PrefabGate;
		[HideInInspector, SerializeField] List<GameObject> Gates;
		// [SerializeField] Transform PrefabPraticles;
		// [SerializeField] Transform PrefabTrigger;
		[SerializeField] Transform Parent;
		[Button] public void Clear ()
		{
			foreach (var gateGO in Gates)
			{
				Tools.Destroy (gateGO);
			}
			Gates = new List<GameObject> ();
		}

		[Button]
		void SetGates ()
		{
			foreach (var point in pointsData.points)
			{
				var startGate = InstatiateGate (point.Start, "StartGate");
				// Instantiate (PrefabPraticles, startGate);
				// Instantiate (PrefabTrigger, transform.position - transform.forward * 20, Quaternion.identity, startGate);
				InstatiateGate (point.End, "EndGate");
			}
		}
		private Transform InstatiateGate (TimePointInfo pointInfo, string prefixName)
		{
			Transform gate = Instantiate (PrefabGate, pointInfo.position, pointInfo.rotation, Parent);
			gate.localPosition = pointInfo.position;
			gate.name = prefixName + "{0}".AsFormat (gate.GetInstanceID ());
			Gates.Add (gate.gameObject);
			return gate;
		}
	}
}
