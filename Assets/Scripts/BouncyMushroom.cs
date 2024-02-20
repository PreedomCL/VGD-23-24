using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField] private float addedBounce = 0.0f;
    [SerializeField] private float bounceFactor = 5.0f;
    [SerializeField] private Collider2D bounceCollider;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider != bounceCollider)
            return;
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerBody = collision.collider.GetComponent<Rigidbody2D>();
            playerBody.velocity = new Vector2(playerBody.velocity.x, -(collision.relativeVelocity.y * bounceFactor + Mathf.Sign(collision.relativeVelocity.y) * addedBounce));
        }
    }
}
