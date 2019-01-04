using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public abstract class SkinChnager : MonoBehaviour
    {
        public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
        [SerializeField] protected SkinType type;
        
        void Start ()
        {
            ApplySkin(skinsData.skinsSelector[type][skinsData.skinIndexsSelector[type].Value]);
        }

        protected abstract void ApplySkin(SkinItem skin);
    }
}
