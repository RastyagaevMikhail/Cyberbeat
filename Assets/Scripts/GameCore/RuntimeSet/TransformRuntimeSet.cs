using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (
        fileName = "TransformRuntimeSet.asset",
        menuName = "GameCore/RuntimeSet/Transform")]
    public class TransformRuntimeSet : RuntimeSet<Transform> { }
}
