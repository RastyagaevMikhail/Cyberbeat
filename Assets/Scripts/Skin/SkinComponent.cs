using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SkinComponent : MonoBehaviour
    {
        [SerializeField] new Renderer renderer;
        public Renderer Renderer => renderer;
        [SerializeField] AnimatorHashPlayer animator;
        private void OnValidate ()
        {
            animator = GetComponent<AnimatorHashPlayer> ();
        }
        [SerializeField] float animationClipTime = 2f;
        public void StartAniamtion ()
        {
            if (!gameObject.activeSelf || !gameObject.activeInHierarchy) return;
            StartCoroutine (cr_randomAnimation ());
        }

        private IEnumerator cr_randomAnimation ()
        {
            WaitForSeconds wfs = new WaitForSeconds (animationClipTime);
            while (true)
            {
                animator.PlayRandom ();
                yield return wfs;
            }
        }
        public void StopAniamtion ()
        {
            StopAllCoroutines ();
        }
    }
}
