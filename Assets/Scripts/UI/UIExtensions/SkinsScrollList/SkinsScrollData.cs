using UnityEngine;

namespace CyberBeat
{
    public class SkinsScrollData
    {

        public Object Prefab;
        public Sprite Icon;
        public SkinItem skin;
        public SkinsScrollData (SkinItem item)
        {
            this.Prefab = item.Prefab;
            this.Icon = item.Icon;
            skin = item;
        }

        public SkinsScrollData (Object prefab, Sprite icon)
        {
            Prefab = prefab;
            Icon = icon;
        }

    }
}