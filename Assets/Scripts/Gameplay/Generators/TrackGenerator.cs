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
        ColorInfoRuntimeSetRuntimeSet ColorsSets => colorsSets.Value;
        static RandomStack<ColorInfoRuntimeSet> ColorsSetsStack = null;
        [SerializeField] PoolVariable pool;
        [SerializeField] TracksCollection tracksCollection;

        RandomConstantMaterial rcm = null;

        List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        [SerializeField] ColorInfoRuntimeSetVariable currentSet;
        [SerializeField] ColorInterractorRuntimeSet nearBeats;
        [SerializeField] UnityEventColor onFirstColorChoosed;
        [SerializeField] UnityEventString onPresetChoosed;
        ColorInfoRuntimeSet CurrentSet { get => currentSet.Value; set => currentSet.Value = value; }
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
            if (rcm == null && CurrentSet != null)
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

            string randPreset = bitData.RandomString.Regex (@"\d");

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
            onPresetChoosed.Invoke (randPreset);

            float half_width = width / 2;
            float step = half_width / 2;

            for (float x = -half_width, i = 0; x <= half_width; x += step, i++)
            {
                var spawnObj = InstattiateObj (row[(int) i], x);
                if (spawnObj == null) continue;
                ColorInterractor colorInterractor = spawnObj.Get<ColorInterractor> ();
                if (debug) Debug.Log ($"{("Before").red()}\n" + colors.Log ());
                if (colors.ContainsKey (bitTime))
                {
                    if (debug) Debug.Log (colors[bitTime].Log ());
                    colors[bitTime].Add (colorInterractor);
                    if (debug) Debug.Log (colors[bitTime].Log ());
                }
                else
                    colors.Add (bitTime, new List<ColorInterractor> { colorInterractor });

                if (debug) Debug.Log (colors[bitTime].Log ());
                if (debug) Debug.Log ($"{("After").green()}\n" + colors.Log ());

                colorInterractor.Init (bitTime, bitData.RandomString.Regex (@"[A-Z]"));
            }
            if (nearBeats.Count == 0)
                foreach (var color in colors[bitTime])
                    nearBeats.Add (color);
        }

        Dictionary<float, List<ColorInterractor>> colors = new Dictionary<float, List<ColorInterractor>> ();
        [SerializeField] bool debug;

        public void OnPlayerBit (IBitData bitData)
        {
            if (debug)
            {
                Debug.Log (colors.Log ());
                Debug.Log (bitData.StartTime);
            }
            if (colors.ContainsKey (bitData.StartTime))
            {
                nearBeats.Clear ();

                colors.Remove (bitData.StartTime);

                if (colors.Count == 0) return;

                float keyOfNextTime = colors.Keys.Min ();

                List<ColorInterractor> listColorInterractors = colors[keyOfNextTime];

                foreach (var color in listColorInterractors)
                    nearBeats.Add (color);

            }
        }
        private void OnDisable ()
        {
            nearBeats.Clear ();
        }

        private void initColors ()
        {
            if (CurrentSet == null)
            {
                CurrentSet = ColorsSetsStack.Get ();
                if (debug) Debug.Log (CurrentSet);
            }
            if (LastRandomColor == default (Color) && CurrentSet != null)
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

            ColorInterractor colorInterractor = obj.Get<ColorInterractor> ();
            if (colorInterractor.IsSwitcher)
                InitRCM ();

            var matSwitcher = colorInterractor.matSwitch;
            if (matSwitcher)
            {
                bool constant = matSwitcher.Constant;

                Material material = constant ? rcm.Constant[baseMaterial] : rcm.RandomSet[baseMaterial].Get ();

                matSwitcher.SetMaterial (material);

                if (!firstColorChoosed)
                {
                    firstColorChoosed = true;
                    onFirstColorChoosed.Invoke (material.GetColor (matSwitcher.DefaultColorName));
                }

                if (!constant)
                    LastRandomColor = material.GetColor (matSwitcher.DefaultColorName);
            }
            obj.OnSpawn (Key);
            return obj;
        }
    }
}
