using GameCore;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "MeshRenderer", menuName = "GameCore/Variable/MeshRenderer")]
    public class MeshRendererVariable : SavableVariable<MeshRenderer>
    {
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }

        [SerializeField] StringVariable TextureName;
        [SerializeField] int textureNameHash = 0;
        string textureName => TextureName?TextureName.Value: "_MainTex";
        [SerializeField] bool usePopretyBlock;
        MaterialPropertyBlock props;
        private void OnEnable ()
        {
            if (usePopretyBlock) { props = new MaterialPropertyBlock (); }
        }
        private void OnValidate ()
        {
            textureNameHash = Shader.PropertyToID (textureName);
        }
        public void SetTexture (Texture texture)
        {
            if (Value)
            {
                if (usePopretyBlock)
                {
                    Value.GetPropertyBlock (props);
                    props.SetTexture (textureNameHash, texture);
                    Value.SetPropertyBlock (props);
                }
                else
                {
                    Value.sharedMaterial.SetTexture (textureNameHash, texture);
                }
            }

        }

        public void SetValue (MeshRendererVariable value)
        {
            Value = value.Value;
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From MeshRendererVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From MeshRendererVariable
        }
        public static implicit operator MeshRenderer (MeshRendererVariable variable)
        {
            return variable.Value;
        }
    }
}
