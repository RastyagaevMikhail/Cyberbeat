using GameCore;

using UnityEngine;
namespace CyberBeat
{
	public abstract class BotInputController : ScriptableObject
	{
		[SerializeField] protected MaterialSwitcherVariable matSwitch;
		[SerializeField] protected FloatVariable startSplineSpeedVariable;
		[SerializeField] protected float RadiusShpereCast = 1f;
		[SerializeField] protected bool bitAll;
		[SerializeField] protected ColorInterractorRuntimeSet nearBeats;
		public bool StartMove = false;
		public abstract void ForEachNear (ColorInterractor colorInterractor);
		
		public abstract void Initialize (Transform target);
		public abstract void UpdatePosition ();
		public abstract float Speed { set; }
	}
}
