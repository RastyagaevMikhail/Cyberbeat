using System;

using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

using GameCore;
namespace CyberBeat
{
    public abstract class SkinItem : EnumScriptable
    {
        [Header ("Icon")]
        public Sprite Icon;
        [Header ("Prefab")]
        public Object Prefab;
        public int Price;

        public void ResetDefault ()
        {
            Bougth = Price == 0;
            getByVideo = false;
        }

        string bouthSaveKey { get { return "{0}.Bougth".AsFormat (name); } }

        public bool Bougth { get { return Tools.GetBool (bouthSaveKey); } set { Tools.SetBool (bouthSaveKey, value); } }
        public bool getByVideo;
        [SerializeField] protected SkinType type;
        public void InitOnCreate (Sprite icon, Object obj, int price, SkinType type)
        {
            Icon = icon;
            Prefab = obj;
            Price = price;
            Bougth = (Price == 0);
            this.type = type;
        }
        public GameData gameData { get { return GameData.instance; } }
        public bool TryBuy () { return Bougth = gameData.TryBuy (Price); }
        public bool IsAvalivable { get { return (Bougth || getByVideo); } }
        public bool CanBuy { get { return gameData.CanBuy (Price); } }

        public abstract void Apply (Object target, params object[] args);
        public bool Applyed<T> (out T target, Object targetObject) where T : UnityEngine.Object
        {
            target = targetObject as T;

            if (target == null)
            {
                GameObject gameObject = (targetObject as GameObject);
                if (!gameObject) return false;
                target = gameObject.GetComponent<T> ();
            }

            return target;
        }
    }

}
