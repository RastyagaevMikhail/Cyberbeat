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
        [SerializeField] Image FillImage;
        bool started;
        [SerializeField] UnityEvent OnPress;
        [SerializeField] UnityEvent OnFillComplete;
        [SerializeField] float CDTime = 1f;
        public void Init (float cdTime = 1f, UnityAction onPress = null)
        {
<<<<<<< HEAD
            OnPointerDown (null);
        }
        public void OnPointerDown (PointerEventData eventData)
        {
            bool TweenIsPlaying =
                cdTween != null &&
                cdTween.IsPlaying ();

            // Debug.LogFormat ("TweenIsPlaying = {0}", TweenIsPlaying);

            if (TweenIsPlaying) return;
            // Debug.LogFormat ("Interractable = {0}", Interractable);
            if (!Interractable)
=======
            CDTime = cdTime;
            if (onPress != null)
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
            {
                OnPress.RemoveAllListeners ();
                OnPress.AddListener (onPress);
            }
<<<<<<< HEAD

            onPress.Invoke ();


            cdTween = DOVirtual
                .Float (0f, CDTime, CDTime, OnTick)
                .OnComplete (onComlete);
=======
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
        }
        public void OnPointerDown (PointerEventData eventData)
        {
            if (started ) return;
            started = true; 
            OnPress.Invoke ();
            StartCoroutine (cr_StartCDFill ());
        }
        public void Reset ()
        {
            StopAllCoroutines ();
            started = false;
            FillImage.fillAmount = 1;
        }
        IEnumerator cr_StartCDFill ()
        {
            float seconds = CDTime.Abs ();
            
            while (seconds > 0)
            {
                seconds -= Time.deltaTime;
                FillImage.fillAmount = 1f - seconds / CDTime.Abs ();
                yield return new WaitForEndOfFrame ();
            }
            OnFillComplete.Invoke ();
            started = false;
        }
    }
}
