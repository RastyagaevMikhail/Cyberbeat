using Sirenix.OdinInspector;

using UnityEngine;

namespace GameCore
{
    public class TransformObject : SerializedMonoBehaviour
    {
        private Transform _transform = null;
        public new Transform transform { get { if (_transform == null) _transform = GetComponent<Transform> (); return _transform; } }
        public Vector3 position { get { return transform.position; } set { transform.position = value; } }
        public virtual float x { get { return position.x; } set { position = new Vector3 (value, y, z); } }
        public virtual float y { get { return position.y; } set { position = new Vector3 (x, value, z); } }
        public virtual float z { get { return position.z; } set { position = new Vector3 (x, y, value); } }
        public Vector3 localPosition { get { return transform.localPosition; } set { transform.localPosition = value; } }
        public float xLocal { get { return localPosition.x; } set { localPosition = new Vector3 (value, yLocal, zLocal); } }
        public float yLocal { get { return localPosition.y; } set { localPosition = new Vector3 (xLocal, value, zLocal); } }
        public float zLocal { get { return localPosition.z; } set { localPosition = new Vector3 (xLocal, yLocal, value); } }
        public Quaternion rotation { get { return transform.rotation; } set { transform.rotation = value; } }
        public Quaternion localRotation { get { return transform.localRotation; } set { transform.localRotation = value; } }
        public Vector3 localScale { get { return transform.localScale; } set { transform.localScale = value; } }
        public void SetParent (Transform child) { transform.SetParent (child); }
    }
}
