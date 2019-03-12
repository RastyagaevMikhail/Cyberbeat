using GameCore;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "Material",
        menuName = "Variables/GameCore/Material")]
    public class MaterialVariable : SavableVariable<Material>
    {
        [SerializeField] StringVariable ColorName;
        [SerializeField] string colorName => ColorName.Value;
        public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
        public void SetValue (Material value)
        {
            Value = value;
        }

        public void SetValue (MaterialVariable value)
        {
            Value = value.Value;
        }
        public void SetColor (Color color)
        {
            Value.SetColor (colorName, color);
        }

        public override void SaveValue ()
        {
            // TODO: Save Code This From MaterialVariable
        }

        public override void LoadValue ()
        {
            // TODO: Load Code This From MaterialVariable
        }

       

        public static implicit operator Material (MaterialVariable variable)
        {
            return variable.Value;
        }
    }
}
