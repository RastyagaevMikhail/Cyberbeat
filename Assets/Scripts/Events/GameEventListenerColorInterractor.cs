using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GameEventListenerColorInterractor : AGameEventListenerUnityOject<ColorInterractor>
	{
		[DrawWithUnity]
		[SerializeField] EventListenerColorInterractor _listener;
		protected override AEventListenerUnityObject<ColorInterractor> listener { get { return _listener; } }

	}
}
