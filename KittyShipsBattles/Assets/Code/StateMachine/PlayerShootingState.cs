using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerShootingState : PlayerBaseState
{
    internal override void EnterState(PlayerStateManager player)
    {
        throw new System.NotImplementedException();
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        Debug.Log("shooting");
        //float dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);

        //float initialSpeed = player.projectileController.CalculateShootingPower(player.selectedPlayer.dragDistance);
        //player.projectileController.ShootProjectile(initialMousePosition, clickedWorldPosition, initialSpeed);

    }
}
