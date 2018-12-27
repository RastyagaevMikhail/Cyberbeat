using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class TransformSelector<T> : AEnumDataSelectorMonoBehaviour<T, Transform> where T : EnumScriptable
	{

	}
}
