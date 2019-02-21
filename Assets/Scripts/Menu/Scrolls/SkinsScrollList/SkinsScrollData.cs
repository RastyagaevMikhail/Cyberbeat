using GameCore;

using UnityEngine;

namespace CyberBeat
{
    public class SkinsScrollData : IDataItem
    {
        public SkinItem skin;
        public SkinsScrollData (SkinItem item)
        {
            skin = item;
        }
        public void InitViewGameObject (GameObject go) { }
    }
}
