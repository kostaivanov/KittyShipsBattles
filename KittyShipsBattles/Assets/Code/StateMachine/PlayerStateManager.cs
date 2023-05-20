using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateManager : PlayerComponents
{
    internal Camera mainCamera;

    internal Vector3 initialMousePosition;
    internal bool isDragging = false;
    internal float mouseDownTime;
    [SerializeField] internal float maxClickDuration; // Maximum time the mouse button can be held down for a click to be registered
    [SerializeField] internal float minDragDistance; // Maximum time the mouse button can be held down for a click to be registered
    internal TrajectoryLine trajectoryLine;

    internal ProjectileController projectileController;

    internal PlayerBaseState currentState;

    internal PlayerSelectedState selectedState = new PlayerSelectedState();
    internal PlayerMovingState movingState = new PlayerMovingState();
    internal PlayerShootingState shootingState = new PlayerShootingState();
    internal PlayerDraggingState draggingState = new PlayerDraggingState();

    //internal PlayerMovement playerMovement;
    internal List<PlayerMovement> playerMovements;
    internal PlayerMovement selectedPlayer;

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        trajectoryLine = GetComponent<TrajectoryLine>();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        playerMovements = new List<PlayerMovement>();
        foreach (var playerObject in playerObjects)
        {
            playerMovements.Add(playerObject.GetComponent<PlayerMovement>());
        }

        playerMovements = new List<PlayerMovement>();
        

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
