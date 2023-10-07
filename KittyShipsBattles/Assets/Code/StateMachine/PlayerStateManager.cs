using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateManager : PlayerComponents
{
    internal Camera mainCamera;
    [SerializeField] internal GameObject healthBar;

    internal TrajectoryLine trajectoryLine;
    internal ProjectileController projectileController;

    internal PlayerBaseState currentState;

    internal MoveOrShootState moveOrShootState = new MoveOrShootState();
    internal PlayerSelectedState selectedState = new PlayerSelectedState();
    internal PlayerIdleState idleState = new PlayerIdleState();

    internal PlayerMovingState movingState = new PlayerMovingState();
    internal PlayerShootingState shootingState = new PlayerShootingState();

    //internal PlayerMovement playerMovement;
    internal List<PlayerMovement> playerMovements;
    internal List<PlayerStateManager> playerStateManagers;
    internal PlayerMovement selectedPlayer;

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        trajectoryLine = GetComponent<TrajectoryLine>();
        projectileController = GetComponent<ProjectileController>();

        healthBar.SetActive(false);

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        playerMovements = new List<PlayerMovement>();
        playerStateManagers = new List<PlayerStateManager>();

        foreach (var playerObject in playerObjects)
        {
            playerMovements.Add(playerObject.GetComponent<PlayerMovement>());
            playerStateManagers.Add(playerObject.GetComponent<PlayerStateManager>());
            //Debug.Log("playerMovements = " + playerMovements.Count);
        }        

        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {      
        currentState.UpdateState(this);
        //Debug.Log($"{this.gameObject.name} is at current state = " + currentState);
    }

    internal void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
