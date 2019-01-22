namespace CyberBeat
{
    using GameCore;

    using UnityEngine;

    [CreateAssetMenu (fileName = "ChaseCamDataPreset", menuName = "CyberBeat/MetaData/ChaseCamDataPreset", order = 0)]
    public class ChaseCamDataPreset : ScriptableObject, IMetaDataPreset<ChaseCamData>, IMetaDataPreset
    {
        [ContextMenuItem("SetDefaultvalues","SetDefaultvalues")]
        [SerializeField] ChaseCamData data;
        object IMetaDataPreset.Data => data;
        public ChaseCamData Data => data;

        [SerializeField] TransformVariable MoveTargetVariable;
        Transform MoveTarget { get { return MoveTargetVariable.Value; } }

        [SerializeField] TransformVariable LookTargetVariable;
        Transform LookTarget { get { return LookTargetVariable.Value; } }

        [ContextMenu ("Set Deafult values")]
        public void SetDefaultvalues ()
        {

            data.TargetMovePosition = MoveTarget.localPosition;
            data.TargetLookPosition = LookTarget.localPosition;

            // var stmd = GetComponent<SpeedTimeMetaData> ();

            data.DurationTimeOfMove = /* stmd ? stmd.time : */ 1f;
            data.DurationTimeOfLook = /* stmd ? stmd.time : */ 1f;

            ChaseCam chaskeCam = ChaseCam.instance;
            data.TimeChase = chaskeCam.ChaseTime;

            var comps = chaskeCam.GetComponentsInChildren<CameraShakeController> ();

            var shakeData = /* stmd ? comps[1] : */ comps[0];

            data.magnitude = shakeData.magnitude;
            data.roughness = shakeData.roughness;
            data.fadeInTime = shakeData.fadeInTime;
            data.fadeOutTime = shakeData.fadeOutTime;

            data.posInfluence = shakeData.shaker.DefaultPosInfluence;
            data.rotInfluence = shakeData.shaker.DefaultRotInfluence;

            this.Save();
        }
    }
}
