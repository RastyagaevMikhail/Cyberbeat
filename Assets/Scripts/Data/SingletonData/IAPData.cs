using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class IAPData : SingletonData<IAPData>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/IAPData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate () { }
		public override void ResetDefault () { }

		[Button] public void CreatProducts ()
		{
			var Upgrades = Tools.GetAtPath<UpgradeData> ("Assets/Data/UpgradeData");
			foreach (var ud in Upgrades)
			{
				ud.GenerateProducts ();
			}
		}
#endif
	}
}
