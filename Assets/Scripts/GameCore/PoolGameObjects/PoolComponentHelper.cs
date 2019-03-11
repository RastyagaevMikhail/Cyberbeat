using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class PoolComponentHelper : TransformObject
    {
        [SerializeField] PoolVariable pool;
        
        [SerializeField] TransformReference parent;
        public void Pop (string key)
        {           
            if(key.IsNullOrEmpty()) return;
            pool.Pop (key, parent.ValueFast);
        }
    }
}
