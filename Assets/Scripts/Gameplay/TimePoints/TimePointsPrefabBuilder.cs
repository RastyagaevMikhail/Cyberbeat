using GameCore;

using System.Collections.Generic;

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
        [SerializeField] Vector3 offset;
        [HideInInspector, SerializeField] List<GameObject> PrefabInstances = new List<GameObject> ();
        // [SerializeField] Transform PrefabPraticles;
        // [SerializeField] Transform PrefabTrigger;
        [SerializeField] Transform Parent;

        [ContextMenu ("Build")]
        void Build ()
        {
            foreach (var point in pointsData[payloadFilter])
            {
                InstatiatePrefab (StartPrefab, point.Start, "Start_{0}".AsFormat (PrefixName));
                InstatiatePrefab (EndPrefab, point.End, "End_{0}".AsFormat (PrefixName));
            }
        }

        [ContextMenu ("Clear")]
        public void Clear ()
        {
            foreach (var gateGO in PrefabInstances)
            {
                Tools.Destroy (gateGO);
            }
            PrefabInstances = new List<GameObject> ();
        }
        private void InstatiatePrefab (Transform prefab, TimePointInfo pointInfo, string prefixName)
        {
            if (!prefab)
            {
                return;
            }

            Transform prefabInstance = Instantiate (prefab, pointInfo.position, pointInfo.rotation, Parent);
            prefabInstance.localPosition = pointInfo.position;
            prefabInstance.name = prefixName + "{0}".AsFormat (prefabInstance.GetInstanceID ());
            prefabInstance.transform.localPosition += offset;
            prefabInstance.gameObject.SetActive (true);

            PrefabInstances.Add (prefabInstance.gameObject);
        }
    }
}
