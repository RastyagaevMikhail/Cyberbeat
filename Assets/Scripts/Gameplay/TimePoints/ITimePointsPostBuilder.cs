using UnityEngine;

namespace CyberBeat
{
    public abstract class ATimePointsPostBuilder : ScriptableObject
    {
        public abstract GameObject PostBuild (TimePointsBuilder builder, GameObject ResultGO);
    }
}
