﻿using DG.Tweening;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "", menuName = "Variables/GameCore/Float")]
    public class FloatVariable : SavableVariable<float>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public float SmoothTime = 1f;

        public void SetSmoothly (float newValue)
        {
            DOVirtual.Float (Value, newValue, SmoothTime, value => Value = value);
        }
        public Tweener DO (float to, float duration, TweenCallback<float> onVirtualUpdate = null)
        {
            return DOVirtual.Float (base.Value, to, duration, onVirtualUpdate == null ? SetValue : onVirtualUpdate);
        }
        public void SetValue (float value)
        {
            Value = value;
        }

        public void SetValue (FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange (float amount)
        {
            Value += amount;
        }

        public void ApplyChange (FloatVariable amount)
        {
            Value += amount.Value;
        }

        public override void SaveValue ()
        {
            PlayerPrefs.SetFloat (name, Value);
        }

        public override void LoadValue ()
        {
            base.LoadValue ();
            _value = PlayerPrefs.GetFloat (name, DefaultValue);
        }
        public void Clamp (float min, float max)
        {
            _value = _value.GetAsClamped (min, max);
        }
        public void Clamp01 ()
        {
            _value.Clamp01 ();
        }
        public float AsPercent (FloatVariable otherVariable)
        {
            return AsPercent (otherVariable.Value);
        }
        public float AsPercent (float otherValue)
        {
            return otherValue * Value;
        }
        public int AsPercent (IntVariable otherVariable)
        {
            return AsPercent (otherVariable.Value);
        }
        public int AsPercent (int otherValue)
        {
            return (int) (otherValue * Value);
        }

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
        
        public static implicit operator float (FloatVariable variable)
        {
            return variable.Value;
        }
    }
}
