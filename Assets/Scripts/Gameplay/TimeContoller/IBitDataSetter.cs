using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class IBitDataSetter : TransformObject
    {
        [SerializeField] UnityEventFloatHandler startTime;
        [SerializeField] UnityEventFloatHandler endTime;
        [SerializeField] UnityEventFloatHandler duration;
        [SerializeField] UnityEventStringHandler stringValue;
        [SerializeField] UnityEventStringHandler randomString;
        [SerializeField] UnityEventVector3Handler setPosition;
        [SerializeField] UnityEventVector3Handler setLossyScale;
        [SerializeField] UnityEventQuaternionHandler setRotation;
        [SerializeField] UnityEventVector3Handler setLocalPosition;
        [SerializeField] UnityEventVector3Handler setLocalScale;
        [SerializeField] UnityEventQuaternionHandler setLocalRotation;
        [SerializeField] UnityEventTransformHandler setTransform;
        public void OnBit (IBitData bitData)
        {
            startTime.Invoke (bitData.StartTime);
            endTime.Invoke (bitData.EndTime);
            duration.Invoke (bitData.Duration);
            stringValue.Invoke (bitData.StringValue);
            randomString.Invoke (bitData.RandomString);
            setPosition.Invoke (position);
            setLossyScale.Invoke (lossyScale);
            setRotation.Invoke (rotation);
            setLocalPosition.Invoke (localPosition);
            setLocalScale.Invoke (localScale);
            setLocalRotation.Invoke (localRotation);
            setTransform.Invoke (transform);
        }
    }

    [Serializable] public class UnityEventFloatHandler : UnityEventHandler<UnityEventFloat, float> { }

    [Serializable] public class UnityEventStringHandler : UnityEventHandler<UnityEventString, string> { }

    [Serializable] public class UnityEventVector3Handler : UnityEventHandler<UnityEventVector3, Vector3> { }

    [Serializable] public class UnityEventQuaternionHandler : UnityEventHandler<UnityEventQuaternion, Quaternion> { }

    [Serializable] public class UnityEventTransformHandler : UnityEventHandler<UnityEventTransform, Transform> { }
    public abstract class UnityEventHandler<UntyEventType, ValueType> where UntyEventType : UnityEvent<ValueType>
    {
        [SerializeField] bool enabled;
        [SerializeField] UntyEventType unityEvent;
        public void Invoke (ValueType argumrnt)
        {
            if (enabled)
                unityEvent.Invoke (argumrnt);
        }
    }

}
