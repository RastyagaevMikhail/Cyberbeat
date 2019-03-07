using GameCore;

using System;

using UnityEngine;

namespace FluffyUnderware.Curvy
{
    public class RigidbodyForcePositionMover : APositionMover
    {
        [SerializeField] RigidbodyVariable target;
        public Rigidbody Target => target;
        public override void MovePosition (Vector3 position, Space space = Space.World)
        {
            if (space == Space.World)
            {
                Target.MovePosition (position);
            }
            // else
            // Target.MovePosition (Target.position + Target.transform.TransformDirection (position));
        }
    }
}
