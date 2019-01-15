using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    [RequireComponent (typeof (Animator))]
    public class AnimatorHashStatePlayer : MonoBehaviour
    {

        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) { _animator = GetComponent<Animator> (); } return _animator; } }

        Dictionary<string, int> _states = null;
        Dictionary<string, int> states { get { return _states ?? (_states = States.ToDictionary (s => s.NameState, s => s.HashState)); } }

        [SerializeField] List<AnimatorHashState> States;
        [ContextMenu ("validate")]
        private void Validate ()
        {
            foreach (var state in States)
                state.Validate ();
        }

        public void Play (string stateName)
        {
            int hash = 0;

            states.TryGetValue (stateName, out hash);

            if (hash == 0)
                hash = AddHash (stateName);

            animator.Play (hash);
        }

        private int AddHash (string stateName)
        {
            if (string.IsNullOrEmpty (stateName)) return 0;

            int hash = Animator.StringToHash (stateName);

            States.Add (new AnimatorHashState () { NameState = stateName, HashState = hash });
            _states.Add (stateName, hash);

            return hash;
        }

        [Serializable]
        public class AnimatorHashState
        {
            public string NameState;
            public int HashState;
            public void Validate ()
            {
                HashState = Animator.StringToHash (NameState);
            }
        }

    }
}
