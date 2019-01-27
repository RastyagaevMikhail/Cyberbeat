using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class PoolComponentHelper : TransformObject
    {
        [SerializeField] PoolVariable pool;

        [SerializeField] bool savePositionInLoacal;
        Vector3 loacalPositoin;
        [SerializeField] TransformReference parent;

        public void Pop (string key)
        {
            bool CanLocal = savePositionInLoacal && parent != null;
            if (CanLocal)
                loacalPositoin = parent.ValueFast.InverseTransformPoint (position);
            
            var obj = pool.Pop (key, parent.ValueFast);

            if (CanLocal) obj.position = localPosition;
        }
    }
}
