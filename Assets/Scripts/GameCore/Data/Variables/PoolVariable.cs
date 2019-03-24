using System;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "Pool",
        menuName = "GameCore/Variable/Pool")]
    public class PoolVariable : SavableVariable<Pool>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Pool value)
        {
            Value = value;
        }

        public void SetValue (PoolVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From PoolVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From PoolVariable
        }

       

        public static implicit operator Pool (PoolVariable variable)
        {
            return variable.Value;
        }

        public void Push (SpawnedObject spawnedObject)
        {
            Value.Push (spawnedObject);
        }
        public SpawnedObject Pop (string key, Transform parent = null)
        {
            return Value.Pop (key, parent);
        }
        public void Pop (string key)
        {
            Value.Pop (key);
        }
        public T Pop<T> (string key, Transform parent = null) where T : Component
        {
            return Value.Pop<T> (key, parent);
        }

#if UNITY_EDITOR
        public void AddToPool (SpawnedObject spawnedObject)
        {
            Value.AddToPool (spawnedObject);
        }
#endif
    }
}
