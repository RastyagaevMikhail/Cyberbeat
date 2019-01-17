using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class MetaAction : IMetaData
    {
        [SerializeField] UnityEvent action;
        public void Invoke ()
        {
            action.Invoke ();
        }
		public float TimeDuaration { get;set; }
    }
}
