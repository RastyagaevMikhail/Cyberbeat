using GameCore;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (fileName = "ComboTunnelPostBuilder", menuName = "CyberBeat/TimePointsPostBuilder/ComboTunnel")]
    public class ComboTunnelPostBuilder : ATimePointsPostBuilder
    {
        [SerializeField] GameObject GatePrefab;
        [SerializeField] string PrefixName = "Gate";
        [SerializeField] string payloadFilter = "Combo";
        [SerializeField] Vector3 offset;
        [SerializeField] GateStateController gateControllerPrefab;

        public override GameObject PostBuild (TimePointsBuilder builder, GameObject generatorGO)
        {
            var count = GameObject.FindObjectsOfType<GateStateController> ().Length;
            var gateController = GameObject.Instantiate (gateControllerPrefab, builder.parent);
            gateController.name = "ComboTunnnel_{0}".AsFormat (count);
            GameObject start = null;
            GameObject end = null;
            foreach (var point in builder.DataSave[payloadFilter])
            {
                start = InstatiatePrefab (GatePrefab, point.Start, "Start_{0}".AsFormat (PrefixName), gateController.transform);
                end = InstatiatePrefab (GatePrefab, point.End, "End_{0}".AsFormat (PrefixName), gateController.transform);
            }
            gateController.Init (count, TracksCollection.instance.CurrentTrack, generatorGO, start, end);

            return gateController.gameObject;
        }

        private GameObject InstatiatePrefab (GameObject prefab, TimePointInfo pointInfo, string prefixName, Transform Parent)
        {
            if (!prefab)
            {
                return null;
            }
            GameObject prefabInstance = GameObject.Instantiate (prefab, pointInfo.position, pointInfo.rotation, Parent);
            prefabInstance.transform.localPosition = pointInfo.position;
            prefabInstance.transform.localPosition += offset;
            prefabInstance.name = prefixName + "{0}".AsFormat (prefabInstance.GetInstanceID ());
            prefabInstance.gameObject.SetActive (true);

            return prefabInstance;
        }
    }
}
