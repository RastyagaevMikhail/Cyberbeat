namespace CyberBeat
{
    using FluffyUnderware.Curvy;

    using GameCore;

    using UnityEngine;

    public class ChaseCamMetaData : MonoBehaviour, ICurvyMetadata
    {
        public ChaseCamData data;

        public string NameOfMetaType
        {
            get
            {
                return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
            }
        }
        [SerializeField] TransformVariable playerTransform;
        [ContextMenu ("Set Deafult values")]
        public void SetDefaultvalues ()
        {
            Transform PlayerParent = playerTransform.parent;

            Transform MoveTarget = PlayerParent.Find ("Move Target");
            Transform LookTarget = PlayerParent.Find ("Look Target");

            data.TargetMovePosition = MoveTarget.localPosition;
            data.TargetLookPosition = LookTarget.localPosition;

            
            data.DurationTimeOfMove = 1f;
            data.DurationTimeOfLook = 1f;

            ChaseCam chaskeCam = ChaseCam.instance;
            data.TimeChase = chaskeCam.ChaseTime;
        }
    }
}
