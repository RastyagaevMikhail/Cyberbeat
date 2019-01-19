using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class PoolComponentHelper : MonoBehaviour
    {
        public Pool pool { get { return Pool.instance; } }

        [SerializeField] TransformReference parnet;

        public void Pop (string key)
        {
            pool.Pop (key, parnet);
        }
    }
}
