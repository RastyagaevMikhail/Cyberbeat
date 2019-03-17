using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace CyberBeat
{
    public class OpenDebugButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] float waitSeconds = 9.5f;
        [SerializeField] GameObject warmImage;
        bool wait = false;
        float time;
        [SerializeField] UnityEvent openDebug;
        public void OnPointerDown (PointerEventData eventData)
        {
            time = Time.time;
            wait = true;
        }
        private void Update ()
        {
            if (wait)
            {
                if (Time.time - time > waitSeconds)
                {
                    warmImage.SetActive (true);
                    wait = false;
                }
            }
        }
        public void OnPointerUp (PointerEventData eventData)
        {
            if (Time.time - time > waitSeconds)
            {
                time = 0;
                warmImage.SetActive (false);
                openDebug.Invoke ();
            }
        }
    }
}
