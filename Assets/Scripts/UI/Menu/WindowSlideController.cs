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
        [SerializeField] RectTransformObject Windows;
        [SerializeField] List<RectTransformObject> windows;
        [SerializeField] float timetransition = 0.5f;
        [SerializeField] IntVariable IndexofWindow;
        int indexofWindow { get { return IndexofWindow.Value; } set { IndexofWindow.Value = value; } }
        private bool startTransition;
        int width { get { return (int) rectTransform.rect.width; } }
        public void ShowLastWindow ()
        {
#if UNITY_EDITOR
            indexofWindow = PlayWindowIndex;
#endif
            windows[PlayWindowIndex].gameObject.SetActive (true);
        }
        public void SetIndexWindow (int newIndexofWindow)
        {
            if (indexofWindow == newIndexofWindow || startTransition) return;
            MakeTransitionTo (newIndexofWindow);
        }

        private void MakeTransitionTo (int newIndexofWindow)
        {
            startTransition = true;
            var newWindow = windows[newIndexofWindow];
            var currentWindow = windows[indexofWindow];
            int dir = (newIndexofWindow - indexofWindow).Sign ();
            if (newIndexofWindow == windows.Count && indexofWindow == 0) dir = -1;
            newWindow.x = dir * width;
            newWindow.gameObject.SetActive (true);
            newWindow.rectTransform.DOAnchorPos3D (Vector3.zero, timetransition);

            currentWindow.rectTransform
                .DOAnchorPos (Vector3.right * -dir * width, timetransition)
                .OnComplete (() =>
                {
                    currentWindow.gameObject.SetActive (false);
                    startTransition = false;
                });
            indexofWindow = newIndexofWindow;
        }
    }
}
