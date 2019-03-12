using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (CanvasGroup))]
    public class DoTweenAlphaFlash : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        private void OnValidate ()
        {
            canvasGroup = GetComponent<CanvasGroup> ();
        }

        [SerializeField] float fadeInTime;
        [SerializeField] Ease easeFadeIn;
        [SerializeField] float fadeOutTime;
        [SerializeField] Ease easeFadeOut;
        [SerializeField] float minFadeAlpha;
        [SerializeField] float maxFadeAlpha;

        public void DoFlash ()
        {
            canvasGroup.DOFade (1, fadeInTime).SetEase (easeFadeIn)
                .OnComplete (() => canvasGroup.DOFade (0, fadeOutTime).SetEase (easeFadeOut));
        }
        public void DoFlasWithAlpha ()
        {
            canvasGroup.DOFade (maxFadeAlpha, fadeInTime).SetEase (easeFadeIn)
                .OnComplete (() => canvasGroup.DOFade (minFadeAlpha, fadeOutTime).SetEase (easeFadeOut));
        }
    }
}
