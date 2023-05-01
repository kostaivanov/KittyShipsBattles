using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerHealth : PlayerComponents
{
    private Projectile projectile;

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
        Debug.Log("Hit the target!");
        if (projectile != null && otherObject.gameObject == projectile.shootingPlayer)
        {
            return;
        }

        if (otherObject.CompareTag("Projectile"))
        {
            otherObject.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
