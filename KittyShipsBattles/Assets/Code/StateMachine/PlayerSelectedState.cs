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
        Debug.Log("Hello - ");

        foreach (PlayerStateManager p in player.playerStateManagers)
        {
            //Debug.Log("selectedPlayer = " + p.name);
            if (p.name != player.selectedPlayer.name)
            {
                p.SwitchState(p.idleState);
                p.healthBar.SetActive(false);
                ////p.selectedState.selected = false;
            }
            else if (p.name == player.selectedPlayer.name)
            {
                player.selectedPlayer = player.GetComponent<PlayerMovement>();
                player.SwitchState(player.selectedState);
            }
        }
        //Debug.Log("name played once this  = " + player.gameObject.name);
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //Debug.Log("name playing this update = " + player.gameObject.name);
        // Get mouse position in world coordinates
        if (player.selectedPlayer.name == player.gameObject.name && player.idleState.selected == true)
        {
            MouseInputManager mouseInputManager = player.GetComponent<MouseInputManager>();
            //Debug.Log("drag distance - " + dragDistance);
            if (mouseInputManager.isDragging(player) == 1)
            {
                player.SwitchState(player.shootingState);
            }
            else if(mouseInputManager.isDragging(player) == 0)
            {
                player.SwitchState(player.movingState);
            }              
        }
    }
}
