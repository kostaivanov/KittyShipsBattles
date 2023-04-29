using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 velocity;
    private float maxDistance;
    private Vector2 startPosition;
    private Vector2 gravity;
    public float dragCoefficient = 0.1f;

    public void Initialize(Vector2 initialVelocity, float distance, Vector2 gravityVector)
    {
        velocity = initialVelocity;
        maxDistance = distance;
        startPosition = transform.position;
        gravity = gravityVector;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Update projectile position based on velocity
        transform.position += (Vector3)(velocity * deltaTime);

        // Calculate air resistance (drag)
        Vector2 airResistance = -dragCoefficient * velocity * velocity.magnitude;

        // Update projectile velocity based on gravity and air resistance
        velocity += (gravity + airResistance) * deltaTime;

        // Check if the projectile has reached maxDistance
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
