using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class PrefabSkinChnager : MonoBehaviour
    {
        [SerializeField] SkinType type;
        [SerializeField] IntVariable index;
        [SerializeField] SkinsEnumDataSelector selector;
        void Start ()
        {
            var prefab = selector[type][index.ValueFast].Prefab;
            var instnce = Instantiate (prefab as GameObject, transform);
            instnce.transform.localPosition = Vector3.zero;
        }
    }
}
