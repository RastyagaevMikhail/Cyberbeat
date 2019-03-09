
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    public class InputControllerComponentVariableSetter : MonoBehaviour
    {
        [SerializeField] InputControllerComponentVariable variable;
        void OnEnable()
        {
            variable.Value = GetComponent<InputControllerComponent>();
        }
        void OnDisable()
        {
            variable = null;
        }
        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<InputControllerComponentVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/CyberBeat/Data/VariableSetter/InputControllerComponent/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}InputControllerComponent.asset");
        }
    }
}

