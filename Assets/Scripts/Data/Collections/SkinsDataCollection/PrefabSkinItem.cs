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
			if (transGroup.Contains (prefab.gameObject)) return;
			SkinComponent skinPrefab = prefab.GetComponent<SkinComponent> ();
			SkinComponent result = Instantiate (skinPrefab, transGroup.transform);
			result.name = prefab.name;

			ApplyStateMaterial (result);

			transGroup.Add (result.gameObject);
		}

		public void ApplyStateMaterial (SkinComponent rend, bool Select = false)
		{
			if (Select)
				rend.Renderer.material = Bougth ? type.OnSeleted : type.OnClosed;
			else
				rend.Renderer.material = Bougth ? type.OnOpend : type.OnClosed;
		}
	}
}
