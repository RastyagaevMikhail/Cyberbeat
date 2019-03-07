// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using GameCore;

using System;

using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[CreateAssetMenu (menuName = "FluffyUnderware/Curvy/PositionMover/Rigidbody/MovePosition")]
	public class RigidbodyPositionMover : APositionMover
	{
		[SerializeField] RigidbodyVariable target;
		public Rigidbody Target => target;

		public override void MovePosition (Vector3 position, Space space)
		{
			if (space == Space.Self)
				Target.MovePosition (Target.position + Target.transform.TransformDirection (position));
			else
				Target.MovePosition (position);
		}
	}
}
