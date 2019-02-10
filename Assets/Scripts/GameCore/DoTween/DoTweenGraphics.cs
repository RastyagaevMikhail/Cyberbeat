using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class DoTweenGraphics : MonoBehaviour
    {
        private Graphic _graphic = null;
        public Graphic graphic { get { if (_graphic == null) _graphic = GetComponent<Graphic> (); return _graphic; } }

        [SerializeField] Ease fadeInEase =  Ease.Linear;
        [SerializeField] Ease fadeOutEase = Ease.Linear;
        [SerializeField] Ease fadeEase = Ease.Linear;
        [SerializeField] float fadeValue =  0.5f;
        public void DoFadeOut (float duration)
        {
            graphic.DOFade (0f, duration).SetEase (fadeOutEase);
        }
        public void DoFadeIn (float duration)
        {
            graphic.DOFade (1f, duration).SetEase (fadeInEase);
        }
          public void DoFade (float duration)
        {
            graphic.DOFade (fadeValue, duration).SetEase (fadeEase);
        }
        public void DOFlip ()
        {
            graphic.DOFlip ();
        }
    }
}
