using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "BoolVariable", menuName = "Variables/GameCore/Bool")]
    public class BoolVariable : SavableVariable<bool>
    {
#if UNITY_EDITOR
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
#endif
        public override void LoadValue ()
        {
            base.LoadValue ();
            _value = Tools.GetBool (name);
        }

        public override void SaveValue ()
        {
            Tools.SetBool (name, Value);
        }

        public void SetValue (bool value)
        {
            Value = value;
        }

        public void SetValue (BoolVariable value)
        {
            Value = value.Value;
        }

    }
}
