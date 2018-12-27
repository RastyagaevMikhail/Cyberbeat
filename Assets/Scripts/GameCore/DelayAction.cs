using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class DelayAction : MonoBehaviour
    {
        [SerializeField] float delay;
        [SerializeField] UnityEvent action;

        public void Invoke()
        {
            StartCoroutine(IvokeDealayAction());
        }
        public void Invoke(float delayDuration, UnityAction _anction = null)
        {
            delay = delayDuration;

            if (_anction != null)
            {
                action.AddListener(_anction);
            }

            Invoke();
        }

        IEnumerator IvokeDealayAction()
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}