using DG.Tweening;

using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

    public class SpeedTimeUser : MetaDataUser<SpeedTimeMetaData, SpeedTimeData>
    {
        [SerializeField] SplineControllerVariable splineControllerVariable;
        public SplineController splineController => splineControllerVariable.ValueFast;

        [SerializeField] UnityEventFloat OnSpeedUpdated;
        Tweener tweener;
        private float lastSpeed;

        public override void OnMetaReached (SpeedTimeMetaData meta)
        {
            OnMetaData (meta.data);
        }
        public override void OnMetaData (SpeedTimeData metaData)
        {
            DOVirtual.Float (splineController.Speed, metaData.Speed, metaData.TimeDuaration, _SetSpeed);
        }

        public void _SetSpeed (FloatVariable newSpeed)
        {
            _SetSpeed (newSpeed.Value);
        }
        public void _SetSpeed (float newSpeed)
        {
            splineController.Speed = newSpeed;
            OnSpeedUpdated.Invoke (newSpeed);
        }
        public void _SlowStop (float StopDuration = 2f)
        {
            lastSpeed = splineController.Speed;
            DOVirtual.Float (splineController.Speed, 0f, StopDuration, _SetSpeed);
        }
    }
}
