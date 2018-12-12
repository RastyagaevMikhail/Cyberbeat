using GameCore;

using Sirenix.Serialization;

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
        private static TrackGenerator _instance = null;
        public static TrackGenerator instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<TrackGenerator> (); return _instance; } }
        public Pool pool { get { return Pool.instance; } }
        public TracksCollection tracksCollection { get { return TracksCollection.instance; } }
        private AudioSource _ASource = null;
        public AudioSource ASource { get { if (_ASource == null) _ASource = GetComponent<AudioSource> (); return _ASource; } }

        Track track { get { return tracksCollection.CurrentTrack; } }
        public void PlayMusinOnSwitcher ()
        {
            if (!ASource.isPlaying)
                ASource.Play ();
        }

        [OdinSerialize]
        RandomConstantMaterial rcm;
        private List<BitInfo> CurrentBits;
        BitInfo currentBit;
        GameData gameData { get { return GameData.instance; } }
        private SimpleMusicPlayer _musicPlayer = null;
        public SimpleMusicPlayer musicPlayer { get { if (_musicPlayer == null) _musicPlayer = GetComponent<SimpleMusicPlayer> (); return _musicPlayer; } }
        private Koreographer _koreographer = null;
        public Koreographer koreographer { get { if (_koreographer == null) _koreographer = GetComponent<Koreographer> (); return _koreographer; } }
        public bool outOfIndexBits { get { return (CurrentBits.Count <= indexRow); } }

        private void Start ()
        {
            LastRandomColor = Colors.instance.RandomColor;

            InitRCM ();

            ASource.clip = track.music.clip;
            musicPlayer.LoadSong (track.koreography, 0, false);
            koreographer.LoadKoreography (track.koreography);
            ResetCurrentRows ();
        }

        private void InitRCM ()
        {
            rcm = new RandomConstantMaterial (); //ScriptableObject.CreateInstance<RandomConstantMaterial> ();
            rcm.Init (LastRandomColor);
        }

        private int indexRow = 0;
        private float time = 0;
        [SerializeField] Color LastRandomColor;
        private void Update ()
        {
            if (!outOfIndexBits && currentBit.time + track.MinTimeOfBit <= time)
            {
                float half_width = width / 2;
                float step = half_width / 2;
                // string Log = "";

                int randPreset = Tools.RandomIn (currentBit.presets);
                RowInfo row = tracksCollection.Prefabs[randPreset];

                for (float x = -half_width, i = 0; x <= half_width; x += step, i++)
                {
                    var spawnObj = InstattiateObj (row[(int) i], x);

                    if (spawnObj)
                    {
                        string metadata = string.Format ("{0}", currentBit.time);
                        spawnObj.Get<MetaDataGizmos> ().MetaData = Tools.LogTextInColor (metadata, Color.blue);
                    }
                }
                indexRow++;

                if (!outOfIndexBits)
                {
                    currentBit = CurrentBits[indexRow];
                }
            }
            time += Time.deltaTime;
        }

        private void ResetCurrentRows ()
        {
            CurrentBits = new List<BitInfo> (track.BitsInfos);
            // CurrentBits.Reverse ();
            currentBit = CurrentBits.First ();
            gameData.SetGeneratedBrick (track.CountConstant);
        }

        SpawnedObject InstattiateObj (SpawnedObject spwn_obj, float xPos)
        {
            if (!spwn_obj) return null;
            string Key = spwn_obj.name;
            var obj = pool.Pop (Key);
            if (!obj) return null;

            obj.position = transform.position + transform.right * xPos + transform.up * obj.yOffset;
            obj.transform.rotation = transform.rotation;
            obj.Get<MetaDataGizmos> ().MetaData = currentBit.time.ToString();
            MaterialSwitcher origianlMaterialSwitcher = spwn_obj.Get<MaterialSwitcher> ();
            if (origianlMaterialSwitcher)
            {
                Material currentMaterial = origianlMaterialSwitcher.CurrentMaterial;

                if (obj.Get<ColorSwitcher> ())
                {
                    InitRCM ();
                }

                var matSwitcher = obj.Get<MaterialSwitcher> ();
                if (matSwitcher)
                {
                    Material ConstantMaterial = rcm.Constant[currentMaterial];
                    Material RandomMaterial = rcm.GetRandom (currentMaterial);
                    matSwitcher.SetMaterial (matSwitcher.Constant ? ConstantMaterial : RandomMaterial);
                    LastRandomColor = RandomMaterial.GetColor ("_EmissionColor");
                }
            }
            return obj;
        }
    }
}
