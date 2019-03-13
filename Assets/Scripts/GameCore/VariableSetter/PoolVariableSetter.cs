using GameCore;

using UnityEngine;
namespace GameCore
{
    public class PoolVariableSetter : MonoBehaviour
    {
        [SerializeField] PoolVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<Pool> ();
        }
        void OnDisable ()
        {
            variable = null;
        }
#if UNITY_EDITOR

        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<PoolVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/Pool/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}Pool.asset");
        }
#endif
    }
}
