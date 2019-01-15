using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class Rotator : TransformObject
    {
        [ContextMenuItem ("Random", "SetAxisRandom")]
        [ContextMenuItem ("forward", "SetAxis_forward")]
        [ContextMenuItem ("back", "SetAxis_back")]
        [ContextMenuItem ("right", "SetAxis_right")]
        [ContextMenuItem ("left", "SetAxis_left")]
        [ContextMenuItem ("up", "SetAxis_up")]
        [ContextMenuItem ("down", "SetAxis_down")]
        public Vector3 axis;
        public float speed = 10f;
        public Space space;

        private void Update ()
        {
            transform.Rotate (speed * Time.deltaTime * axis, space);
        }

        [ContextMenu ("Rotate Random")]
        void SetAxisRandom () { axis = Random.insideUnitSphere; }
        public void SetAxis_forward () { axis = Vector3.forward; }
        public void SetAxis_back () { axis = Vector3.back; }
        public void SetAxis_right () { axis = Vector3.right; }
        public void SetAxis_left () { axis = Vector3.left; }
        public void SetAxis_up () { axis = Vector3.up; }
        public void SetAxis_down () { axis = Vector3.down; }
    }
}
