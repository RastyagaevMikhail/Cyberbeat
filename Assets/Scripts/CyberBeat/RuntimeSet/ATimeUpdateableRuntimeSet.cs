using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "ATimeUpdateableRuntimeSet.asset",
        menuName = "CyberBeat/RuntimeSet/ATimeUpdateable")]
    public class ATimeUpdateableRuntimeSet : RuntimeSet<ATimeUpdateable>
    {

        public void Start ()
        {
            ForEach (updatable => updatable.Start ());
        }
        public void UpdateInTime (float time)
        {
            ForEach (updatable => updatable.UpdateInTime (time));
        }

        [SerializeField] UnityEventATimeUpdateable onAddComplete;
        protected override UnityEvent<ATimeUpdateable> OnAddComplete
        {
            get
            {
                return onAddComplete;
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
