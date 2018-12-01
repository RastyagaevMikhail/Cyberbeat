using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;
using GameCore;
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
        public virtual void OnSpawn ()
        {

        }
        public virtual void OnPlayerContact ()
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

            var particles = spawnedObject.GetComponent<MaterialSwitcher> ();

            particles.transform.position = transform.position + transform.forward * 2f;

            const string colorName = "_TintColor";
            Material sharedMaterial = particles.partRenderer.sharedMaterial;
            Color MyColor = matSwitch.CurrentColor;
            sharedMaterial.SetColor (colorName, MyColor);

            TakedColor.Value = MyColor;
            OnColorTeked.Raise (TakedColor);
            pool.Push (gameObject);
            transform.position = StartPosition;
        }
    }
}
