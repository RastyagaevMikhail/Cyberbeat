﻿using System;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class GameEventListenerSystemAction : MonoBehaviour
    {
        [SerializeField] EventListenerSystemAction listener;

        private void OnEnable ()
        {
            if (!listener.OnEnable ())
            {
                Debug.LogError ("Event not set On listener", this);
            }
        }

        private void OnDisable ()
        {
            if (!listener.OnDisable ())
            {
                Debug.LogError ("Event not set On listener", this);
            }
        }
    }
}