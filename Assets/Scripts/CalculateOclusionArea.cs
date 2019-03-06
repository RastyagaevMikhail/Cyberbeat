using FluffyUnderware.Curvy;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (OcclusionArea))]
    public class CalculateOclusionArea : MonoBehaviour
    {
        [SerializeField] CurvySpline spline;
        [SerializeField] OcclusionArea area;
        private void OnValidate ()
        {
            area = GetComponent<OcclusionArea> ();
            spline = GetComponentInParent<CurvySpline> ();
        }

        [Button] 
        public void Calculate ()
        {
            area.size = spline.Bounds.size;
            area.center = spline.Bounds.center;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty (area);
#endif
        }
    }
}
