using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

using SonicBloom.Koreo;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using GameCore;
using Tools = GameCore.Tools;

namespace CyberBeat
{
    [Serializable]
    public class CurvyTrackBitViewer
    {
        CurvySpline Spline;
        List<KoreographyEvent> events = null;
        List<KoreographyEvent> Events
        {
            get
            {
                return events ??
                    (
                        events = track ?
                        track[layerColor.layer].FindAll (e => e.EndSample - e.StartSample == 0) :
                        new List<KoreographyEvent> ()
                    );
            }
        }
        float StartSpeed { get { return track ? track.StartSpeed : 0f; } }
        float SampleRate { get { return track ? track.koreography.SampleRate : 0f; } }
        public Color PointsColor { get { return layerColor.color; } }
        public bool draw { get { return layerColor.enable; } }
        public float SizePoint { get { return layerColor.SizePoints; } }

        public float OffsetSizePoint { get { return layerColor.OffsetSizePoint; } }

        [SerializeField, HideInInspector] Track track;
        [SerializeField /* , HideInInspector */ ] LayerColor layerColor;
        public CurvyTrackBitViewer (Track track, LayerColor layerColor, CurvySpline _Spline)
        {
            this.track = track;
            this.layerColor = layerColor;
            Spline = _Spline;
            Spline.OnRefresh.AddListener (Refresh);
            SpeedTimeMetaData.OnValueChanged += Refresh;
            Refresh (null);
        }

        [SerializeField]
        [HideInInspector]
        public List<BitPointInfo> BitPoints = new List<BitPointInfo> ();
        List<float> time_bits_of_rows;

        void Refresh (CurvySplineEventArgs arg0)
        {
            if (!Spline) return;
            BitPoints = new List<BitPointInfo> ();
            float speed = StartSpeed;
            float sum_time = 0;
            if (time_bits_of_rows == null || time_bits_of_rows.Count != Events.Count)
                time_bits_of_rows = Events.Select (s => s.StartSample / SampleRate).ToList ();

            foreach (var segment in Spline.GetComponentsInChildren<CurvySplineSegment> ())
            {

                var speed_time_meta_data = segment.GetComponent<SpeedTimeMetaData> ();
                if (speed_time_meta_data)
                {
                    //Вермя за которое измеиться скорость - время которое игрок движется с ускорением
                    float meta_Time = speed_time_meta_data.time;
                    // Скорость которую игрок достигнет через meta_Time
                    float meta_Speed = speed_time_meta_data.Speed;
                    // Ускорение
                    float acseleration = speed_time_meta_data.Acseleration;
                    // Длина сегмента которую пройдет игрок с ускорением
                    float length_before_acseleration = speed_time_meta_data.S;
                    // Длина сегмента которую пройдет игрок после ускорения
                    float length_after_acseleration = segment.Length - length_before_acseleration;
                    // Время, оставшееся для прохождения до конца сегмента, после прохождения куска с ускорением.
                    float time_after_aceleration = length_after_acseleration / meta_Speed;

                    // Расчет во время ускорения
                    CalculatePointsOnSegementBetween (
                        segment, sum_time,
                        sum_time + meta_Time,
                        (time) => speed * time + (acseleration * time * time) / 2f,
                        (length) => length,
                        GetMetaDataByTime
                    );

                    sum_time += meta_Time;
                    speed = meta_Speed;

                    // Расчет после ускорения
                    CalculatePointsOnSegementBetween (
                        segment, sum_time,
                        sum_time + time_after_aceleration,
                        (time) => speed * time,
                        (length) => speed_time_meta_data.S + length,
                        GetMetaDataByTime
                    );

                    sum_time += time_after_aceleration;

                }
                else
                {
                    // Общее время прохождения сегмента
                    float total_time_segment = segment.Length / speed;
                    // Расчет без ускорения
                    CalculatePointsOnSegementBetween (
                        segment, sum_time,
                        sum_time + total_time_segment,
                        (time) => speed * time,
                        (length) => length,
                        GetMetaDataByTime
                    );
                    sum_time += total_time_segment;
                }

            }
        }

