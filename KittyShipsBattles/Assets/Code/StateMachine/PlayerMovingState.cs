using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovingState : PlayerBaseState
{
    private bool isMoving = false;
    public const float movementThreshold = 0.1f;
    private Vector3 previousPosition;

    internal override void EnterState(PlayerStateManager player)
    {

        player.animator.Play("Moving_P2");
        isMoving = true;
        previousPosition = player.gameObject.transform.position;
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        // Move player to the clicked position
        player.selectedPlayer.MovePlayer(player.mouseInputManager.clickedWorldPosition);


        Vector3 positionChange = player.gameObject.transform.position - previousPosition;
        player.trajectoryLine.EndLine();
        if (player.gameObject.transform.position.Equals(previousPosition))
        {
            player.SwitchState(player.selectedState);
            isMoving = false;
        }
        previousPosition = player.gameObject.transform.position;
    }
}
