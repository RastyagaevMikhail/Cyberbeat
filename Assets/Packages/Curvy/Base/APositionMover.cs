// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[System.Serializable]
	public abstract class APositionMover : ScriptableObject, IPositionMover
	{
		public abstract void MovePosition (Vector3 position, Space space = Space.World);
	}
}
