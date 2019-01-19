namespace CyberBeat
{
    using SonicBloom.Koreo;

    using System;

    using UnityEngine.Events;

    [Serializable] public class UnityEventKoreographyEvent : UnityEvent<KoreographyEvent> { }
    [Serializable] public class UnityEventKoreography : UnityEvent<Koreography> { }

    [Serializable] public class UnityEventColorInterractor : UnityEvent<ColorInterractor> { }

    [Serializable] public class UnityEventBoosterData : UnityEvent<BoosterData> { }
    [Serializable] public class UnityEventBayable : UnityEvent<ABayable> { }
    [Serializable] public class UnityEventSpeedTimeData : UnityEvent<SpeedTimeData> { }
    [Serializable] public class UnityEventInputControlType : UnityEvent<InputControlType> { }
}
