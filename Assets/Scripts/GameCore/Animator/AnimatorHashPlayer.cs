using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class AnimatorHashPlayer : MonoBehaviour
    {
        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }
        public int AnimationsCount { get { return names.Count; } }

        [SerializeField] List<HashName> names;
        Dictionary<string, int> hash = null;
        Dictionary<string, int> Hash { get { return (hash??(hash = names.ToDictionary (n => n.name, n => n.hash))); } }

        public RandomStack<HashName> RandomStackNames => randomStackNames??(randomStackNames = new RandomStack<HashName> (names));
        [SerializeField] RandomStack<HashName> randomStackNames;
        [SerializeField] bool debug;

        public void PlayRandom ()
        {
            HashName hashName = RandomStackNames.Get ();
            if (debug) Debug.LogFormat ("hashName = {0}", hashName);
            animator.Play (hashName.hash);
        }

        private void OnEnable ()
        {
            if (_animator == null)
                _animator = GetComponent<Animator> ();
            if (hash == null) hash = names.ToDictionary (n => n.name, n => n.hash);
        }

        public void Play (string nameAnimation)
        {
            animator.Play (Hash[nameAnimation]);
        }
        public void Rebind ()
        {
            animator.Rebind ();
        }

        [ContextMenu ("Validate")]
        private void OnValidate ()
        {
            foreach (var n in names)
                n.OnValidate ();
        }

        [Serializable]
        public class HashName
        {
            public int hash;
            public string name;
            public void OnValidate ()
            {
                hash = Animator.StringToHash (name);
            }
            public override string ToString () => $"{name}:{hash}";
        }
    }
}
