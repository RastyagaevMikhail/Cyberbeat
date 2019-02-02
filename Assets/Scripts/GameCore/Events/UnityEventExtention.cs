using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
	[Serializable] public class UnityEventBool : UnityEvent<bool> { }

	[Serializable] public class UnityEventInt : UnityEvent<int> { }

	[Serializable] public class UnityEventFloat : UnityEvent<float> { }

	[Serializable] public class UnityEventString : UnityEvent<string> { }

	[Serializable] public class UnityEventTimeSpan : UnityEvent<TimeSpan> { }

	[Serializable] public class UnityEventDateTime : UnityEvent<DateTime> { }

	[Serializable] public class UnityEventSystemObject : UnityEvent<System.Object> { }

	[Serializable] public class UnityEventColor : UnityEvent<Color> { }

	[Serializable] public class UnityEventVector2 : UnityEvent<Vector2> { }

	[Serializable] public class UnityEventVector3 : UnityEvent<Vector3> { }

	[Serializable] public class UnityEventVector4 : UnityEvent<Vector4> { }

	[Serializable] public class UnityEventUnityObject : UnityEvent<UnityEngine.Object> { }

	[Serializable] public class UnityEventGameObject : UnityEvent<GameObject> { }

	[Serializable] public class UnityEventTransform : UnityEvent<Transform> { }

	[Serializable] public class UnityEventRectTransform : UnityEvent<RectTransform> { }

	[Serializable] public class UnityEventMaterial : UnityEvent<Material> { }

	[Serializable] public class UnityEventScriptableObject : UnityEvent<ScriptableObject> { }

	[Serializable] public class UnityEventSystemAction : UnityEvent<Action> { }

	[Serializable] public class UnityEventBoolVariable : UnityEvent<BoolVariable> { }

	
}
