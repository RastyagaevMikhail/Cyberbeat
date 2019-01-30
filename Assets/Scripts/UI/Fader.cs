using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameCore
{
    [RequireComponent (typeof (CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] UnityEvent OnFadeInComplete;
        [SerializeField] UnityEvent OnFadeOutComplete;
        private CanvasGroup _canvasGroup = null;
        public CanvasGroup canvasGroup { get { if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup> (); return _canvasGroup; } }
        public void FadeIn (float TimeIn)
        {
            canvasGroup.DOFade (1f, TimeIn).OnComplete (OnFadeInComplete.Invoke);
        }
        public void FadeOut (float TimeOut)
        {
            canvasGroup.DOFade (0f, TimeOut).OnComplete (OnFadeOutComplete.Invoke);
        }
    }
}
