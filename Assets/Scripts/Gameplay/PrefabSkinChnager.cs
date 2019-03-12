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
        [SerializeField] TransformReference parentReference;
        Transform parent => parentReference.ValueFast;
        void Start ()
        {
            var prefab = selector[type][index.Value].Prefab;

            var instnce = Instantiate (prefab as GameObject, parent);
            instnce.transform.localPosition = Vector3.zero;
        }
    }
}
