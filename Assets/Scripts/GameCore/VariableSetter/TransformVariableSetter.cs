using GameCore;

using UnityEngine;
namespace GameCore
{
    public class TransformVariableSetter : MonoBehaviour
    {
        [SerializeField] TransformVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<Transform> ();
        }
        void OnDisable ()
        {
            variable = null;
        }
#if UNITY_EDITOR

        [ContextMenu ("CreateVariableInstance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<TransformVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/Transform/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}Transform.asset");
        }
#endif
    }
}
