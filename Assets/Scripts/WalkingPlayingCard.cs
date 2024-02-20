using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayingCard : MonoBehaviour
{
    [SerializeField] private float distanceLeft = 5f;
    [SerializeField] private float distanceRight = 5f;
    [SerializeField] private float speed = 1f;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector3 spawnPoint;
    private float direction = -1;

    void Start()
    {
        spawnPoint = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(direction < 0)
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
        }
        else
        {
            animator.SetBool("left", false);
            animator.SetBool("right", true);
        }
    }

    private void FixedUpdate()
    {
        if(direction < 0 && transform.position.x < spawnPoint.x - distanceLeft)
        {
            direction = 1;
        }
        else if(direction > 0 && transform.position.x > spawnPoint.x + distanceRight)
        {
            direction = -1;
        }

        rigidbody.velocity = new Vector2(speed * direction, rigidbody.velocity.y);
    }

    void ResetPosition()
    {
        transform.position = spawnPoint;
        direction = -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<SidePlayerController>().KillPlayer();
        }
    }
}
