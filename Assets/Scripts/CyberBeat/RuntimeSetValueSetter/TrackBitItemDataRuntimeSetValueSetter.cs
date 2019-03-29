
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    public class TrackBitItemDataRuntimeSetValueSetter : MonoBehaviour
    {
        [SerializeField] TrackBitItemDataRuntimeSet set;
        [SerializeField] TrackBitItemData value;
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

