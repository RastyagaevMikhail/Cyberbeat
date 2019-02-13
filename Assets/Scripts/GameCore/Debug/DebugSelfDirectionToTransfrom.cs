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
		[SerializeField] TransformReference TargetReference;
		Transform target => TargetReference.ValueFast;
		[SerializeField] Color color = Color.green;

		private void OnDrawGizmos ()
		{
			Gizmos.color = color;
			Gizmos.DrawLine (position, target.position);
			Gizmos.DrawCube (target.position, Vector3.one);
		}
	}
}
