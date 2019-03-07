using GameCore;

using UnityEngine;
namespace GameCore
{
    public class RigidbodyVariableSetter : MonoBehaviour
    {
        [SerializeField] RigidbodyVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<Rigidbody> ();
        }
        void OnDisable ()
        {
            variable = null;
        }

        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<RigidbodyVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/Rigidbody/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}Rigidbody.asset");
        }

    }
}
