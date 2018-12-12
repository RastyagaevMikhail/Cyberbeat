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
        public virtual MaterialSwitcher matSwitch
        {
            get
            {
                if (_matSwitch == null) _matSwitch = GetComponent<MaterialSwitcher> ();
                return _matSwitch;
            }
        }

        public float bit;
        public float Prevbit;
        [SerializeField] Vector3 StartPosition;

        Pool pool { get { return Pool.instance; } }
        protected Player player { get { return Player.instance; } }

        public virtual void OnPlayerContact (GameObject go)
        {

        }
        private void OnEnable ()
        {
            StartPosition = transform.position;
        }

        [SerializeField] protected GameEventObject OnDeathCollorInterractor;
        [SerializeField] protected GameEventObject OnColorTeked;
        [SerializeField] protected ColorVariable TakedColor;
        public virtual void Death ()
        {
            // Debug.LogFormat (this,"OnDeathCollorInterractor = {0}", OnDeathCollorInterractor);
            OnDeathCollorInterractor.Raise (this);

            SpawnedObject spawnedObject = pool.Pop ("DeathPatrts");
            if (!spawnedObject) return;

            var particles = spawnedObject.Get<MaterialSwitcher> ();


            player.transform.SetParent (player.transform);
            particles.transform.position = player.position + player.transform.forward * 2f ;

            Color MyColor = matSwitch.CurrentColor;
            particles.SetColor (MyColor);

            TakedColor.SetValue (MyColor);
            OnColorTeked.Raise (TakedColor);
            pool.Push (gameObject);
            transform.position = StartPosition;
        }

    }
}
