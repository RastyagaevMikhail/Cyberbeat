using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
[CreateAssetMenu(
    fileName = "ATimeUpdateableRuntimeSet.asset",
    menuName = "CyberBeat/RuntimeSet/ATimeUpdateable")]
    public class ATimeUpdateableRuntimeSet : RuntimeSet<ATimeUpdateable> 
    {
        [SerializeField] UnityEventATimeUpdateable onAddComplete;
        protected override UnityEvent<ATimeUpdateable> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
        [SerializeField] UnityEventATimeUpdateable onRemoveComplete;
        protected override UnityEvent<ATimeUpdateable> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}
