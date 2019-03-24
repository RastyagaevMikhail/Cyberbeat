using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "StringVariable", menuName = "GameCore/Variable/System/String", order = 0)]
    public class StringVariable : ScriptableObject
    {
        [SerializeField]
        private string value = "";

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}