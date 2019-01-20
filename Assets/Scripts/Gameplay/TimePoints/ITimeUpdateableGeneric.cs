using GameCore;

using UnityEngine.Events;

namespace CyberBeat
{
    public interface ITimeUpdateableGeneric<TSavableVariable, TValue, TUnityEvent, TEventArgument> : ITimeUpdateable
    where TSavableVariable : SavableVariable<TValue>
        where TUnityEvent : UnityEvent<TEventArgument>
        {
            TSavableVariable Variable { get; }
            TUnityEvent UnityEvent { get; }
        }
}
