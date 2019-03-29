using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "TrackBitItemDataRuntimeSet.asset",
        menuName = "CyberBeat/RuntimeSet/TrackBitItemData")]
    public class TrackBitItemDataRuntimeSet : RuntimeSet<TrackBitItemData>
    {
        [SerializeField] UnityEventTrackBitItemData onAddComplete;
        protected override UnityEvent<TrackBitItemData> OnAddComplete
        {
            get
            {
                return onAddComplete;
            }
        }

        [SerializeField] UnityEventTrackBitItemData onRemoveComplete;
        protected override UnityEvent<TrackBitItemData> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }

        public void Start ()
        {
            ForEach (bit => bit.Start ());
        }
        public void UpdateInTime (float time)
        {
            ForEach (bit => bit.UpdateInTime (time));
        }
    }
}
