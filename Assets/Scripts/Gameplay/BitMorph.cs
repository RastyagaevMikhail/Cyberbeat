using System;
using System.Collections.Generic;
using System.Linq;

using DG.Tweening;

using Sirenix.OdinInspector;

using SonicBloom.Koreo;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class BitMorph : SerializedMonoBehaviour
    {
        Track track { get { return TracksCollection.instance.CurrentTrack; } }

        [SerializeField] UnityEvent OnBit;
        [SerializeField] UnityEvent OnAwake;
        [SerializeField] List<IMorph> ScaleMorphs;
        [SerializeField] List<IMorph> RotationMorphs;
        public GameData gameData { get { return GameData.instance; } }
        

        [SerializeField] bool enable;
        private void Awake ()
        {
            if (!enable || !Koreographer.Instance) return;

            Koreographer.Instance.RegisterForEvents (String.Format ("Bit"), OnBitKoreograpHyEvent);
            InitMorph (ScaleMorphs);
            InitMorph (RotationMorphs);
            OnAwake.Invoke ();
        }
        void InitMorph (List<IMorph> Morphs)
        {
            foreach (var morph in Morphs)
                morph.Init (transform);
        }
        private void OnBitKoreograpHyEvent (KoreographyEvent koreoEvent)
        {
            OnBit.Invoke ();
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

        [System.Serializable]
        public class ScaleMorph : Morph
        {
            [SerializeField] float SatrtValue = 1.5f, EndValue = 1f;
            [SerializeField] float Duration = 0.1f;

            public override void Invoke ()
            {
                transform.DOScale (SatrtValue, Duration).OnComplete (() => transform.DOScale (EndValue, Duration));
            }

        }

        [System.Serializable]
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
        public class Morph : IMorph
        {
            protected Transform transform;
            public void Init (Transform _transform) { transform = _transform; }
            public virtual void Invoke () { }
        }

        public interface IMorph
        {
            void Init (Transform _transform);
            void Invoke ();
        }
    }
}
