using Sirenix.OdinInspector.Editor;

using System;

using UnityEngine;

namespace GameCore.Editor
{

    [OdinDrawer]
    public class ColorVariableDrawer : ScriptableObjectDrawer<ColorVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Color/"; } }

    }

    [OdinDrawer]
    public class BoolVariableDrawer : ScriptableObjectDrawer<BoolVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Bool/"; } }
    }

    [OdinDrawer]
    public class IntVariableDrawer : ScriptableObjectDrawer<IntVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Int/"; } }
    }

    [OdinDrawer]
    public class FloatVariableDrawer : ScriptableObjectDrawer<FloatVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Float/"; } }
    }

    [OdinDrawer]
    public class Vector3VariableDrawer : ScriptableObjectDrawer<Vector3Variable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Vector3/"; } }
    }

    [OdinDrawer]
    public class TimeSpanVariableDrawer : ScriptableObjectDrawer<TimeSpanVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/TimeSpan/"; } }
    }

    [OdinDrawer]
    public class DateTimeVariableDrawer : ScriptableObjectDrawer<DateTimeVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/DateTime/"; } }
    }

    [OdinDrawer]
    public class IntListVariableDrawer : ScriptableObjectDrawer<IntListVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/IntList/"; } }
    }

    [OdinDrawer]
    public class EventListenerContainerDrawer : ScriptableObjectDrawer<EventListenerContainer>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/EventListenerContainer/"; } }
    }
    [OdinDrawer]
    public class UnityObjectVariableDrawer : ScriptableObjectDrawer<UnityObjectVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Object/"; } }
    }
    
}
