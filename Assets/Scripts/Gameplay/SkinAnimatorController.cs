using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SkinAnimatorController : MonoBehaviour
    {
        [SerializeField] AnimatorHashPlayerVariable skinAnimator;

        public void OnBit (IBitData bitData)
        {
            skinAnimator.Rebind ();
            skinAnimator.Play ("OnBit");
        }
    }
}
