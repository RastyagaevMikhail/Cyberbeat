using DG.Tweening;

using SonicBloom.Koreo;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class BitMorph : MonoBehaviour
    {
        [SerializeField] UnityEvent OnBitEvent;
        [SerializeField] UnityEvent OnAwake;
        [SerializeField] List<IMorph> ScaleMorphs;
        [SerializeField] List<IMorph> RotationMorphs;
        private void Awake ()
        {
            InitMorph (ScaleMorphs);
            InitMorph (RotationMorphs);
            OnAwake.Invoke ();
        }
        void InitMorph (List<IMorph> Morphs)
        {
            foreach (var morph in Morphs)
                morph.Init (transform);
        }
        private void OnBit (IBitData bitData)
        {
            InvokeScaleMorphs ();
            InvokeRotrationMorphs ();
            OnBitEvent.Invoke ();
        }

        public void InvokeScaleMorphs ()
        {
            foreach (var morph in ScaleMorphs)
                morph.Invoke ();
        }
        public void InvokeRotrationMorphs ()
        {
            foreach (var morph in ScaleMorphs)
                morph.Invoke ();
        }

        [Serializable]
        public class ScaleMorph : Morph
        {
            [SerializeField] float SatrtValue = 1.5f, EndValue = 1f;
            [SerializeField] float Duration = 0.1f;

            public override void Invoke ()
            {
                transform.DOScale (SatrtValue, Duration).OnComplete (() => transform.DOScale (EndValue, Duration));
            }

        }

        [Serializable]
        public class RotationMorph : Morph
        {
            [SerializeField] float duration = 1;
            [SerializeField] float angle = 180f;
            [SerializeField] Vector3 WorldDirection = Vector3.up;
            [SerializeField] Space space = Space.Self;
            [SerializeField] DG.Tweening.LoopType loopType;
            [SerializeField] int LoopsCount = 1;
            [SerializeField] DG.Tweening.RotateMode rotateMode;
            public override void Invoke ()
            {
                Vector3 Direction = space == Space.Self ? transform.up : WorldDirection;
                transform.DOLocalRotateQuaternion (Quaternion.LookRotation (Direction * angle), duration).SetLoops (LoopsCount, loopType);
            }
        }

        [System.Serializable]
        public abstract class Morph : IMorph
        {
            protected Transform transform;
            public void Init (Transform _transform) { transform = _transform; }
            public abstract void Invoke ();
        }

        public interface IMorph
        {
            void Init (Transform _transform);
            void Invoke ();
        }
    }
}
