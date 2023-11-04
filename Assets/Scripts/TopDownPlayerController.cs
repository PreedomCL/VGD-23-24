using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField] private InputManager inputManager;

    private Rigidbody2D playerBody;
    private Animator animator;

    private Vector2 move;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputManager.EnableTopdownControl();
        inputManager.TopdownMoveEvent += OnMovement;
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetBool("Right", move.x > 0);
        // animator.SetBool("Left", move.x < 0);
        // animator.SetBool("Up", move.y > 0);
        // animator.SetBool("Down", move.y < 0);
    }

    public void OnMovement(Vector2 move)
    {
        this.move = move;
    }

    private void FixedUpdate()
    {
        playerBody.velocity = move * speed;
    }
}
