namespace CyberBeat
{

    using System;

    using UnityEngine.Events;

    [Serializable] public class UnityEventBayable : UnityEvent<ABayable> { }

    [Serializable] public class UnityEventSpeedTimeData : UnityEvent<SpeedTimeData> { }

    [Serializable] public class UnityEventColorInfo : UnityEvent<ColorInfo> { }
}
