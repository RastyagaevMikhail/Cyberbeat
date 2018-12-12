using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class TransformSelector<T> : EnumDataSelector<T, Transform> where T : EnumScriptable
	{

	}

}
