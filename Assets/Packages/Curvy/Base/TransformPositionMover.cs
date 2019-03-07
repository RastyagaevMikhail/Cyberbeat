using System;
using GameCore;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[CreateAssetMenu(menuName = "FluffyUnderware/Curvy/PositionMover/Transform")]
	public class TransformPositionMover : APositionMover
	{
		[SerializeField] TransformVariable target;
		public Transform Target => target;
		public override void MovePosition (Vector3 position, Space space = Space.World)
		{
			if (space == Space.Self)
				Target.localPosition = position;
			else
				Target.position = position;
		}
	}
}
