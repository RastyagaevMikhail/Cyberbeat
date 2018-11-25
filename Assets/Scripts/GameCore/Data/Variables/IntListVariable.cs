using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	using Sirenix.OdinInspector;

	using UnityEngine;

	[CreateAssetMenu (fileName = "IntList", menuName = "Variables/GameCore/IntList", order = 0)]
	public class IntListVariable : SavableVariable<List<int>>
	{
		public override void SaveValue () { }
		public override void LoadValue () { }
		public int this [int index]
		{
			get { return Value[index]; }
			set { Value[index] = value; }
		}
	}
}
