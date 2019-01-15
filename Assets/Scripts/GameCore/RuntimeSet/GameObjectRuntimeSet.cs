using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
[CreateAssetMenu(
    fileName = "GameObjectRuntimeSet.asset",
    menuName = "GameCore/RuntimeSet/GameObject")]
    public class GameObjectRuntimeSet : RuntimeSet<GameObject> { }
}
