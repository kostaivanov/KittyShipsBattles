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
    private Vector2 velocity;

    internal float CalculateShootingPower(float dragDistance)
    {
        // Calculate shooting power based on the mouse movement
        float shootingPower = Mathf.Clamp01(dragDistance / maxDistance);
        float initialSpeed = Mathf.Lerp(minInitialSpeed, maxInitialSpeed, shootingPower);
        return initialSpeed;
    }

    internal void ShootProjectile(Vector3 initialMousePosition, Vector3 mouseUpPosition, float initialSpeed)
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
