using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerSelectedState : PlayerBaseState
{
    private Camera mainCamera;
    private PlayerMovement selectedPlayer;

    internal override void EnterState(PlayerStateManager player)
    {
        mainCamera = Camera.main; 
        //if (playerMovements.Count > 0)
        //{
        //    selectedPlayer = playerMovements[0];
        //}
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //Handle player seleciton and highlighting
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            PlayerMovement hitPlayer = hit.collider.GetComponent<PlayerMovement>();
            if (hitPlayer != null)
            {
                hitPlayer.Sethighlight(true);

                if (Input.GetMouseButtonDown(0))
                {
                    selectedPlayer = hitPlayer;
                    Debug.Log("selectedPlayer = " + selectedPlayer.name);
                }
            }
            else
            {
                //foreach (var player in playerMovements)
                //{
                //    player.Sethighlight(false);
                //}
            }
        }
        else
        {
            //foreach (var player in playerMovements)
            //{
            //    player.Sethighlight(false);
            //}
        }
    }
}
