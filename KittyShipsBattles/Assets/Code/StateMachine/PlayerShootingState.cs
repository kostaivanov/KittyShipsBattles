using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerShootingState : PlayerBaseState
{
    internal override void EnterState(PlayerStateManager player)
    {

    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //float dragDistance = Vector2.Distance(player.mouseInputManager.initialMousePosition, player.mouseInputManager.clickedWorldPosition);
        Debug.Log("Shooting");
        player.animator.Play("Shooting_P2");
        float initialSpeed = player.projectileController.CalculateShootingPower(player.mouseInputManager.dragDistance);
        player.projectileController.ShootProjectile(player.mouseInputManager.initialMousePosition, player.mouseInputManager.clickedWorldPosition, initialSpeed);

        player.trajectoryLine.EndLine();
        player.SwitchState(player.selectedState);
    }
}
