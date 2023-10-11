using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovingState : PlayerBaseState
{
    private bool isMoving = false;
    public const float movementThreshold = 0.2f;
    private Vector3 previousPosition;

    internal override void EnterState(PlayerStateManager player)
    {

        player.animator.Play("Moving_P2");
        isMoving = true;
        previousPosition = player.gameObject.transform.position;
        Debug.Log("previousPosition = " + previousPosition);
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        // Move player to the clicked position
        player.selectedPlayer.MovePlayer(player.mouseInputManager.clickedWorldPosition);


        Vector3 positionChange = player.gameObject.transform.position - previousPosition;
        Debug.Log("Entering positionChange - " + positionChange);
        player.trajectoryLine.EndLine();
        if (positionChange.magnitude < movementThreshold)
        {
            Debug.Log("Entering 2 ?");
            player.SwitchState(player.selectedState);
            isMoving = false;
        }
    }
}
