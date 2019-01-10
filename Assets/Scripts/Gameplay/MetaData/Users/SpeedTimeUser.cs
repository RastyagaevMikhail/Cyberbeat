using DG.Tweening;

using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;

using UnityEngine;
namespace CyberBeat
{

    public class SpeedTimeUser : MetaDataUser<SpeedTimeMetaData>
    {
        private SplineController _splineController = null;
        public SplineController splineController { get { if (_splineController == null) { _splineController = GetComponent<SplineController> (); } return _splineController; } }
        private void Awake ()
        {
            _SetSpeed (TracksCollection.instance.CurrentTrack.StartSpeed);
        }

        [SerializeField] FloatVariable SpeedVariable;
        public TweenCallback<float> OnSpeedUpdated;
        Tweener tweener;
        public override void OnMetaReached (SpeedTimeMetaData meta)
        {
            tweener = SpeedVariable.DO (meta.Speed, meta.time, speed =>
            {
                SpeedVariable.Value = speed;
                OnSpeedUpdated (speed);
            });
        }

        public void _SetSpeed (FloatVariable newSpeed)
        {
            _SetSpeed (newSpeed.Value);
        }
        public void _SetSpeed (float newSpeed)
        {
            splineController.Speed = newSpeed;
            // splineController.Play ();
        }
        public void _SlowStop (float StopDuration = 2f)
        {
            splineController.Speed.Do (0f, StopDuration, _SetSpeed, splineController.Stop);
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
                _SetSpeed (SpeedVariable);
        }
    }
}
