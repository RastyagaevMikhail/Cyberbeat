using System;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "Pool",
        menuName = "Variables/CyberBeat/Pool")]
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

        [ContextMenu ("Toggle Savable")]
        void ToggleSavable () { isSavable = !isSavable; CheckSavable (); }

        [ContextMenu ("Check Savable")]
        void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }

        public static implicit operator Pool (PoolVariable variable)
        {
            return variable.Value;
        }

        public void Push (SpawnedObject spawnedObject)
        {
            ValueFast.Push (spawnedObject);
        }
        public SpawnedObject Pop (string key, Transform parent = null)
        {
            return ValueFast.Pop (key, parent);
        }
        public void Pop (string key)
        {
            ValueFast.Pop (key);
        }
        public T Pop<T> (string key, Transform parent = null) where T : Component
        {
            return ValueFast.Pop<T> (key, parent);
        }

        public void AddToPool(SpawnedObject spawnedObject)
        {
            ValueFast.AddToPool(spawnedObject);
        }
    }
}
