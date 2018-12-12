using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore.DebugTools
{
	using GameCore;

	using System;
	[ExecuteInEditMode]
	public class DebugSelfDirectionToTransfrom : TransformObject
	{
		[SerializeField] Transform Target;
		[SerializeField] Color color = Color.green;

		private void OnDrawGizmos ()
		{
			Gizmos.color = color;
			Gizmos.DrawLine (position, Target.position);
			Gizmos.DrawCube (Target.position, Vector3.one);
		}

		public void SetTarget (Transform newTarget)
		{
			Target = newTarget;
		}
	}
}
