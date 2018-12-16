 using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class ColorInterractor : Interractor
    {

        protected MaterialSwitcher _matSwitch = null;
        public virtual MaterialSwitcher matSwitch { get { if (_matSwitch == null) _matSwitch = GetComponent<MaterialSwitcher> (); return _matSwitch; } }
        protected Player player { get { return Player.instance; } }
        Pool pool { get { return Pool.instance; } }
        public float bit;

        public virtual void OnPlayerContact (GameObject go)
        {

        }

        [SerializeField] protected GameEventColorInterractor OnDeathCollorInterractor;
        [SerializeField] protected GameEventColor OnColorTeked;
        public void Death ()
        {
            // Debug.LogFormat (this,"OnDeathCollorInterractor = {0}", OnDeathCollorInterractor);
            OnDeathCollorInterractor.Raise (this);

            SpawnedObject spawnedObject = pool.Pop ("DeathPatrts");
            if (!spawnedObject) return;

            var particles = spawnedObject.Get<MaterialSwitcher> ();

            // particles.transform.SetParent (player.transform);
            particles.transform.position = position + transform.forward * 5f;

            Color MyColor = matSwitch.CurrentColor;
            particles.SetColor (MyColor);

            OnColorTeked.Raise (MyColor);
            pool.Push (gameObject);

        }

    }
}
