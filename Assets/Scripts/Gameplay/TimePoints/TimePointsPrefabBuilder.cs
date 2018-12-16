using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;

using GameCore;

using Sirenix.OdinInspector;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class TimePointsPrefabBuilder : MonoBehaviour
	{

		[SerializeField] TimePointsData pointsData;
		[SerializeField] Transform StartPrefab;
		[SerializeField] Transform EndPrefab;
		[SerializeField] string PrefixName = "Gate";
		[SerializeField] string payloadFilter = "Combo";
		[HideInInspector, SerializeField] List<GameObject> PrefabInstances = new List<GameObject> ();
		// [SerializeField] Transform PrefabPraticles;
		// [SerializeField] Transform PrefabTrigger;
		[SerializeField] Transform Parent;

		[Button] void Build ()
		{
			foreach (var point in pointsData[payloadFilter])
			{
				InstatiatePrefab (StartPrefab, point.Start, "Start_{0}".AsFormat (PrefixName));
				InstatiatePrefab (EndPrefab, point.End, "End_{0}".AsFormat (PrefixName));
			}
		}

		[Button] public void Clear ()
		{
			foreach (var gateGO in PrefabInstances)
			{
				Tools.Destroy (gateGO);
			}
			PrefabInstances = new List<GameObject> ();
		}
		private void InstatiatePrefab (Transform prefab, TimePointInfo pointInfo, string prefixName)
		{
			if (!prefab) return;

			Transform prefabInstance = Instantiate (prefab, pointInfo.position, pointInfo.rotation, Parent);
			prefabInstance.localPosition = pointInfo.position;
			prefabInstance.name = prefixName + "{0}".AsFormat (prefabInstance.GetInstanceID ());
			PrefabInstances.Add (prefabInstance.gameObject);
		}
	}
}