        private void CalculatePointsOnSegementBetween (
            CurvySplineSegment segment,
            float start_time,
            float end_time,
            Func<float, float> local_length_setter,
            Func<float, float> localF_of_bit_seter,
            Func<float, string> metadata_seter = null)
        {
            var bits_on_after_aceleration_parts_on_segment = time_bits_of_rows.FindAll (t => t >= start_time && t < end_time);
            foreach (var bit in bits_on_after_aceleration_parts_on_segment)
            {
                // Локальное время бита относительно текущего сегмента
                float local_time_of_bit_on_segment = bit - start_time;
                // Локальное растояние бита от начала сегмента
                float local_length_of_bit_on_segment = local_length_setter (local_time_of_bit_on_segment);

                // Процент сегмента на расстояни текущего бита
                float localF_of_bit = segment.DistanceToLocalF (localF_of_bit_seter (local_length_of_bit_on_segment));
                // Точка положения бита на участке после ускорения
                Vector3 point_of_bit = segment.Interpolate (localF_of_bit);

                // Мета-Дата на бите
                string MetaData = "";
                if (metadata_seter != null)
                    MetaData = metadata_seter (bit);
                float tf = Spline.GetNearestPointTF (point_of_bit);
                var up = Spline.GetOrientationUpFast (tf);
                BitPoints.Add (new BitPointInfo (point_of_bit, up, MetaData));
            }
        }

        public string GetMetaDataByTime (float time)
        {
            var PresetsOnTimes = Events.ToDictionary (e => (float) e.StartSample / SampleRate, e => GetPayloadAsString (e.Payload));
            return PresetsOnTimes[time];
        }

        string GetPayloadAsString (IPayload payload)
        {
            if (payload == null) return "";
            Type type = payload.GetType ();
            return PayloadSelector[type] (payload);
        }

        Dictionary<Type, Func<IPayload, string>> PayloadSelector = new Dictionary<Type, Func<IPayload, string>>
        { { typeof (IntPayload), p => (p as IntPayload).IntVal.ToString () },
            { typeof (FloatPayload), p => (p as FloatPayload).FloatVal.ToString () },
            { typeof (TextPayload), p => (p as TextPayload).TextVal.ToString () },
            { typeof (IPayload), p => "" }
        };
#if UNITY_EDITOR
        public void DrawPoints ()
        {
            if (!draw || !Spline) return;
            float sizePoint = SizePoint;
            float offsetSizePoint = OffsetSizePoint;
            if (!Application.isPlaying)
                offsetSizePoint = 0;

            foreach (var bitPoint in BitPoints)
            {
                var p = bitPoint.position;
                var up = bitPoint.up;
                Handles.color = PointsColor;
                Handles.SphereHandleCap (0, p, Quaternion.identity, HandleUtility.GetHandleSize (p) * sizePoint + offsetSizePoint, EventType.Repaint);
                if (!string.IsNullOrEmpty (bitPoint.metadata))
                {
                    p += up * HandleUtility.GetHandleSize (p);
                    string Text = string.Format ("<b><color=\"#{1}\">{0}</color></b>", bitPoint.metadata, ColorUtility.ToHtmlStringRGB (PointsColor));
                    Handles.Label (p, Text, Tools.BackdropHtmlLabel);
                }
            }
        }

        // [DrawGizmo (GizmoType.Active | GizmoType.NonSelected | GizmoType.NotInSelectionHierarchy)]
        // static void MetaGizmoDrawer (CurvyTrackBitViewer data, GizmoType context)
        // {
        //     data.DrawPoints ();
        // }
#endif
    }

    [Serializable]
    public struct BitPointInfo
    {
        public Vector3 position;
        public Vector3 up;
        public string metadata;
        public BitPointInfo (Vector3 _position, Vector3 _up, string _metadata)
        {
            position = _position;
            metadata = _metadata;
            up = _up;
        }

    }
}
