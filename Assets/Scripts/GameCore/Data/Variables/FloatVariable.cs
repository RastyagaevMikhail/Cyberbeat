using DG.Tweening;

using Sirenix.OdinInspector;

using UnityEngine;
namespace GameCore {
    [CreateAssetMenu(fileName = "", menuName = "Variables/GameCore/Float")]
    public class FloatVariable : SavableVariable<float> {
        [Button]
        public override void ResetDefault() {
            if (ResetByDefault) {
                Value = DefaultValue;
                SaveValue();
            }
        }
        public void SetSmoothly(float newValue) {
            SetSmoothly(newValue, 1f);
        }

        public void SetSmoothly(float newValue, float SmoothTime = 1f) {
            DOVirtual.Float(Value, newValue, SmoothTime, value => Value = value);
        }
        public Tweener DO(float to, float duration, TweenCallback<float> onVirtualUpdate = null) {
            return DOVirtual.Float(base.Value, to, duration, onVirtualUpdate == null ? SetValue : onVirtualUpdate);
        }
        public void SetValue(float value) {
            Value = value;
        }

        public void SetValue(FloatVariable value) {
            Value = value.Value;
        }

        public void ApplyChange(float amount) {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount) {
            Value += amount.Value;
        }

        [Button]
        public override void SaveValue() {
            PlayerPrefs.SetFloat(name, Value);
        }

        [Button]
        public override void LoadValue() {
            base.LoadValue();
            _value = PlayerPrefs.GetFloat(name, DefaultValue);
        }
        public void Clamp(float min, float max) {
            _value = _value.GetAsClamped(min, max);
        }
        public void Clamp01() {
            _value.Clamp01();
        }
        public float AsPercent(FloatVariable otherVariable) {
            return AsPercent(otherVariable.Value);
        }
        public float AsPercent(float otherValue) {
            return otherValue * Value;
        }
        public int AsPercent(IntVariable otherVariable) {
            return AsPercent(otherVariable.Value);
        }
        public int AsPercent(int otherValue) {
            return (int) (otherValue * Value);
        }
#if UNITY_EDITOR
        [ShowInInspector] string savedValue => PlayerPrefs.GetFloat(name, 0).ToString();
#endif
    }
}
