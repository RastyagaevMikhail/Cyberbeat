using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class PoolComponentHelper : MonoBehaviour
    {
        [SerializeField] PoolVariable pool;

        [SerializeField] TransformReference parnet;

        public void Pop (string key)
        {
            pool.Pop (key, parnet.ValueFast);
        }
    }
}
