using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    using FluffyUnderware.Curvy;
    using FluffyUnderware.DevTools;

    using System.Reflection;
    using System;

    [System.Serializable]
    public class TrackSplineSegment
    {
        [SerializeField] string name;

        public Dictionary<Type, ICurvyMetadata> meta = null;
        public CurvySplineSegment segment;

        public float StartSpeed;

        public float EndSpeed;

        public float TimeOnSegment;

        public float StartTime;

        public float EndTime;

        public TrackSplineSegment Init_TrackSplineSegment (CurvySplineSegment segment, float startTime, float StartSpeed)
        {
            this.segment = segment;
            StartTime = startTime;
            this.StartSpeed = StartSpeed;
            // Вызывать после присвоения стартовой скорости
            Initialization_With_SpeedTimeMetaData ();
            meta = segment.GetComponents<ICurvyMetadata> ().ToDictionary (md => md.GetType ());
#if UNITY_EDITOR
            var metaName = "";
            foreach (var m in meta)
            {
                Type t = m.Value.GetType ();
                metaName += string.Format ("[{0}]", t.Name.Replace ("MetaData", ""));
            }
            string speed = StartSpeed == EndSpeed ? StartSpeed.ToString () : "{0}-->{1}".AsFormat (StartSpeed, EndSpeed);
            name = string.Format ("{0}[{1:00.00}-{2:00.00}][{3:00.00}][{4}]{5}", segment.name, StartTime, EndTime, TimeOnSegment, speed, metaName);
#endif

            return this;
        }
        public bool HasMeta<T> () where T : ICurvyMetadata
        {
            return GetMeta<T> () != null;
        }
        public T GetMeta<T> () where T : ICurvyMetadata
        {
            T result = default (T);
            checkMeta ();
            Type type = typeof (T);
            result = (T) meta[type];
            return result;
        }

        [SerializeField, HideInInspector]
        List<Type> typesOfMetaData = null;

        List<Type> TypesOfMetaData
        {
            get
            {
                if (typesOfMetaData == null)
                {
                    Type ourtype = typeof (ICurvyMetadata); // Базовый тип
                    Assembly assembly = Assembly.GetAssembly (ourtype);
                    var assemblyTypes = assembly.GetTypes ().ToList ();
                    typesOfMetaData = assemblyTypes.FindAll (type =>
                    {
                        bool result = type.GetInterface (ourtype.Name) != null;
                        return result;
                    });
                }
                return typesOfMetaData;
            }
        }
        private void checkMeta ()
        {
            if (meta == null || meta.Count != TypesOfMetaData.Count)
            {
                meta = segment.GetComponents<ICurvyMetadata> ().ToDictionary (md => md.GetType ());
                foreach (var item in TypesOfMetaData)
                {
                    if (!meta.ContainsKey (item))
                    {
                        meta.Add (item, null);
                    }
                }
            }
        }
        // Вызывать после присвоения стартовой скорости
        public void Initialization_With_SpeedTimeMetaData ()
        {
            var stmd = GetMeta<SpeedTimeMetaData> ();
            if (stmd)
                TimeOnSegment = stmd.time + ((segment.Length - stmd.S) / stmd.Speed);
            else
                TimeOnSegment = segment.Length / StartSpeed;
            EndTime = StartTime + TimeOnSegment;

            EndSpeed = stmd ? stmd.Speed : StartSpeed;

        }

        public float LocalFByTrackTime (float time)
        {
            var stmd = GetMeta<SpeedTimeMetaData> ();
            // Локальное время бита относительно текущего сегмента
            bool timeInAceleration = stmd ? time.InRange (StartTime, StartTime + stmd.time) : false;
            var localTime = time - StartTime;
            // Локальное растояние бита от начала сегмента
            float length =
                stmd ?
                timeInAceleration ?
                StartSpeed * localTime + (stmd.Acseleration * localTime * localTime) / 2f :
                StartSpeed * localTime :
                StartSpeed * localTime;
            return segment.DistanceToLocalF (length);
        }
        public float TFByTrackTime (float time)
        {
            return segment.LocalFToTF (LocalFByTrackTime (time));

        }
        public Vector3 InterpolateByTrackTime (float time)
        {
            return segment.Interpolate (LocalFByTrackTime (time));
        }

        public bool IsTrackTime (float time)
        {
            bool result = time.InRange (StartTime, EndTime);
            // Debug.LogFormat ("IsTrackTime of {0} {1} <= {3} <={2} is {4}", name, StartTime, EndTime, time, result);
            return result;
        }

    }
}
