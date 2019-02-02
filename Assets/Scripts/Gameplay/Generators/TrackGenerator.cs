using GameCore;

using Sirenix.OdinInspector;

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
        [SerializeField] List<ColorInfoRuntimeSet> ColorsSets;
        static RandomStack<ColorInfoRuntimeSet> ColorsSetsStack = null;
        [SerializeField] PoolVariable pool;
        [SerializeField] TracksCollection tracksCollection;

        RandomConstantMaterial rcm = null;

        List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        [SerializeField] ColorInfoRuntimeSetVariable currentSet;
        ColorInfoRuntimeSet CurrentSet { get => currentSet.ValueFast; set => currentSet.ValueFast = value; }
        Color LastRandomColor = default (Color);
        float lastBeat = -1f;
        protected override void Awake ()
        {
            base.Awake ();
            if (ColorsSetsStack == null) ColorsSetsStack = new RandomStack<ColorInfoRuntimeSet> (ColorsSets);
            InitRCM ();
            lastBeat = -1f;
        }

        private void InitRCM ()
        {
            initColors ();
            if (rcm == null)
                rcm = new RandomConstantMaterial (CurrentSet.GetColors ());

            rcm.SetLastRandomColor (LastRandomColor);
        }

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
            }
        }

        private void initColors ()
        {
            if (CurrentSet == null) CurrentSet = ColorsSetsStack.Get ();
            if (LastRandomColor == default (Color)) LastRandomColor = CurrentSet.GetRandom ().color;
        }
        private void OnDestroy ()
        {
            CurrentSet = null;
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
