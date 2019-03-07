using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public interface IPositionMover
	{
		void MovePosition (Vector3 position, Space space = Space.World);
	}
}
