using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/ParticleSystem")]
    public class ParticleSystemSelector : AEnumDataSelectorScriptableObject<string, ParticleSystem>
    {
        public List<ParticleSystemTypeData> datas;
        public override List<TypeData<string, ParticleSystem>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, ParticleSystem>> ().ToList ();
            }
        }
        [System.Serializable] public class ParticleSystemTypeData : TypeData<string, ParticleSystem> { }
        [SerializeField] string ValiadtePath = "Assets/Data/";
    #if UNITY_EDITOR
        [ContextMenu ("Validate")]
        void Validate ()
        {
            datas = Tools.GetAtPath<ParticleSystem> (ValiadtePath)
                .Select (p => new ParticleSystemTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }
    #endif
    }
}