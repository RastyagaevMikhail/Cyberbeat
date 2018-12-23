using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class ColorInterractor : Interractor
    {

        protected MaterialSwitcher _matSwitch = null;
        public virtual MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponent<MaterialSwitcher>(); } return _matSwitch; } }
        protected Player player { get { return Player.instance; } }
        Pool pool { get { return Pool.instance; } }
        public float bit;

        public virtual void OnPlayerContact(GameObject go)
        {

        }

        [SerializeField] protected GameEventColorInterractor OnDeathCollorInterractor;
        [SerializeField] protected GameEventColor OnColorTeked;
        [SerializeField] string deathParticles_poolKey = "getColor";
        [SerializeField] bool PushOnDeath;

        public void Death()
        {
            // Debug.LogFormat (this,"OnDeathCollorInterractor = {0}", OnDeathCollorInterractor);
            OnDeathCollorInterractor.Raise(this);

            SpawnedObject spawnedObject = pool.Pop(deathParticles_poolKey, player.transform);
            if (!spawnedObject)
            {
                return;
            }

            Color MyColor = matSwitch.CurrentColor;

            OnColorTeked.Raise(MyColor);
            if(PushOnDeath)
                pool.Push(gameObject); //!!!Push On End animation  "Scale"

        }

    }
}
