using DG.Tweening;

using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

    public class SpeedTimeUser : MetaDataUser<SpeedTimeMetaData, SpeedTimeData>
    {
        private SplineController _splineController = null;
        public SplineController splineController { get { if (_splineController == null) { _splineController = GetComponent<SplineController> (); } return _splineController; } }

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

        public void Pause ()
        {
            if (tweener != null)
                tweener.Pause ();
            _SetSpeed (0);
        }
        public void Resume ()
        {
            if (tweener != null && tweener.IsActive ())
                tweener.TogglePause ();
            else
                _SetSpeed (lastSpeed);
        }

    }
}
