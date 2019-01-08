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
            CDTime = cdTime;
            if (onPress != null)
            {
                OnPress.RemoveAllListeners ();
                OnPress.AddListener (onPress);
            }
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
