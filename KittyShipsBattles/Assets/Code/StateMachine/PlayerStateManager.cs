using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateManager : PlayerComponents
{
    internal PlayerMovement playerMovement;
    internal ProjectileController projectileController;

    internal PlayerBaseState currentState;

    internal PlayerSelectedState selectedState = new PlayerSelectedState();
    internal PlayerMovingState movingState = new PlayerMovingState();
    internal PlayerShootingState shootingState = new PlayerShootingState();
    internal PlayerDraggingState draggingState = new PlayerDraggingState();

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        currentState = selectedState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    internal void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
