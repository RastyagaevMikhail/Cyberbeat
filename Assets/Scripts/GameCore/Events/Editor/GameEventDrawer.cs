namespace GameCore.Editor
{
    using GameCore;

    using Sirenix.OdinInspector.Editor.Drawers;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;

    using UnityEditor;

    using UnityEngine;
    [OdinDrawer]
    [CanEditMultipleObjects]
    public class GameEventDrawer : ScriptableObjectDrawer<GameEvent>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Events/"; } }
    }

    [OdinDrawer][CanEditMultipleObjects]
    public class GameEventObjectDrawer : ScriptableObjectDrawer<GameEventObject>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Events/"; } }
    }

}
