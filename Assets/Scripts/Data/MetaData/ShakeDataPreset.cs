namespace CyberBeat
{
    using GameCore;
   
    using UnityEngine;

    [CreateAssetMenu (fileName = "ShakeDataPreset", menuName = "CyberBeat/MetaData/ShakeData", order = 0)]
    public class ShakeDataPreset : ScriptableObject, IMetaDataPreset<ShakeData>, IMetaDataPreset
    {
        [SerializeField] ShakeData data;
        public void CopyFrom (ShakeData data)
        {
            this.data = data;
            this.Save();
        }
        object IMetaDataPreset.Data => data;
        public ShakeData Data => data;

    }
}
