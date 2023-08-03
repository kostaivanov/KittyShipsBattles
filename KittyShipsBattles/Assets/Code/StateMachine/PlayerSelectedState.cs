using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerSelectedState : PlayerBaseState
{
    internal bool selected;
    internal override void EnterState(PlayerStateManager player)
    {
        if (player.playerMovements.Count > 0)
        {
            selected = true;
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
                    Debug.Log("player = " + player.selectedPlayer.name);
                    //Debug.Log("player.playerStateManagers count = " + player.playerStateManagers.Count);


                    foreach (PlayerStateManager p in player.playerStateManagers)
                    {
                        //Debug.Log("selectedPlayer = " + p.name);
                        if (p.name != player.selectedPlayer.name)
                        {
                            //Debug.Log("Hello - " + p.name);
                            p.SwitchState(p.selectedState);
                            p.healthBar.SetActive(false);
                            p.selectedState.selected = false;
                        }
                        else if(p.name == player.selectedPlayer.name)
                        {
                            p.SwitchState(p.moveOrShootState);
                        }
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
        else
        {
            foreach (var _player in player.playerMovements)
            {
                _player.Sethighlight(false);
            }
        }
    }
}
