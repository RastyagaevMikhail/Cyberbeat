﻿using DG.Tweening;

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

        [SerializeField] FloatVariable SpeedVariable;
        public TweenCallback<float> OnSpeedUpdated;
        public override void OnMetaReached (SpeedTimeMetaData meta)
        {
            SpeedVariable.DO (meta.Speed, meta.time, speed =>
            {
                SpeedVariable.Value = speed;
                OnSpeedUpdated (speed);
            });
        }

        public void _SetSpeed (FloatVariable newSpeed)
        {
            _SetSpeed(newSpeed.Value);
        }
        public void _SetSpeed (float newSpeed)
        {
            splineController.Speed = newSpeed;
            splineController.Play ();
        }
        public void _SlowStop (float StopDuration = 2f)
        {
            splineController.Speed.Do (0f, StopDuration, value =>
                {
                    splineController.Speed = value;
                    // splineController.Play ();
                },
                splineController.Stop);
        }
    }
}
