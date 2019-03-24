using GameCore;

using System;

using UnityEngine;
namespace GameCore
{
    public class AnimatorHashPlayerVariableSetter : MonoBehaviour
    {
        [SerializeField] AnimatorHashPlayerVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<AnimatorHashPlayer> ();
        }
        void OnDisable ()
        {
            variable.Value = null;
        }

        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<AnimatorHashPlayerVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/AnimatorHashPlayer/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}AnimatorHashPlayer.asset");
        }

        internal void Init (AnimatorHashPlayerVariable animatorVar)
        {
            variable = animatorVar;
        }
    }
}
