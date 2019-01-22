using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/ATimeUpdateable/BitTimeItem/TrackBitItemData")]
    public class TrackBitItemData : BitTimeItem<TrackBit>
    {
        protected override void OnTimeIsOver ()
        {
            base.OnTimeIsOver ();
            Debug.LogFormat ("indexOfTime = {0}", indexOfTime);
            Debug.LogFormat ("TimeItems.Count() = {0}", TimeItems.Count ());
            Debug.LogFormat ("TimeItems.GetType() = {0}", TimeItems.GetType ());
            foreach (var item in TimeItems)
            {
                Debug.LogFormat ("item = {0}", item);
            }
        }
    }
}
