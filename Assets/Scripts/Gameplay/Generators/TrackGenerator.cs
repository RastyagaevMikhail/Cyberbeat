using GameCore;

using SonicBloom.Koreo;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class TrackGenerator : TransformObject
    {
        private const float width = 5f;
        [SerializeField] PoolVariable pool;
        [SerializeField] TracksCollection tracksCollection;

        RandomConstantMaterial rcm;

        List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        protected override void Awake ()
        {
            base.Awake ();
            LastRandomColor = Colors.instance.RandomColor;

            InitRCM ();
            lastBeat = -1f;
        }

        private void InitRCM ()
        {
            rcm = new RandomConstantMaterial (LastRandomColor);
        }
        float lastBeat = -1f;
        [SerializeField] Color LastRandomColor;
        public void OnBit (IBitData bitData)
        {

            float bitTime = bitData.StartTime;

            if (lastBeat >= bitTime)
                return;
            else
                lastBeat = bitTime;

            string randPreset = bitData.RandomString;
            List<Material> row = null;
            try
            {
                row = tracksCollection.Presets[randPreset];
            }
            catch (System.Exception)
            {
                Debug.LogFormat ("randPreset = {0}", randPreset);
                throw;
            }

            float half_width = width / 2;
            float step = half_width / 2;

            for (float x = -half_width, i = 0; x <= half_width; x += step, i++)
            {
                var spawnObj = InstattiateObj (row[(int) i], x);
                if (spawnObj == null) continue;

                ColorInterractor colorInterractor = spawnObj.Get<ColorInterractor> ();

                colorInterractor.Init (bitTime);

                // Neighbors.Add (colorInterractor);

#if UNITY_EDITOR
                string metadata = string.Format ("{0}", bitTime);
                spawnObj.Get<MetaDataGizmos> ().MetaData = Tools.LogTextInColor (metadata, Color.blue);
#endif

            }

            // if (Neighbors.Count > 1)
            // {
            //     foreach (var neighbor in Neighbors)
            //         foreach (var n in Neighbors)
            //             neighbor.AddNeighbor (n);
            // }
            // Neighbors.Clear ();
        }

        SpawnedObject InstattiateObj (Material baseMaterial, float xPos)
        {
            if (!baseMaterial) return null;
            string Key = baseMaterial.name;
            var obj = pool.Pop (Key);
            if (!obj) return null;

            obj.position = position + right * xPos + up * obj.y;
            obj.rotation = rotation;

            if (obj.Get<ColorSwitcher> ())
                InitRCM ();

            var matSwitcher = obj.Get<MaterialSwitcher> ();
            if (matSwitcher)
            {
                Material ConstantMaterial = rcm.Constant[baseMaterial];
                Material RandomMaterial = rcm.RandomSet[baseMaterial].Get ();
                matSwitcher.SetMaterial (matSwitcher.Constant ? ConstantMaterial : RandomMaterial);
                LastRandomColor = RandomMaterial.GetColor (matSwitcher.DefaultColorName);
            }
            return obj;
        }
    }
}
