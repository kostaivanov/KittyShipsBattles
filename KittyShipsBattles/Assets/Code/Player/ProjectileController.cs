using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private GameObject muzzle;
    [SerializeField] private float minInitialSpeed;
    [SerializeField] private float maxInitialSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private Vector2 gravity = new Vector2(0, -9.81f);
    private Vector3 initialMousePosition;
    private Vector2 velocity;
    private bool isDragging = false;

    private TrajectoryLine trajectoryLine;

    private List<PlayerMovement> playerMovements;
    private PlayerMovement selectedPlayer;
    private Camera mainCamera;
    private float mouseDownTime;
    [SerializeField] private float maxClickDuration; // Maximum time the mouse button can be held down for a click to be registered
    [SerializeField] private float minDragDistance; // Maximum time the mouse button can be held down for a click to be registered

    private void Start()
    {
        mainCamera = Camera.main;
        trajectoryLine = GetComponent<TrajectoryLine>();
    }


    void Update()
    {
       

        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Detect mouse button press and start dragging
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
            float initialSpeed = CalculateShootingPower(dragDistance);

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
                ShootProjectile(clickedWorldPosition, initialSpeed);
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

    internal float CalculateShootingPower(float dragDistance)
    {
        // Calculate shooting power based on the mouse movement
        float shootingPower = Mathf.Clamp01(dragDistance / maxDistance);
        float initialSpeed = Mathf.Lerp(minInitialSpeed, maxInitialSpeed, shootingPower);
        return initialSpeed;
    }

    internal void ShootProjectile(Vector3 mouseUpPosition, float initialSpeed)
    {
        // Calculate velocity based on the mouse movement direction
        velocity = (initialMousePosition - mouseUpPosition).normalized * initialSpeed;

        // Instantiate projectile and set its initial position and velocity
        GameObject projectile = Instantiate(projectilePrefab, muzzle.transform.position, Quaternion.identity);

        //Set the shooting palyer reference on the projectile
        //projectile.GetComponent<Projectile>().SetShootingPlayer(this.gameObject);
        projectile.GetComponent<Projectile>().shootingPlayer = this.gameObject;

        projectile.GetComponent<Projectile>().Initialize(velocity, maxDistance, gravity);

        // Reset dragging state

    }
}
