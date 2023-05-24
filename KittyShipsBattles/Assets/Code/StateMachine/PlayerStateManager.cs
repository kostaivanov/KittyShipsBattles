using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerStateManager : PlayerComponents
{
    internal Camera mainCamera;
    [SerializeField] internal GameObject healthBar;


    #region Shooting/Moving variables
    internal Vector3 initialMousePosition;

    internal float mouseDownTime;
    [SerializeField] internal float maxClickDuration; // Maximum time the mouse button can be held down for a click to be registered
    [SerializeField] internal float minDragDistance; // Maximum time the mouse button can be held down for a click to be registered

    internal bool isDragging = false;
    #endregion

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
        projectileController = GetComponent<ProjectileController>();

        healthBar.SetActive(false);

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        playerMovements = new List<PlayerMovement>();

        foreach (var playerObject in playerObjects)
        {
            playerMovements.Add(playerObject.GetComponent<PlayerMovement>());
            Debug.Log("playerMovements = " + playerMovements.Count);
        }        

        currentState = selectedState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (selectedPlayer.name == gameObject.name)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = mousePosition;
                isDragging = true;
                mouseDownTime = Time.time;
            }

            // Detect mouse button release and shoot projectile
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                float clickDuration = Time.time - mouseDownTime;
                //Vector3 mouseUpPosition = Input.mousePosition;
                Vector3 clickedWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickedWorldPosition.z = 0;

                float dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);
                float initialSpeed = projectileController.CalculateShootingPower(dragDistance);

                //Debug.Log("mouseDownTime: " + mouseDownTime);
                //Debug.Log("Time.time: " + Time.time);
                //Debug.Log("clickDuration: " + clickDuration);
                //Debug.Log("drag distance: " + dragDistance);
                //Debug.Log("minDragDistance " + minDragDistance);

                // Get mouse position in world coordinates
                Vector3 viewportPosition = mainCamera.WorldToViewportPoint(clickedWorldPosition);

                //Debug.Log("Distance between start point and last point of the mouse: " + Vector2.Distance(initialMousePosition, mousePosition));
                //Debug.Log("max Distance: " + maxDistance);
                //Debug.Log("shootingPower: " + shootingPower);
                //Debug.Log("Mathf.Lerp of initialSpeed: " + initialSpeed);

                if (clickDuration < maxClickDuration && dragDistance < minDragDistance &&
                viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    Debug.Log("Entering?");
                    // Move player to the clicked position
                    selectedPlayer.MovePlayer(clickedWorldPosition);
                }
                else
                {
                    // Shoot the projectile
                    projectileController.ShootProjectile(initialMousePosition, clickedWorldPosition, initialSpeed);
                }

                isDragging = false;
                trajectoryLine.EndLine();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = mousePosition;
                trajectoryLine.RenderLine(initialMousePosition, mousePosition);
            }
        }

        currentState.UpdateState(this);
        Debug.Log("We play? = " + currentState);
    }

    internal void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
