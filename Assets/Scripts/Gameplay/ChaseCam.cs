using FluffyUnderware.DevTools;

using GameCore;

using System;
using System.Collections;

using UnityEngine;
namespace CyberBeat
{
    [ExecuteInEditMode]
    public class ChaseCam : TransformObject
    {
        private static ChaseCam _instance = null;
        public static ChaseCam instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<ChaseCam> (); return _instance; } }

        [SerializeField] TransformVariable lookAt;
        public Transform LookAtTarget { get => lookAt ? lookAt.ValueFast : null; }

        [SerializeField] TransformVariable moveTo;
        public Transform MoveTo { get => moveTo?moveTo.ValueFast : null; }

        [SerializeField] TransformVariable rollTo;
        public Transform RollTo { get => rollTo?rollTo.ValueFast : null; }

        [Positive]
        public float ChaseTime = 0.5f;

        Vector3 mLastPos;
        Vector3 mVelocity;
        Vector3 mRollVelocity;

#if UNITY_EDITOR
        void Update ()
        {
            if (!Application.isPlaying)
            {
                if (MoveTo)
                    position = MoveTo.position;
                if (LookAtTarget)
                {
                    if (!RollTo) LookAt (LookAtTarget);
                    else LookAt (LookAtTarget, RollTo.up);
                }
                // if (RollTo)
                //     transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
            }
        }

#endif

        // Update is called once per frame
        void LateUpdate ()
        {
            if (MoveTo)
                position = Vector3.SmoothDamp (position, MoveTo.position, ref mVelocity, ChaseTime);
            if (LookAtTarget)
            {
                if (!RollTo) LookAt (LookAtTarget);
                else LookAt (LookAtTarget, Vector3.SmoothDamp (up, RollTo.up, ref mRollVelocity, ChaseTime));
            }
            // if (RollTo)
            //     transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
        }
    }
}
