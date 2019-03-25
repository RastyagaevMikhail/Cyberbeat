using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SkinRaodSetter : MonoBehaviour
    {
        [SerializeField] Material material;
        [SerializeField] SkinType skinType;
        [SerializeField] SkinsEnumDataSelector selector;
        [SerializeField] IntVariable skinIndex;

        private void Start ()
        {
            material.SetTexture ("_EmissionMap", selector[skinType][skinIndex].Prefab as Texture);
        }
    }
}
