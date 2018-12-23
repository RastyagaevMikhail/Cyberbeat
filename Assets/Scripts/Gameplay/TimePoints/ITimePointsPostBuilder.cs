using UnityEngine;

namespace CyberBeat
{
    public interface ITimePointsPostBuilder
    {
        GameObject PostBuild (TimePointsBuilder builder, GameObject ResultGO);
    }
}
