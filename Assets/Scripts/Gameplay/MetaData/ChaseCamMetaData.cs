namespace CyberBeat
{
    using FluffyUnderware.Curvy;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities;

    using UnityEngine;

    public class ChaseCamMetaData : MonoBehaviour, ICurvyMetadata
    {
        [HideLabel]
        public ChaseCamData data;

        public string NameOfMetaType
        {
            get
            {
                return this.GetType().Name.Replace("MetaData", "").SplitPascalCase();
            }
        }
        [Button]
        public void SetDefaultvalues()
        {
            Transform PlayerParent = Player.instance.transform.parent;

            Transform MoveTarget = PlayerParent.Find("Move Target");
            Transform LookTarget = PlayerParent.Find("Look Target");

            data.TargetMovePosition = MoveTarget.localPosition;
            data.TargetLookPosition = LookTarget.localPosition;

            var stmd = GetComponent<SpeedTimeMetaData>();

            data.DurationTimeOfMove = stmd ? stmd.time : 1f;
            data.DurationTimeOfLook = stmd ? stmd.time : 1f;

            ChaseCam chaskeCam = ChaseCam.instance;
            data.TimeChase = chaskeCam.ChaseTime;

            var comps = chaskeCam.GetComponentsInChildren<TestCameraShake>();

            var shakeData = stmd ? comps[1] : comps[0];

            data.magnitude = shakeData.magnitude;
            data.roughness = shakeData.roughness;
            data.fadeInTime = shakeData.fadeInTime;
            data.fadeOutTime= shakeData.fadeOutTime;

            data.posInfluence = shakeData.shaker.DefaultPosInfluence;
            data.rotInfluence = shakeData.shaker.DefaultRotInfluence;

        }
    }
}
