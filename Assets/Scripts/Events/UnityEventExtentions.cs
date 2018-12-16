namespace CyberBeat
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    using SonicBloom.Koreo;

    using System;

    using UnityEngine.Events;

    [Serializable] public class UnityEventKoreographyEvent : UnityEvent<KoreographyEvent> { }
    [Serializable] public class UnityEventColorInterractor : UnityEvent<ColorInterractor> { }
}
