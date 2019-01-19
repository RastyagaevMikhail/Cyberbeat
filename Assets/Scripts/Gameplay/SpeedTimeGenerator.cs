using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SpeedTimeGenerator : TransformObject
    {
        private const int sampleRate = 44100;
        [SerializeField] SpeedTimeDataPresetSelector selector;
        [SerializeField] UnityEventSpeedTimeData OnBitEvent;
        public void OnBit (KoreographyEvent koreographyEvent)
        {
            var data = selector[koreographyEvent.GetTextValue ()].Data;
            data.TimeDuaration = koreographyEvent.GetDurationTime (sampleRate);
            OnBitEvent.Invoke (data);
        }
    }
}
