using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameCore
{
    public class DoTweenGraphics : MonoBehaviour
    {
        // [HideInInspector]
        [SerializeField] Graphic graphic;
        private void OnValidate ()
        {
            if (graphic == null) graphic = GetComponent<Graphic> ();
        }

        [SerializeField] Ease fadeInEase = Ease.Linear;
        [SerializeField] Ease fadeOutEase = Ease.Linear;
        [SerializeField] Ease fadeEase = Ease.Linear;
        [SerializeField] float fadeValue = 0.5f;
        [SerializeField] UnityEvent onComplete;

        public float FadeValue { set => fadeValue = value; }

        public void DoFadeOut (float duration)
        {
            DoFadeOutTween (duration);
        }
        public Tween DoFadeOutTween (float duration)
        {
            return graphic.DOFade (0f, duration).SetEase (fadeOutEase).OnComplete(onComplete.Invoke);
        }
        public void DoFadeIn (float duration)
        {
            DoFadeInTween (duration);
        }
        public Tween DoFadeInTween (float duration)
        {
            return graphic.DOFade (1f, duration).SetEase (fadeInEase).OnComplete(onComplete.Invoke);;
        }
        public Tween DoFadeTween (float duration)
        {
            return graphic.DOFade (fadeValue, duration).SetEase (fadeEase).OnComplete(onComplete.Invoke);;
        }
        public void DoFade (float duration)
        {
            DoFadeTween (duration);
        }
    }
}
