using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class PrefabSkinItem : SkinItem
	{
		GameObject prefab { get { return (Prefab as GameObject); } }
		public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
		TransformGroup transGroup = null;
		public override void Apply (Object target, params object[] args)
		{

			bool isApplied = Applyed (out transGroup, target);
			if (!isApplied) return;
			if (transGroup.Contains (prefab)) return;
			GameObject result = Instantiate (prefab, transGroup.transform);
			result.name = prefab.name;
			
			ApplyStateMaterial (result.GetComponent<Renderer> ());

			transGroup.Add (result);
		}

		public void ApplyStateMaterial (Renderer rend, bool Select = false)
		{
			if (Select)
				rend.material = Bougth ? type.OnSeleted : type.OnClosed;
			else
				rend.material = Bougth ? type.OnOpend : type.OnClosed;
		}
	}
}
