using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class PlayerComponents : MonoBehaviour
{
    internal Collider2D playerCollider2D;
    internal Rigidbody2D playerRigidBody;
    internal Animator animator;
    internal SpriteRenderer sprite;

    // Start is called before the first frame update
    internal virtual void Start()
    {
        playerCollider2D = GetComponent<Collider2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
}
