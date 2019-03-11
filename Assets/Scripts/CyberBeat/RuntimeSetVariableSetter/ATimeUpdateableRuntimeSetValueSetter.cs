
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    public class ATimeUpdateableRuntimeSetValueSetter : MonoBehaviour
    {
        [SerializeField] ATimeUpdateableRuntimeSet set;
        [SerializeField] ATimeUpdateable value;
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

