using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Difficulty", menuName = "CyberBeat/Track/Dufficulty")]
    public class TrackDifficulty : ScriptableObject
    {
        [SerializeField] string localizationID;
        public string LocalizationID => localizationID;
    }

}
