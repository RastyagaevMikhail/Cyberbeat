using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public class MaterialSwitcher : MonoBehaviour
	{
		public Material CurrentMaterial { get { return renderer.sharedMaterial; } set { renderer.sharedMaterial = value; } }

		public Color CurrentColor { get { return this [DefaultColorName]; } set { this [DefaultColorName] = value; OnColorChanged.Invoke (value); } }
		public Color this [string colorName]
		{
			get
			{

				Color color = Color.white;
				if (usePropertyMaterialBlock)
				{
					renderer.GetPropertyBlock (matProp);
					color = matProp.GetColor (ColorNameHash);
				}
				else
				{
					color = CurrentMaterial.GetColor (ColorNameHash);
				}
				return color;
			}
			set
			{
				if (usePropertyMaterialBlock)
				{
					ValidatePropertyBlock ();
					renderer.GetPropertyBlock (matProp);
					matProp.SetColor (ColorNameHash, value);
					renderer.SetPropertyBlock (matProp);
				}
				else
				{
					CurrentMaterial.SetColor (ColorNameHash, value);
				}
			}
		}
		private Renderer _renderer = null;
		public new Renderer renderer { get { if (_renderer == null) _renderer = GetComponent<Renderer> (); return _renderer; } }
		public bool Constant = true;
		public bool newMaterialOnAwake;
		[SerializeField] StringVariable DefaultColorNameVariable;
		const string defaultColorName = "_Color";
		public string DefaultColorName { get { return DefaultColorNameVariable?DefaultColorNameVariable.Value : defaultColorName; } }

		[SerializeField] bool usePropertyMaterialBlock;
		MaterialPropertyBlock matProp;

		[SerializeField] UnityEventColor OnColorChanged;
		[SerializeField] int ColorNameHash = 0;
		private void OnValidate ()
		{
			ColorNameHash = Shader.PropertyToID (DefaultColorName);
		}
		private void Awake ()
		{
			ValidatePropertyBlock ();

			if (newMaterialOnAwake)
			{
				CurrentMaterial = Instantiate (CurrentMaterial);
			}
		}

		private void ValidatePropertyBlock ()
		{
			if (usePropertyMaterialBlock)
				matProp = new MaterialPropertyBlock ();
		}

		public void SetMyColorTo (MaterialSwitcher materialSwitcher)
		{
			materialSwitcher.CurrentColor = (CurrentColor);
		}
		public bool ChechColor (Color otherColor)
		{
			// return CurrentColor.Equals (otherColor);
			return
			CurrentColor.r == otherColor.r &&
				CurrentColor.g == otherColor.g &&
				CurrentColor.b == otherColor.b;
		}
		public void SetColor (ColorVariable variable)
		{
			CurrentColor = variable.Value;
		}
	}
}
