using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace CyberBeat
{
    public class WindowSlideController : RectTransformObject
    {
        private const int PlayWindowIndex = 1;
        [Header ("Animation Settings")]
        [SerializeField] Ease easeMove = Ease.OutBack;
        [SerializeField] float timetransition = 0.5f;
        [Header ("Data")]
        [SerializeField] IntVariable IndexofWindow;
        [Header ("Links")]
        [SerializeField] List<RectTransformObject> windows;
        [SerializeField] UnityEventBool startingTransition;
        [SerializeField] bool debug;

        int indexofWindow { get { return IndexofWindow.Value; } set { IndexofWindow.Value = value; } }
        private bool startTransition;
        int width { get { return (int) rectTransform.rect.width; } }
        public void ShowLastWindow ()
        {
            if (debug) Debug.Log ($"{this.Log()}");
#if UNITY_EDITOR
            indexofWindow = 2;
#endif
            MakeTransitionTo (PlayWindowIndex);
        }
        public void SetIndexWindow (int newIndexofWindow)
        {
            if (debug) Debug.Log ($"{this.Log()}.SetIndexWindow({newIndexofWindow})\nindexofWindow = {indexofWindow}\nstartTransition = {startTransition}", this);
            if (indexofWindow == newIndexofWindow || startTransition) return;
            MakeTransitionTo (newIndexofWindow);
        }

        private void MakeTransitionTo (int newIndexofWindow)
        {

            startTransition = true;
            startingTransition.Invoke (startTransition);
            var newWindow = windows[newIndexofWindow];
            if (debug) Debug.LogFormat ("newWindow = {0}", newWindow);
            var currentWindow = windows[indexofWindow];
            if (debug) Debug.LogFormat ("currentWindow = {0}", currentWindow);
            int dir = (newIndexofWindow - indexofWindow).Sign ();
            if (newIndexofWindow == windows.Count && indexofWindow == 0) dir = -1;
            newWindow.x = dir * width;
            newWindow.gameObject.SetActive (true);
            newWindow.rectTransform.DOAnchorPos3D (Vector2.zero, timetransition)
                .SetEase (easeMove);

            currentWindow.rectTransform
                .DOAnchorPos (Vector2.right * -dir * width, timetransition)
                .SetEase (easeMove)
                .OnComplete (() =>
                {
                    currentWindow.gameObject.SetActive (false);
                    startTransition = false;
                    startingTransition.Invoke (false);
                });
            indexofWindow = newIndexofWindow;
        }
    }
}
