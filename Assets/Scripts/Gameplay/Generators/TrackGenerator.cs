using DG.Tweening;

using GameCore;

using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    public class TrackGenerator : TransformObject
    {
        private const float width = 5f;
        public Pool pool { get { return Pool.instance; } }
        public TracksCollection tracksCollection { get { return TracksCollection.instance; } }

        RandomConstantMaterial rcm;

        List<ColorInterractor> Neighbors = new List<ColorInterractor> ();
        private void Start ()
        {
            LastRandomColor = Colors.instance.RandomColor;

            InitRCM ();
            lastBeat = 0;
        }

        [SerializeField] StringVariable DefaultColorName;
        private void InitRCM ()
        {
            rcm = new RandomConstantMaterial (); //ScriptableObject.CreateInstance<RandomConstantMaterial> ();
            rcm.Init (LastRandomColor, DefaultColorName.Value);
        }
        float lastBeat = 0;
        [SerializeField] Color LastRandomColor;
        public void OnBit (KoreographyEvent koreographyEvent)
        {
            if (!koreographyEvent.HasTextPayload ()) return;

            float bitTime = (float) koreographyEvent.StartSample / 44100f;

            if (lastBeat >= bitTime)
                return;
            else
                lastBeat = bitTime;

            var TextPayloadValue = koreographyEvent.GetTextValue ();
            string[] SplitedPresetsStrings = TextPayloadValue.Split (',');

            int randPreset = SplitedPresetsStrings.Select (strInt => int.Parse (strInt)).GetRandom ();
            List<SpawnedObject> row = null;
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

                if (spawnObj)
                {
                    ColorInterractor colorInterractor = spawnObj.Get<ColorInterractor> ();

                    colorInterractor.Init (bitTime);

                    Neighbors.Add (colorInterractor);

#if UNITY_EDITOR
                    string metadata = string.Format ("{0}", bitTime);
                    spawnObj.Get<MetaDataGizmos> ().MetaData = Tools.LogTextInColor (metadata, Color.blue);
#endif
                }
            }
            if (Neighbors.Count > 1)
            {
                foreach (var neighbor in Neighbors)
                    foreach (var n in Neighbors)
                        neighbor.AddNeighbor (n);
            }
            Neighbors.Clear ();
        }

        SpawnedObject InstattiateObj (SpawnedObject spwn_obj, float xPos)
        {
            if (!spwn_obj) return null;
            string Key = spwn_obj.name;
            var obj = pool.Pop (Key);
            if (!obj) return null;

            obj.position = position + right * xPos + up * obj.y;
            obj.rotation = rotation;

            MaterialSwitcher originalMaterialSwitcher = spwn_obj.Get<MaterialSwitcher> ();
            if (originalMaterialSwitcher)
            {
                Material currentMaterial = originalMaterialSwitcher.CurrentMaterial;

                if (obj.Get<ColorSwitcher> ())
                {
                    InitRCM ();
                }
                else
                {
                    obj.localScale = Vector3.one;
                }

                var matSwitcher = obj.Get<MaterialSwitcher> ();
                if (matSwitcher)
                {
                    Material ConstantMaterial = rcm.Constant[currentMaterial];
                    Material RandomMaterial = rcm.GetRandom (currentMaterial);
                    matSwitcher.SetMaterial (matSwitcher.Constant ? ConstantMaterial : RandomMaterial);
                    LastRandomColor = RandomMaterial.GetColor (matSwitcher.DefaultColorName);
                }
            }
            return obj;
        }
    }
}
