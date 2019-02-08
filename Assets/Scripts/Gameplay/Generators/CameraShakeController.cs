using EZCameraShake;

using Sirenix.OdinInspector;

using UnityEngine;
namespace CyberBeat
{
    public class CameraShakeController : MonoBehaviour
    {
        [SerializeField] CameraShaker shaker;
        [SerializeField] ShakeDataPresetSelector Selector;
        public void OnShakeBit (IBitData bitData)
        {
            var data = Selector[bitData.StringValue].Data;
            data.TimeDuaration = bitData.Duration;
            data.ShakeOnce (shaker);
        }

#if UNITY_EDITOR
        [SerializeField] bool test;
        [SerializeField] ShakeData data;
        
        [Button]
        public void Shake ()
        {
            data.ShakeOnce (shaker);
        }

        [ContextMenuItem ("Copy to", "CopyToPreset")]
        [SerializeField] ShakeDataPreset PresetFromSave;
        [ContextMenu ("Copy To Preset")]
        void CopyToPreset ()
        {
            PresetFromSave.CopyFrom (data);
        }
#endif
    }
}
