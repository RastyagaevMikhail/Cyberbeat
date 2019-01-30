﻿using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/BitDataCollection/Track")]
    public class TrackBitsCollection : ABitDataCollection
    {
        [SerializeField] List<TrackBit> bits;
        public override List<IBitData> Bits => bits.Cast<IBitData> ().ToList ();
        public override void Init (List<KoreographyEvent> events)
        {
            bits = events.Select (e => new TrackBit (e)).ToList ();
            this.Save();
        }
    }

    [System.Serializable]
    public class TrackBit : IBitData
    {
        [SerializeField] TrackBitPayload payloadData;
        [SerializeField] float startTime;
        public float StartTime { get => startTime; }

        [SerializeField] float endTime;
        public float EndTime => endTime;
        [SerializeField] float duration;
        public float Duration => duration;
        public IPayloadData PayloadData { get => payloadData; }
        public string RandomString { get => PayloadData.RandomString; }
        public string StringValue { get => PayloadData.StringValue; }
        public string[] Strings => payloadData.Strings;

        public TrackBit (KoreographyEvent koreographyEvent)
        {
            Init (koreographyEvent);
        }
        public void Init (KoreographyEvent koreographyEvent)
        {
            startTime = (float) koreographyEvent.StartSample / 44100f;
            endTime = (float) koreographyEvent.EndSample / 44100f;
            duration = endTime - startTime;
            payloadData = new TrackBitPayload ();
            payloadData.Init (koreographyEvent.Payload);
        }
    }
    [System.Serializable] class TrackBitPayload : APayloadData { }
}
