using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SkinComponent : MonoBehaviour
    {
        [SerializeField] new Renderer renderer;
        public Renderer Renderer => renderer;
        [SerializeField] AnimatorHashPlayer animator;
        [SerializeField] MaterialSwitcher matSwitcher;
        private void OnValidate ()
        {
            animator = GetComponent<AnimatorHashPlayer> ();
            matSwitcher = GetComponentInChildren<MaterialSwitcher> ();
        }

        [SerializeField] float animationClipTime = 2f;
        public void StartAniamtion ()
        {
            if (!gameObject.activeSelf || !gameObject.activeInHierarchy) return;
            StartCoroutine (cr_randomAnimation ());
        }

        public void StartInit (Color color)
        {
            matSwitcher.CurrentColor = color;
        }

        private IEnumerator cr_randomAnimation ()
        {
            WaitForSeconds wfs = new WaitForSeconds (animationClipTime);
            while (true)
            {
                animator.PlayRandom ();
                yield return wfs;
            }
        }
        public void StopAniamtion ()
        {
            StopAllCoroutines ();
        }
#if UNITY_EDITOR

        [ContextMenu ("Validate")]
        void Validate ()
        {
            Action<UnityEngine.Object> SD = UnityEditor.EditorUtility.SetDirty;

            Func<string, ScriptableObject> VSO = Tools.ValidateSO;

            SD (gameObject);
            var animatorVar = VSO ("Assets/Data/Variables/AnimatorHashPlayer/PlayerSkinAnimator.asset") as AnimatorHashPlayerVariable;
            var setter = gameObject.GetOrAddComponent<AnimatorHashPlayerVariableSetter> ();
            SD (setter);
            setter.Init (animatorVar);

            Tools.Destroy (GetComponent<OnEnableUnityEvent> ());

            var matSwitchVar = VSO ("Assets/Data/Variables/MaterialSwitcher/PlayerMaterialSwitcher.asset") as MaterialSwitcherVariable;
            var matSwSetter = matSwitcher.gameObject.GetOrAddComponent<MaterialSwitcherVariableSetter> ();
            SD (matSwSetter);
            matSwSetter.Init (matSwitchVar);
            Tools.Destroy (matSwitcher.gameObject.GetComponent<OnEnableUnityEvent> ());
            SD (matSwitcher.gameObject);
        }
#endif
    }
}
