using System.Collections.Generic;
using System.Linq;
using GameCore;
using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Status", menuName = "CyberBeat/Track/Status")]
    public class TrackStatus : ScriptableObject
    {

        [SerializeField] List<SpriteLanguage> langSettings;
        private Dictionary<SystemLanguage, Sprite> _spriteHash = null;
        private Dictionary<SystemLanguage, Sprite> spriteHash => _spriteHash ?? InitHash ();

        private void OnEnable ()
        {
            InitHash ();
        }
        private Dictionary<SystemLanguage, Sprite> InitHash ()
        {
            return (_spriteHash = langSettings.ToDictionary (ls => ls.Lang, ls => ls.Sprite));
        }

        SystemLanguage language => LocalizationManager.instance.currentLanguage;
        public Sprite Sprite
        {
            get
            {
                Sprite result = null;
                spriteHash.TryGetValue (language, out result);
                return result;
            }
        }
    }
}
