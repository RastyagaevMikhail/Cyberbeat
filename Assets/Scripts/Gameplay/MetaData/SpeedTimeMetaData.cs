using FluffyUnderware.Curvy;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace CyberBeat
{
    // [ExecuteInEditMode]
    public class SpeedTimeMetaData : MonoBehaviour, ICurvyMetadata
    {
        public SpeedTimeData data;
        public float Speed { get { return data.Speed; } }
        public float time { get { return data.TimeDuaration; } }

        private CurvySplineSegment _ControlPoint = null;
        public CurvySplineSegment ControlPoint { get { if (_ControlPoint == null) _ControlPoint = GetComponent<CurvySplineSegment> (); return _ControlPoint; } }

        public Vector3 Up { get { return ControlPoint.GetOrientationUpFast (0); } }
        TrackVariable trackVariable;
        float startSpeedOnTrack { get { return trackVariable.ValueFast.StartSpeed; } }

        [SerializeField] float startSpeed = 0;
        public float StartSpeed
        {
            get
            {
                CalculatePrevSpeed ();
                return startSpeed;
            }
        }

        [ContextMenu ("CalculatePrevSpeed")]
        void CalculatePrevSpeed ()
        {
            var PrevSegment = ControlPoint.PreviousSegment;
            if (PrevSegment == null)
            {
                startSpeed = startSpeedOnTrack;
                return;
            }
            while (PrevSegment)
            {
                var STMD = PrevSegment.GetComponent<SpeedTimeMetaData> ();
                if (STMD)
                {
                    startSpeed = STMD.Speed;
                    return;
                }
                PrevSegment = PrevSegment.PreviousSegment;
            }
            startSpeed = startSpeedOnTrack;

        }
        private float acseleration;
        public float Acseleration
        {
            get
            {
                if (time == 0)
                {
                    acseleration = 0;
                    return acseleration;
                }
                acseleration = (Speed - StartSpeed) / time;
                return acseleration;
            }
        }
        public float S { get { if (time == 0) return 0; return StartSpeed * time + (Acseleration * time * time) / 2f; } }

        public override string ToString ()
        {
            return string.Format ("Speed:{0}\nTime:{1:F2}", Speed, time);
        }
        public static Action<CurvySplineEventArgs> OnValueChanged;

        public float FPoint { get { return ControlPoint.DistanceToLocalF (S); } }

        private void OnValidate ()
        {
            if (OnValueChanged != null)
                OnValueChanged (null);
            if (Speed == 0) { data.Speed = StartSpeed; data.TimeDuaration = 1f; }
        }
#if UNITY_EDITOR
        private void OnDestroy ()
        {
            data.Speed = StartSpeed;
            data.TimeDuaration = 0;
            if (OnValueChanged != null)
                OnValueChanged (null);
        }
#endif
        #region Impelmet interface
        //---Implement ICurvyInterpolatableMetadata---
        public object Value { get { return data; } }
        public object InterpolateObject (ICurvyMetadata b, float f)
        {
            return Interpolate (b, f);
        }
        //--------------------------------------------
        //---Implement ICurvyInterpolatableMetadata<U>---
        public float Interpolate (ICurvyMetadata b, float f)
        {
            return 0f;
        }
        //--------------------------------------------
        public string NameOfMetaType
        {
            get
            {
                return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
            }
        }

        public float InterpolateSpeedByLocalF (float f)
        {
            float value = Mathf.Lerp (StartSpeed, Speed, AadaptInterpolation (f));
            return value;
        }
        public float GetTimeBitFromLocalInterpalation (float f)
        {
            float value = 0;
            return value;
        }
        private float AadaptInterpolation (float f)
        {
            float fPoint = FPoint;
            return Mathf.Clamp (f, 0, fPoint) / fPoint;
        }

        public float InterpolateSpeedByPosition (Vector3 posiiton)
        {
            float value = InterpolateSpeedByLocalF (ControlPoint.GetNearestPointF (posiiton));
            return value;
        }

        #endregion

    }

    [Serializable]

    public class SpeedTimeData : IMetaData
    {
        public float Speed;
        [Range (0, 10)]
        [SerializeField] float time;

        public float TimeDuaration { get { return time; } set { time = value; } }

        public void Print ()
        {
            Debug.LogFormat ("Speed = {0}\ntime = {1}", Speed, TimeDuaration);
        }
    }
}
