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
        [SerializeField] StringMaterialsSelector Presets;

        [SerializeField] ColorInfoRuntimeSetVariable currentSet;
        [SerializeField] ColorInterractorRuntimeSet nearBeats;
        [SerializeField] UnityEventColor onFirstColorChoosed;
        [SerializeField] UnityEventString onPresetChoosed;
        ColorInfoRuntimeSet CurrentSet { get => currentSet.Value; set => currentSet.Value = value; }
        Color LastRandomColor = default (Color);
        float lastBeat = -1f;
        bool firstColorChoosed = false;
        Color ConstantColor;
        RandomStack<Color> ColorsStack;
        Dictionary<float, List<ColorInterractor>> colorsByBit = new Dictionary<float, List<ColorInterractor>> ();
        [SerializeField] bool debug;
        void Awake ()
        {
            if (ColorsSetsStack == null) ColorsSetsStack = new RandomStack<ColorInfoRuntimeSet> (ColorsSets.ToArray ());
            CurrentSet = ColorsSets.GetRandom ();
            UpdateConstantColor ();
            lastBeat = -1f;
        }

        private void UpdateConstantColor ()
        {
            var colors = CurrentSet.ToArray ().Select (ci => ci.color).ToList ();
            ConstantColor = colors.GetRandom ();
            colors.Remove (ConstantColor);
            ColorsStack = new RandomStack<Color> (colors);
        }

        public void OnBit (IBitData bitData)
        {

            float bitTime = bitData.StartTime;

            if (lastBeat >= bitTime)
                return;
            else
                lastBeat = bitTime;

            string randPreset = bitData.RandomString.Regex (@"\d*");

            List<Material> row = null;
            try
            {
                row = Presets[randPreset];
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
                Material material = row[(int) i];
                if (material == null) continue;
                SpawnedObject spawnObj = InstatiateObject (x, material.name);

                ColorInterractor colorInterractor = spawnObj.Get<ColorInterractor> ();

                SetColor (colorInterractor);

                AddToNeatBeats (bitTime, colorInterractor);

                colorInterractor.Init (bitTime, bitData.RandomString.Regex (@"[A-Z]"));
            }
            if (nearBeats.Count == 0)
                foreach (var color in colorsByBit[bitTime])
                    nearBeats.Add (color);
        }
        private SpawnedObject InstatiateObject (float xPos, string Key)
        {
            var obj = pool.Pop (Key, null);

            if (obj == null) return null;

            obj.position = position + right * xPos + up * obj.y;
            obj.rotation = rotation;
            obj.OnSpawn (Key);
            return obj;
        }
        private void SetColor (ColorInterractor colorInterractor)
        {
            if (colorInterractor.IsSwitcher)
                UpdateConstantColor ();

            var matSwitcher = colorInterractor.matSwitch;

            matSwitcher.CurrentColor = matSwitcher.Constant ? ConstantColor : ColorsStack.Get ();
        }

        private void AddToNeatBeats (float bitTime, ColorInterractor colorInterractor)
        {
            if (debug) Debug.Log ($"{("Before").red()}\n" + colorsByBit.Log ());
            if (colorsByBit.ContainsKey (bitTime))
            {
                if (debug) Debug.Log (colorsByBit[bitTime].Log ());
                colorsByBit[bitTime].Add (colorInterractor);
                if (debug) Debug.Log (colorsByBit[bitTime].Log ());
            }
            else
                colorsByBit.Add (bitTime, new List<ColorInterractor> { colorInterractor });

            if (debug) Debug.Log (colorsByBit[bitTime].Log ());
            if (debug) Debug.Log ($"{("After").green()}\n" + colorsByBit.Log ());
        }

        public void OnPlayerBit (IBitData bitData)
        {
            if (debug)
            {
                Debug.Log (colorsByBit.Log ());
                Debug.Log (bitData.StartTime);
            }
            if (colorsByBit.ContainsKey (bitData.StartTime))
            {
                nearBeats.Clear ();

                colorsByBit.Remove (bitData.StartTime);

                if (colorsByBit.Count == 0) return;

                float keyOfNextTime = colorsByBit.Keys.Min ();

                List<ColorInterractor> listColorInterractors = colorsByBit[keyOfNextTime];

                foreach (var color in listColorInterractors)
                    nearBeats.Add (color);
            }
        }
        private void OnDisable ()
        {
            nearBeats.Clear ();
        }
        private void OnDestroy ()
        {
            CurrentSet = null;
            firstColorChoosed = false;
        }
    }
}
