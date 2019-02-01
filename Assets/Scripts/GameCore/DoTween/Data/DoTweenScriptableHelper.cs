using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore.DoTween
{
    using DG.Tweening;
    public class DoTweenScriptableHelper : SingletonData<DoTweenScriptableHelper>
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/DoTween")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { }
        public override void ResetDefault () { }

#endif
        [Header ("Init Settings")]
        [SerializeField] bool RecycleAllByDefault = false;
        [SerializeField] bool UseSafeMode = true;
        [SerializeField] LogBehaviour defaultLogBehaviour = LogBehaviour.Default;
        [Header ("Capacity Settings")]
        [SerializeField] int TweenersCapacity = 200;
        [SerializeField] int SequencesCapacity = 10;
        public void Init ()
        {
            DG.Tweening.DOTween.Init (RecycleAllByDefault, UseSafeMode, defaultLogBehaviour).SetCapacity (TweenersCapacity, SequencesCapacity);
        }
    }
}
