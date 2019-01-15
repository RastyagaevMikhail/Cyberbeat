using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GameCore
{
    public class CDButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] Image fillImage;
        [SerializeField] float _CDTime = 1f;
        public float CDTime { get { return _CDTime; } set { _CDTime = value; } }

        [SerializeField] bool interractable = true;
        public bool Interractable { get { return interractable; } set { interractable = value; } }

        [Header ("Events")]
        [SerializeField] UnityEvent onPress;
        [SerializeField] UnityEvent onPressFaild;
        [SerializeField] UnityEvent onFillComplete;
        [SerializeField] UnityEventFloat onTickAsSeconds;
        [SerializeField] UnityEventFloat onTickAsFillPercent;
        Tween cdTween;
        public void OnDown ()
        {
            OnPointerDown (null);
        }
        public void OnPointerDown (PointerEventData eventData)
        {
            bool TweenIsPlaying = cdTween != null && cdTween.IsPlaying ();

            if (TweenIsPlaying) return;

            if (!Interractable)
            {
                onPressFaild.Invoke ();
                return;
            }

            onPress.Invoke ();

            cdTween = DOVirtual
                .Float (0f, CDTime, CDTime, OnTick)
                .OnComplete (onComlete);
        }
        private void onComlete ()
        {
            cdTween.Kill (true);
            onFillComplete.Invoke ();
        }
        private void OnTick (float value)
        {
            onTickAsSeconds.Invoke (value);

            float fillPercent = value / CDTime.Abs ();

            fillImage.fillAmount = fillPercent;

            onTickAsFillPercent.Invoke (fillPercent);
        }
        public void Reset ()
        {
            cdTween.Kill (true);
        }
    }
}
