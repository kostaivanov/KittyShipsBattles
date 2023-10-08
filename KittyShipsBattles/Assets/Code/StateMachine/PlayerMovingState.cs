using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovingState : PlayerBaseState
{
    internal override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering?");
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("Entering update?");

        // Move player to the clicked position
        player.selectedPlayer.MovePlayer(player.mouseInputManager.clickedWorldPosition);
        player.animator.Play("Moving_P2");
        player.SwitchState(player.selectedState);
    }
}
