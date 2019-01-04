using GameCore;

using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public abstract class ColorInterractor : Interractor
    {
        protected Animator _animator = null;
        public virtual Animator animator { get { if (_animator == null) { _animator = GetComponent<Animator> (); } return _animator; } }
        private Collider _collider = null;
        new public Collider collider { get { if (_collider == null) _collider = GetComponent<Collider> (); return _collider; } }
        protected MaterialSwitcher _matSwitch = null;
        public virtual MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponent<MaterialSwitcher> (); } return _matSwitch; } }
        protected Player player { get { return Player.instance; } }
        Pool pool { get { return Pool.instance; } }
        public float bit;

        private void OnValidate ()
        {
            DeathHashState = Animator.StringToHash ("Death");
        }

        public abstract void OnPlayerContact ();
        [SerializeField] protected GameEventColorInterractor OnDeathCollorInterractor;
        [SerializeField] protected GameEventColor OnColorTeked;
        [SerializeField] string deathParticles_poolKey = "getColor";
        [SerializeField] bool PushOnDeath;
        [SerializeField] bool DeathParticlesOnMe = true;
        public List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        [SerializeField] int DeathHashState;

        public virtual void Death ()
        {

            // Debug.LogFormat (this, "OnDeathCollorInterractor = {0}", this);
            OnDeathCollorInterractor.Raise (this);

            OnDeSpawn ();
            PlayDeathAnimation ();
            SpawnedObject spawnedObject = pool.Pop (deathParticles_poolKey, DeathParticlesOnMe ? transform : player.transform);
            if (!spawnedObject)
            {
                return;
            }

            Color MyColor = matSwitch.CurrentColor;

            OnColorTeked.Raise (MyColor);
            if (PushOnDeath)
                pool.Push (gameObject); //!!!Push On End animation  "Scale"

        }

        private void PlayDeathAnimation ()
        {
            bool HasState = animator.HasState (0, DeathHashState);
            if (HasState)
                animator.Play (DeathHashState);
        }
        public void OnSpawn ()
        {
            collider.enabled = true;
        }
        public virtual void OnDeSpawn ()
        {
            Neighbors.Clear ();
            collider.enabled = false;
        }
    }
}
