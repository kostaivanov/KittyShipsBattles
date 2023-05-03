using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject projectilePrefab;
    [SerializeField] private GameObject muzzle;
    public float minInitialSpeed = 5f;
    public float maxInitialSpeed = 20f;
    public float maxDistance = 100f;
    public Vector2 gravity = new Vector2(0, -9.81f);
    private Vector3 initialMousePosition;
    private Vector2 velocity;
    private bool isDragging = false;

    private TrajectoryLine trajectoryLine;

    private PlayerMovement playerMovement;
    private Camera mainCamera;
    private float mouseDownTime;
    [SerializeField] private float maxClickDuration; // Maximum time the mouse button can be held down for a click to be registered
    [SerializeField] private float minDragDistance; // Maximum time the mouse button can be held down for a click to be registered

    private void Start()
    {
        mainCamera = Camera.main;
        trajectoryLine = GetComponent<TrajectoryLine>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        // Detect mouse button press and start dragging
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
            isDragging = true;
            mouseDownTime = Time.time;
        }
        // Detect mouse button release and shoot projectile
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            float clickDuration = Time.time - mouseDownTime;
            Vector3 mouseUpPosition = Input.mousePosition;
            float dragDistance = Vector2.Distance(initialMousePosition, mouseUpPosition);

            // Calculate shooting power based on the mouse movement
            float shootingPower = Mathf.Clamp01(dragDistance / maxDistance);
            float initialSpeed = Mathf.Lerp(minInitialSpeed, maxInitialSpeed, shootingPower);

            // Get mouse position in world coordinates
            Vector3 clickedWorldPosition = mainCamera.ScreenToWorldPoint(mouseUpPosition);
            clickedWorldPosition.z = 0;

            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(clickedWorldPosition);

            Debug.Log("Shooting power - " + shootingPower);
            if (clickDuration < maxClickDuration && dragDistance < minDragDistance / maxDistance &&
            viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                // Move player to the clicked position
                playerMovement.MovePlayer();
            }
            else
            {
                // Shoot the projectile
                ShootProjectile(mouseUpPosition, initialSpeed);
            }

            isDragging = false;
            trajectoryLine.EndLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = Input.mousePosition;
            Vector3 initialWorldPosition = mainCamera.ScreenToWorldPoint(initialMousePosition);
            initialWorldPosition.z = 0;
            Vector3 currentWorldPosition = mainCamera.ScreenToWorldPoint(currentPoint);
            currentWorldPosition.z = 0;
            trajectoryLine.RenderLine(initialWorldPosition, currentWorldPosition);
        }
    }

    private void ShootProjectile(Vector3 mouseUpPosition, float initialSpeed)
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
