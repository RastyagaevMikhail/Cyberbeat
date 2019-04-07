using UnityEngine;

namespace GameCore
{
    public class PropertyBlockInfo : ScriptableObject
    {
        [HideInInspector]
        [SerializeField] protected Renderer renderer;
        protected MaterialPropertyBlock propBlock = null;
        [HideInInspector]
        [SerializeField] protected int namehash = 0;
        [SerializeField] protected string Name;
        public void Init (Renderer rend)
        {
            renderer = rend;
        }

        public void OnValidte ()
        {
            ValidateHash ();
            ValidateBlock ();
        }

        private void ValidateHash ()
        {
            namehash = Shader.PropertyToID (Name);
        }

        protected void ValidateBlock ()
        {
            if (propBlock == null)
                propBlock = new MaterialPropertyBlock ();
        }

        public virtual void OnUpdateInEditor ()
        {
            ValidateBlock ();
        }
    }
    public abstract class PropertyBlockInfo<T> : PropertyBlockInfo
    {
        public abstract T property { set; get; }

        [SerializeField] protected T value = default (T);
        public override void OnUpdateInEditor ()
        {
            base.OnUpdateInEditor ();
            property = value;
        }
    }
}
