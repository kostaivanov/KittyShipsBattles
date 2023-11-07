using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerSelectedState : PlayerBaseState
{
    internal Vector3 initialMousePosition;
    internal float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag

    internal override void EnterState(PlayerStateManager player)
    {
        player.animator.Play("IDLE_P2");
        //Debug.Log("Hello - ");
        player.mouseInputManager.dragDistance = -1;

    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //Debug.Log("name playing this update = " + player.gameObject.name);
        // Get mouse position in world coordinates
        // && player.idleState.selected == true
        if (player.selectedPlayer.name == player.gameObject.name)
        {
            player.mouseInputManager.allowedToShoot = true;

            //int dragResult = player.mouseInputManager.isDragging(player);
            //MouseInputManager mouseInputManager = player.GetComponent<MouseInputManager>();
            player.mouseInputManager.dragDistance = -1;
            //Debug.Log("drag distance - " + dragDistance);
            player.mouseInputManager.isDragging(player);


            if (player.mouseInputManager.shouldShoot)
            {
                Debug.Log("choose shooting");
                player.SwitchState(player.shootingState);
            }
            else if (!player.mouseInputManager.isMouseDown)
            {
                Debug.Log("choose Moving");
                player.SwitchState(player.movingState);
            }
        }
    }
}
