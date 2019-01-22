using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SpeedTimeGenerator : TransformObject
    {
        [SerializeField] SpeedTimeDataPresetSelector selector;
        [SerializeField] UnityEventSpeedTimeData OnBitEvent;
        public void OnBit (IBitData BitData)
        {
            var data = selector[BitData.StringValue].Data;
            data.TimeDuaration = BitData.Duration;
            OnBitEvent.Invoke (data);
        }
    }
}
