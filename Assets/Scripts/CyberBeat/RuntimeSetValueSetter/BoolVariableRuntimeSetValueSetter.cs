
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    public class BoolVariableRuntimeSetValueSetter : MonoBehaviour
    {
        [SerializeField] BoolVariableRuntimeSet set;
        [SerializeField] BoolVariable value;
        void OnEnable()
        {
            set.Add(value);
        }
        void OnDisable()
        {
            set.Remove(value);
        }
    }
}

