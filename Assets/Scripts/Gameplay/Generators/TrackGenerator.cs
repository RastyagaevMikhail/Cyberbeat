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
        [SerializeField] ColorInfoRuntimeSetRuntimeSetVariable colorsSets;
        ColorInfoRuntimeSetRuntimeSet ColorsSets => colorsSets.ValueFast;
        static RandomStack<ColorInfoRuntimeSet> ColorsSetsStack = null;
        [SerializeField] PoolVariable pool;
        [SerializeField] TracksCollection tracksCollection;

        RandomConstantMaterial rcm = null;

        List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        [SerializeField] ColorInfoRuntimeSetVariable currentSet;
        [SerializeField] GameEventColor onFirstColorChoosed;
        [SerializeField] GameEventIBitData onPresetChoosed;
        ColorInfoRuntimeSet CurrentSet { get => currentSet.ValueFast; set => currentSet.ValueFast = value; }
        Color LastRandomColor = default (Color);
        float lastBeat = -1f;
        bool firstColorChoosed = false;
        protected override void Awake ()
        {
            base.Awake ();
            if (ColorsSetsStack == null) ColorsSetsStack = new RandomStack<ColorInfoRuntimeSet> (ColorsSets.ToArray ());
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

            string randomString = bitData.RandomString;
            onPresetChoosed.Raise (new PresetBitInfo (bitData.StartTime, randomString));
            string[] randomSplitedString = randomString.Split ("/".ToCharArray ());

            string randPreset = randomSplitedString[0];

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
            if (CurrentSet == null)
            {
                CurrentSet = ColorsSetsStack.Get ();

            }
            if (LastRandomColor == default (Color))
            {
                LastRandomColor = CurrentSet.GetRandom ().color;

            }
        }
        private void OnDestroy ()
        {
            CurrentSet = null;
            firstColorChoosed = false;
        }
        SpawnedObject InstattiateObj (Material baseMaterial, float xPos)
        {
            if (!baseMaterial) return null;
            string Key = baseMaterial.name;
            var obj = pool.Pop (Key, null);
            if (!obj) return null;

            obj.position = position + right * xPos + up * obj.y;
            obj.rotation = rotation;

            if (obj.Get<ColorInterractor> ().CurrentInfo.isSwitcher)
                InitRCM ();

            var matSwitcher = obj.Get<MaterialSwitcher> ();
            if (matSwitcher)
            {
                bool constant = matSwitcher.Constant;

                Material material = constant ? rcm.Constant[baseMaterial] : rcm.RandomSet[baseMaterial].Get ();

                matSwitcher.SetMaterial (material);

                if (!firstColorChoosed)
                {
                    firstColorChoosed = true;
                    onFirstColorChoosed.Raise (material.GetColor (matSwitcher.DefaultColorName));
                }

                if (!constant)
                    LastRandomColor = material.GetColor (matSwitcher.DefaultColorName);
            }
            obj.OnSpawn (Key);
            return obj;
        }
    }
    public class PresetBitInfo : IBitData
    {
        private readonly float startTime;
        private readonly string stringValue;

        public PresetBitInfo (float startTime, string stringValue)
        {
            this.startTime = startTime;
            this.stringValue = stringValue;
        }

        public float StartTime => startTime;

        public float EndTime =>
            throw new NotImplementedException ();

        public float Duration =>
            throw new NotImplementedException ();

        public IPayloadData PayloadData =>
            throw new NotImplementedException ();

        public string RandomString =>
            throw new NotImplementedException ();

        public string StringValue => stringValue;

        public string[] Strings =>
            throw new NotImplementedException ();

        public void Init (KoreographyEvent koreographyEvent)
        {
            throw new NotImplementedException ();
        }
    }
}
