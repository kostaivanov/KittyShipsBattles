using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerIdleState : PlayerBaseState
{
    internal bool selected;

    internal override void EnterState(PlayerStateManager player)
    {
        if (player.playerMovements != null && player.playerMovements.Count > 0)
        {
            selected = true;
            if (player.gameObject.name == player.playerMovements[0].gameObject.name && player.countSelection == 0)
            {
                player.countSelection += 1;
                player.selectedPlayer = player.playerMovements[0];
                player.healthBar.SetActive(true);
                player.SwitchState(player.selectedState);
            }
        }
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //throw new System.NotImplementedException();
    }
}
