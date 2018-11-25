using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [RequireComponent (typeof (Animator))]
    public class AnimatorParameterSetter : MonoBehaviour
    {
        public List<FloatSetter> Floats;
        public List<IntSetter> Ints;
        public List<BoolSetter> Bools;
        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }

        [Button]
        private void Validate ()
        {
            Floats.ForEach (f => f.OnValidate ());
            Ints.ForEach (i => i.OnValidate ());
            Bools.ForEach (b => b.OnValidate ());
        }

        private void Update ()
        {
            Floats.ForEach (f => f.Update (animator));
            Ints.ForEach (i => i.Update (animator));
            Bools.ForEach (b => b.Update (animator));
        }
    }

    [System.Serializable]
    public class FloatSetter : VariabelSetter<FloatVariable>
    {
        public override void Update (Animator animator)
        {
            animator.SetFloat (parameterHash, Variable.Value);
        }
    }

    [System.Serializable]
    public class IntSetter : VariabelSetter<IntVariable>
    {
        public override void Update (Animator animator)
        {
            animator.SetInteger (parameterHash, Variable.Value);
        }
    }

    [System.Serializable]
    public class BoolSetter : VariabelSetter<BoolVariable>
    {
        public override void Update (Animator animator)
        {
            animator.SetBool (parameterHash, Variable.Value);
        }
    }
    public class VariabelSetter<T>
    {
        public T Variable;
        public string ParameterName;
        [SerializeField] protected int parameterHash;
        public void OnValidate ()
        {
            parameterHash = Animator.StringToHash (ParameterName);
        }
        public virtual void Update (Animator animator) { }
    }
}
