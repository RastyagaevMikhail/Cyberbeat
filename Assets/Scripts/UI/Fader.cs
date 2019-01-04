using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [RequireComponent (typeof (CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] float TimeIn = 1f;
        [SerializeField] float TimeOut = 1f;
        [SerializeField] GameEvent OnFadeOutComplete;
        [SerializeField] GameEvent OnFadeInComplete;
        private CanvasGroup _canvasGroup = null;
        public CanvasGroup canvasGroup { get { if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup> (); return _canvasGroup; } }
        public void FadeIn ()
        {
            // Debug.LogFormat("FadeIn.{0}",name);
            canvasGroup.DOFade (1f, TimeIn).OnComplete (OnFadeInComplete.Raise);
        }
        public void FadeOut ()
        {
            // Debug.LogFormat("FadeOut.{0}",name);
            canvasGroup.DOFade (0f, TimeIn).OnComplete (OnFadeOutComplete.Raise);
        }
    }
}
