using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerHealth : PlayerComponents
{
    private Projectile projectile;

    internal float healthPoints = 50;

    private void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Projectile projectile = otherObject.GetComponent<Projectile>();

        if (projectile != null && this.gameObject == projectile.shootingPlayer)
        {
            return;
        }

        if (otherObject.CompareTag("Projectile"))
        {
            otherObject.gameObject.SetActive(false);
            healthPoints -= 10;
            Debug.Log("Health points = " + healthPoints);

            if (healthPoints == 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
