using GameCore.Editor;

using Sirenix.OdinInspector.Editor;
namespace CyberBeat
{
    [OdinDrawer]
    public class TimeOfEventsDataDrawer : ScriptableObjectDrawer<TimeOfEventsData>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/TimeEvents/"; } }
    }

    [OdinDrawer]
    public class TimePointsDataDrawer : ScriptableObjectDrawer<TimePointsData>
    {
        protected override string LocalFolderPath { get { return "Assets/Data/TimePoints/"; } }
    }
}
