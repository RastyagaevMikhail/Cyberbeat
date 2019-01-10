using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore.DoTween
{
    [RequireComponent (typeof (Animator))]
    public class DoTweenAnimatorController : MonoBehaviour
    {
        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }

        [SerializeField] float duration = 1f;
        [SerializeField] Ease ease = Ease.Linear;
        [SerializeField] int layer = -1;
        Tween currentTween;
        public void Play (string NameState, TweenCallback OnComplete = null)
        {
            int hashState = Animator.StringToHash (NameState);
            currentTween = DOVirtual.Float (0f, 1f, duration, value => onPlay (layer, value, hashState)).SetEase (ease).OnComplete (OnComplete);

        }
        public void Pause ()
        {
            if (currentTween != null && currentTween.IsPlaying ()) currentTween.Pause ();
        }
        public void Resume ()
        {
            if (currentTween != null && !currentTween.IsPlaying ()) currentTween.TogglePause ();
        }
        public void Stop ()
        {
            if (currentTween != null)
            {
                currentTween.Pause ();
                currentTween.Kill ();
            }
        }

        private void onPlay (int layer, float value, int hashState)
        {
            animator.Play (hashState, layer, value);
            animator.speed = 0;
        }
    }
}
