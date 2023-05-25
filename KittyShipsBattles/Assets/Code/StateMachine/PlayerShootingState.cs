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
        // Shoot the projectile
        //player.projectileController.ShootProjectile(player.initialMousePosition, clickedWorldPosition, initialSpeed);
    }
}
