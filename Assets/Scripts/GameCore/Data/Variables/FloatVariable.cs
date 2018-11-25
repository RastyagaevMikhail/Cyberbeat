using UnityEngine;
using DG.Tweening;
namespace GameCore
{
    [CreateAssetMenu(fileName = "", menuName = "Variables/GameCore/Float")]
    public class FloatVariable : SavableVariable<float>
    {
        public float SmoothTime = 1f;
    
        public void SetSmoothly(float newValue)
        {
            DOVirtual.Float(Value, newValue, SmoothTime, value => Value = value);
        }
        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }

        public override void SaveValue()
        {
            PlayerPrefs.SetFloat(name, Value);
        }

        public override void LoadValue()
        {
            base.LoadValue();
            _value = PlayerPrefs.GetFloat(name, 0f);
        }
    }
}
