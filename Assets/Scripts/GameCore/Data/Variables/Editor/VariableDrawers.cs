using Sirenix.OdinInspector.Editor;

using System;

using UnityEngine;

namespace GameCore.Editor
{

    [OdinDrawer]
    public class ColorVariableDrawer : ScriptableObjectDrawer<ColorVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Colors/"; } }

    }

    [OdinDrawer]
    public class BoolVariableDrawer : ScriptableObjectDrawer<BoolVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Bools/"; } }
    }

    [OdinDrawer]
    public class IntVariableDrawer : ScriptableObjectDrawer<IntVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Ints/"; } }
    }

    [OdinDrawer]
    public class FloatVariableDrawer : ScriptableObjectDrawer<FloatVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Floats/"; } }
    }

    [OdinDrawer]
    public class Vector3VariableDrawer : ScriptableObjectDrawer<Vector3Variable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/Vectors/"; } }
    }

    [OdinDrawer]
    public class TimeSpanVariableDrawer : ScriptableObjectDrawer<TimeSpanVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/TimeSpans/"; } }
    }

    [OdinDrawer]
    public class DateTimeVariableDrawer : ScriptableObjectDrawer<DateTimeVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/DateTimes/"; } }
    }

    [OdinDrawer]
    public class IntListVariableDrawer : ScriptableObjectDrawer<IntListVariable>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/IntLists/"; } }
    }

    [OdinDrawer]
    public class EventListenerContainerDrawer : ScriptableObjectDrawer<EventListenerContainer>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/Variables/EventListenerContainers/"; } }
    }
}
