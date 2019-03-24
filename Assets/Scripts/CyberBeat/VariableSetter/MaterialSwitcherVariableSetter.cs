
using System;
using GameCore;
using UnityEngine;
namespace  CyberBeat
{
    public class MaterialSwitcherVariableSetter : MonoBehaviour
    {
        [SerializeField] MaterialSwitcherVariable variable;
        void OnEnable()
        {
            variable.Value = GetComponent<MaterialSwitcher>();
        }
        void OnDisable()
        {
            variable.Value = null;
        }
        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<MaterialSwitcherVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/CyberBeat/Data/VariableSetter/MaterialSwitcher/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}MaterialSwitcher.asset");
        }

		public void Init(MaterialSwitcherVariable matSwitchVar)
		{
			variable = matSwitchVar;
		}
	}
}

