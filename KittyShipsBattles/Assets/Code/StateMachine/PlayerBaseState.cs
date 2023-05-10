using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class PlayerBaseState
{
    internal abstract void EnterState(PlayerStateManager player);
    internal abstract void UpdateState(PlayerStateManager player);
}
