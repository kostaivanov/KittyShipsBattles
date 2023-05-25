using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerSelectedState : PlayerBaseState
{

 

    internal override void EnterState(PlayerStateManager player)
    {
        if (player.playerMovements.Count > 0)
        {
            if (player.gameObject.name == player.playerMovements[0].gameObject.name)
            {
                player.selectedPlayer = player.playerMovements[0];
                player.healthBar.SetActive(true);
                player.SwitchState(player.moveOrShootState);
            }

        }
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //Handle player seleciton and highlighting
        RaycastHit2D hit = Physics2D.Raycast(player.mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            PlayerMovement hitPlayer = hit.collider.GetComponent<PlayerMovement>();
            if (hitPlayer != null)
            {
                hitPlayer.Sethighlight(true);

                if (Input.GetMouseButtonDown(0))
                {
                    player.selectedPlayer = hitPlayer;
                    player.SwitchState(player.shootingState);
                    Debug.Log("selectedPlayer = " + player.selectedPlayer.name);
                }
            }
            else
            {
                foreach (var _player in player.playerMovements)
                {
                    _player.Sethighlight(false);
                }
            }
        }
        else
        {
            foreach (var _player in player.playerMovements)
            {
                _player.Sethighlight(false);
            }
        }
    }
}
