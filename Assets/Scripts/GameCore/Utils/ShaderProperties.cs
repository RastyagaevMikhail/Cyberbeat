using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameCore
{

	[System.Serializable]
	public class ShaderProperties
	{
		public List<string> value;
		[SerializeField] ShaderProperyTypes types;
		[ValueDropdown ("value")]
		public string currentValue;
#if UNITY_EDITOR
		public void OnValidate ()
		{
			if (shader)
				SetType (types);
		}
		private Dictionary<ShaderUtil.ShaderPropertyType, List<string>> values;
		public ShaderProperties (Shader shader)
		{
			Init (shader);
		}
		public ShaderProperties (Shader shader, ShaderProperyTypes typeProperties = ShaderProperyTypes._All)
		{
			Init (shader, typeProperties);
		}
		public ShaderProperties (Shader shader, int typeProperties = 0)
		{
			ShaderProperyTypes _typeProperties = (ShaderProperyTypes) typeProperties;
			Init (shader, _typeProperties);
		}
		void Init (Shader shader, ShaderProperyTypes types = ShaderProperyTypes._All)
		{
			this.shader = shader;
			initValues ();
			SetType (types);
			currentValue = value.First ();

		}

		private void SetType (ShaderProperyTypes types)
		{
			switch (types)
			{
				case ShaderProperyTypes._Color:
					initValue (ShaderUtil.ShaderPropertyType.Color);
					break;
				case ShaderProperyTypes._Texture:
					initValue (ShaderUtil.ShaderPropertyType.TexEnv);
					break;
				case ShaderProperyTypes._Float:
					initValue (ShaderUtil.ShaderPropertyType.Float);
					break;
				case ShaderProperyTypes._Vector:
					initValue (ShaderUtil.ShaderPropertyType.Vector);
					break;
				case ShaderProperyTypes._Range:
					initValue (ShaderUtil.ShaderPropertyType.Range);
					break;
				default:
					if (values == null) initValues ();
					value = values.Values.SelectMany (x => x).ToList ();
					break;
			}
		}

		[SerializeField] Shader shader;
		void initValue (ShaderUtil.ShaderPropertyType type)
		{
			if (values == null) initValues ();
			if (values.ContainsKey (type))
				value = values[type];
		}

		private void initValues ()
		{
			values = new Dictionary<ShaderUtil.ShaderPropertyType, List<string>> ();
			int countProps = ShaderUtil.GetPropertyCount (shader);
			var names = Enumerable.Range (0, countProps)
				.Select (i => ShaderUtil.GetPropertyName (shader, i)).ToList ();
			for (int i = 0; i < names.Count; i++)
			{
				var type = ShaderUtil.GetPropertyType (shader, i);
				if (!values.ContainsKey (type))
					values[type] = new List<string> ();
				values[type].Add (names[i]);
			}
		}

		public ShaderProperties (List<string> _values)
		{
			value = _values;
		}

		public static implicit operator ShaderProperties (List<string> value)
		{
			return new ShaderProperties (value);
		}

		public static implicit operator List<string> (ShaderProperties value)
		{
			return value.value;
		}
		public static implicit operator ShaderProperties (string value)
		{
			return new ShaderProperties (new List<string> () { value });
		}

		public static implicit operator string (ShaderProperties value)
		{
			return value.currentValue;
		}
#endif

	}

	public enum ShaderProperyTypes
	{
		_All = 0,
		_Color = 1,
		_Texture = 2,
		_Float = 3,
		_Vector = 4,
		_Range = 5
	}
}
