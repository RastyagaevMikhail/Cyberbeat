using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "ObjectVariable", menuName = "GameCore/Variable/UnityEngine/Object")]
    public class UnityObjectVariable : SavableVariable<Object>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Object value)
        {
            Value = value;
        }
        public T As<T> () where T : Object
        {
            return Value as T;
        }
        public void SetValue (UnityObjectVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            PlayerPrefs.SetString (name, JsonUtility.ToJson (Value));
        }

        public override void LoadValue ()
        {
            base.LoadValue ();
            _value = JsonUtility.FromJson<Object> (PlayerPrefs.GetString (name));
        }
    }

    public static class UnityObjectVariableEx
    {
        public static bool CheckAs<T> (this UnityObjectVariable uov, out T variable) where T : Object
        {
            variable = uov.As<T> ();
            return uov && uov.Value && variable;
        }
    }
}
