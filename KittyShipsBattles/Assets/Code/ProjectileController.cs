using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float minInitialSpeed = 5f;
    public float maxInitialSpeed = 20f;
    public float maxDistance = 100f;
    public Vector2 gravity = new Vector2(0, -9.81f);
    private Vector2 initialMousePosition;
    private Vector2 velocity;
    private bool isDragging = false;

    void Update()
    {
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Detect mouse button press and start dragging
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = mousePosition;
            isDragging = true;
        }
        // Detect mouse button release and shoot projectile
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Calculate shooting power based on the mouse movement
            float shootingPower = Mathf.Clamp01(Vector2.Distance(initialMousePosition, mousePosition) / maxDistance);
            float initialSpeed = Mathf.Lerp(minInitialSpeed, maxInitialSpeed, shootingPower);

            // Calculate velocity based on the mouse movement direction
            velocity = (initialMousePosition - mousePosition).normalized * initialSpeed;

            // Instantiate projectile and set its initial position and velocity
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(velocity, maxDistance, gravity);

            // Reset dragging state
            isDragging = false;
        }
    }
}
