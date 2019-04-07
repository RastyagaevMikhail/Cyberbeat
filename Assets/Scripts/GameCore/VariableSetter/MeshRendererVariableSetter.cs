
using GameCore;
using UnityEngine;
namespace  GameCore
{
    public class MeshRendererVariableSetter : MonoBehaviour
    {
        [SerializeField] MeshRendererVariable variable;
        void OnEnable()
        {
            variable.Value = GetComponent<MeshRenderer>();
        }
        void OnDisable()
        {
            variable.Value = null;
        }
        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<MeshRendererVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/MeshRenderer/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}MeshRenderer.asset");
        }
    }
}

